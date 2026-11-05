"""Independent Python re-implementation of the PressureCalculator scales, written from the formulas
read in FormMain.cs / Crystallography EOS.cs (not a transliteration of the C# control flow)."""
import re, sys, math, csv
import numpy as np
from scipy.interpolate import CubicSpline
from scipy.integrate import quad

EOS_SRC = sys.argv[1]; FORMMAIN = sys.argv[2]; CS_OUT = sys.argv[3]
R = 8.3144621; NA = 6.0221367e23

def BM3(K0, Kp, x):   # x = V0/V
    return 1.5*K0*(x**(7/3)-x**(5/3))*(1+0.75*(Kp-4)*(x**(2/3)-1))
def BM4(K0, Kp, Kpp, x):
    f = 0.5*(x**(2/3)-1)
    return 3*K0*f*(1+2*f)**2.5*(1+1.5*(Kp-4)*f+1.5*(K0*Kpp+(Kp-4)*(Kp-3)+35/9)*f*f)
def Vinet(K0, Kp, x):
    y = x**(-1/3)
    return 3*K0*(1-y)/y**2*math.exp(1.5*(Kp-1)*(1-y))
def D3int(x):  # int_0^x z^3/(e^z-1) dz
    return quad(lambda z: z**3/math.expm1(z) if z > 0 else 0.0, 0, x, limit=200)[0]
def Eth(n, T, theta):   # Debye thermal energy per mol formula, J/mol
    x = theta/T
    return 9*n*R*T/x**3*D3int(x)
def Pth_MG_powerlaw(Z, n, th0, g0, q, T0, V0A3, T, VA3):
    """gamma = g0 (V/V0)^q, theta = th0 exp((g0-gamma)/q); V in A^3 per cell -> m^3/mol"""
    Vm = VA3*1e-30*NA/Z
    g = g0*(VA3/V0A3)**q; th = th0*math.exp((g0-g)/q)
    return g/Vm*(Eth(n, T, th)-Eth(n, T0, th))*1e-9
def Pth_MG_altshuler(Z, n, th0, g0, a, b, T0, V0A3, T, VA3):
    """gamma = g0[1+a((V/V0)^b-1)], theta = th0 (V/V0)^{-(1-a) g0} exp((g0-gamma)/b)"""
    Vm = VA3*1e-30*NA/Z; r = VA3/V0A3
    g = g0*(1+a*(r**b-1)); th = th0*r**(-(1-a)*g0)*math.exp((g0-g)/b)
    return g/Vm*(Eth(n, T, th)-Eth(n, T0, th))*1e-9

# ---- tables -> natural cubic spline in x for each T, then natural spline in T, linear extrapolation outside knots
src = open(EOS_SRC, encoding='utf-8-sig').read().split('\n')
def read_table(decl):
    i = next(k for k, l in enumerate(src) if decl in l)
    rows = []
    while True:
        l = src[i]
        if '[' in l and re.search(r'\d', l):
            body = l[l.index('[')+1:]
            nums = re.findall(r'double\.NaN|-?\d+\.?\d*(?:[eE][-+]?\d+)?', body)
            if len(nums) >= 2:
                rows.append([float('nan') if 'NaN' in s else float(s) for s in nums])
        if l.rstrip().endswith('];') or l.strip() == '];':
            break
        i += 1
    return rows
def nat_spline(xs, ys):
    o = np.argsort(xs); xs = np.asarray(xs)[o]; ys = np.asarray(ys)[o]
    cs = CubicSpline(xs, ys, bc_type='natural')
    d0 = cs(xs[0], 1); d1 = cs(xs[-1], 1)
    def f(x):
        if x <= xs[0]: return float(ys[0]+d0*(x-xs[0]))
        if x >= xs[-1]: return float(ys[-1]+d1*(x-xs[-1]))
        return float(cs(x))
    return f
class Table:
    def __init__(self, decl):
        rows = read_table(decl); self.T = rows[0][1:]; self.sp = []
        for j in range(len(self.T)):
            pts = [(r[0], r[j+1]) for r in rows[1:] if not math.isnan(r[j+1])]
            self.sp.append(nat_spline([p[0] for p in pts], [p[1] for p in pts]))
        self.xr = (min(r[0] for r in rows[1:]), max(r[0] for r in rows[1:]))
    def P(self, x, T):
        return nat_spline(self.T, [s(x) for s in self.sp])(T)
TAB = {k: Table(k+' =') for k in ['AuJamieson', 'PtJamieson', 'Brown', 'Tsuchiya', 'Zha', 'Table_Au_Yokoo', 'Table_Pt_Yokoo']}
ross = read_table('Ross =')[1:]; ross_sp = nat_spline([r[0] for r in ross], [r[1] for r in ross])

fm = open(FORMMAIN, encoding='utf-8-sig').read()
pbA0 = [(float(a), float(b)) for a, b in re.findall(r'\(\s*(\d+)\s*,\s*(4\.\d+)\s*\)', fm)]
pbV = [(float(a), float(b), float(c)) for a, b, c in re.findall(r'\((\d+),\s+(4\d\.\d+)\s*,\s+(5\.\d+)\)', fm)]
lin = lambda pts, t: float(np.interp(t, [p[0] for p in pts], [p[1] for p in pts])) if pts[0][0] <= t <= pts[-1][0] else \
    (pts[0][1]+(pts[1][1]-pts[0][1])/(pts[1][0]-pts[0][0])*(t-pts[0][0]) if t < pts[0][0] else pts[-1][1]+(pts[-1][1]-pts[-2][1])/(pts[-1][0]-pts[-2][0])*(t-pts[-1][0]))

def py(mat, sc, inp, T):
    T0 = 300.0
    if mat in ('Au', 'Pt', 'NaClB1', 'MgO', 'Al2O3', 'Re', 'Mo', 'Pb'):
        vr = inp; x = 1/vr
    if mat == 'Au':
        V0 = 4.07825**3; V = V0*vr
        return {'Jamieson82': lambda: TAB['AuJamieson'].P(1-vr, T),
                'Anderson89': lambda: BM3(166.65, 5.4823, x)+(0.00714-0.0115*math.log(x))*(T-300),
                'Sim02': lambda: BM3(167, 5.0, x)+Pth_MG_powerlaw(4, 1, 170, 2.97, 1.0, 300, V0, T, V),
                'Tsuchiya03': lambda: TAB['Tsuchiya'].P(1-vr, T),
                'Yokoo09': lambda: TAB['Table_Au_Yokoo'].P(1-vr, T),
                'Fratanduono21': lambda: Vinet(170.09, 5.880, x)}[sc]()
    if mat == 'Pt':
        V0 = 3.9231**3; V = V0*vr
        def matsui():
            Vm = V*1e-30*NA/4; V0m = V0*1e-30*NA/4
            g = 2.70*(V/V0)**1.10; th = 230*math.exp((2.70-g)/1.10)
            Pel = -1.01162E-11*T**3+1.89796E-07*T**2+6.73266E-05*T-3.70065E-02
            # n = 1 atom per formula; molar volume per formula unit (Z = 4)
            return Vinet(273, 5.20, x)+g/Vm*(Eth(1, T, th)-Eth(1, T0, th))*1e-9+Pel
        return {'Jamieson82': lambda: TAB['PtJamieson'].P(1-vr, T),
                'Holmes89': lambda: Vinet(266, 5.81, x)+0.261e-4*266*(T-300),
                'Matsui09': matsui,
                'Yokoo09': lambda: TAB['Table_Pt_Yokoo'].P(1-vr, T),
                'Fratanduono21': lambda: Vinet(259.7, 5.839, x)}[sc]()
    if mat == 'NaClB1':
        V0 = 5.639**3; V = V0*vr
        return {'Brown99': lambda: TAB['Brown'].P(1-vr, T),
                'Matsui12': lambda: BM4(23.7, 5.14, -0.392, x)+Pth_MG_powerlaw(4, 2, 279, 1.56, 0.96, 300, V0, T, V)}[sc]()
    if mat == 'MgO':
        V0 = 4.2112**3; V = V0*vr
        return {'Jackson98': lambda: BM3(162.5, 4.13, x)+Pth_MG_powerlaw(4, 2, 673, 1.41, 1.3, 300, V0, T, V),
                'Dewaele00': lambda: BM3(161, 3.94, x)+Pth_MG_powerlaw(4, 2, 800, 1.45, 0.8, 300, V0, T, V),
                'Aizawa06': lambda: BM3(160, 4.15, x)+Pth_MG_powerlaw(4, 2, 773, 1.41, 0.7, 300, V0, T, V),
                'Tange09Vinet': lambda: Vinet(160.63, 4.367, x)+Pth_MG_altshuler(4, 2, 761, 1.442, 0.138, 5.4, 300, V0, T, V),
                'Tange09BM': lambda: BM3(160.64, 4.221, x)+Pth_MG_altshuler(4, 2, 761, 1.431, 0.29, 3.5, 300, V0, T, V)}[sc]()
    if mat == 'Al2O3':
        V0 = 255.89472; V = V0*vr
        V0T = V0*math.exp(2.6e-5*(T-300)+1.81e-9/2*(T*T-300*300)+0.67*(1/T-1/300))
        return BM3(258-0.020*(T-300), 4.88, V0T/V)
    if mat == 'Re':
        V = 29.42795*vr
        return {'Zha04': lambda: TAB['Zha'].P(1-vr, T), 'Anz': lambda: Vinet(352.6, 4.56, 29.467/V),
                'Sakai': lambda: Vinet(358, 4.8, 29.47/V), 'Dub': lambda: BM4(342, 6.15, -0.029, 29.46/V)}[sc]()
    if mat == 'Mo':
        V0 = 31.14; V = V0*vr
        if sc == 'Huang16': return BM3(255, 4.25, x)+Pth_MG_powerlaw(2, 1, 470, 2.01, 0.6, 300, V0, T, V)
        V0T = V0*math.exp(1.31e-5*(T-300)+11.2e-9/2*(T*T-300*300)); K = 268-2.13e-2*(T-300)
        return BM4(K, 3.81, -1.41e-2, V0T/V)
    if mat == 'NaClB2':
        a = inp; V = a**3
        if sc.startswith('Sata'):
            Pr, Kr = (31.14, 143.5) if sc == 'Sata02Pt' else (32.15, 141.0)
            y = (V/27.17)**(1/3); return Pr*y**-2*math.exp(-(3*Kr/Pr-2)*(y-1))
        return {'Ueda08': lambda: Vinet(28.45, 5.16, (24.76e-6/NA*1e30)/V)+0.00468*(T-300),
                'Sakai11BM': lambda: BM3(47.00, 4.10, 37.73/V), 'Sakai11Vinet': lambda: Vinet(40.40, 5.04, 37.73/V)}[sc]()
    if mat == 'Ar':
        a = inp; Vm = a**3*1e-24*NA/4  # cm^3/mol (per atom)
        if sc == 'Ross86': return ross_sp(Vm)
        V0m = 22.557; g = 0.5+2.20*(Vm/V0m)
        return BM3(3.03, 7.24, V0m/Vm)+g/(Vm*1e-6)*(Eth(1, T, 93.3)-Eth(1, 4, 93.3))*1e-9
    if mat == 'Pb':
        a0 = 4.95059; a = a0*vr**(1/3)
        a0T = a0/lin(pbA0, 300)*lin(pbA0, T)
        B = lin([(p[0], p[1]) for p in pbV], T); Bp = lin([(p[0], p[2]) for p in pbV], T)
        return Vinet(B, Bp, (a0T/a)**3)
    if mat == 'Ruby':
        r = inp/694.25; d = r-1
        return {'Mao78': lambda: 1904/5*(r**5-1), 'MaoQuasi86': lambda: 1904/7.665*(r**7.665-1),
                'MaoHydro': lambda: 1904/7.715*(r**7.715-1), 'Shen20': lambda: 1870*d*(1+5.63*d)}[sc]()
    if mat == 'Ragan':
        nu_ref = 1e7/694.25; f = lambda t: 4.49E-2*t-4.81E-4*t*t+3.71E-7*t**3
        c = nu_ref-f(297.15)
        return c if sc == 'prm' else 1e7/(c+f(T))
    if mat == 'Diamond':
        nu = inp; x = (nu-1334)/1334
        return {'Akahama04': lambda: 66.9-0.5281*nu+3.585e-4*nu*nu, 'Akahama06': lambda: 547*x*(1+0.5*2.75*x),
                'FratLow_code': lambda: 530.77*x+753.83*x*x, 'FratHigh': lambda: 199.49-852.78*x+3103.8*x*x}[sc]()
    raise KeyError(mat)

out = csv.writer(open(sys.argv[4], 'w', newline=''))
out.writerow(['material', 'scale', 'input', 'T_K', 'P_cs', 'P_py', 'diff'])
worst = {}
for mat, sc, inp, T, pcs in csv.reader(open(CS_OUT)):
    inp = float(inp); T = float(T); pcs = float(pcs)
    try: ppy = py(mat, sc, inp, T)
    except Exception as e: ppy = float('nan')
    d = pcs-ppy
    out.writerow([mat, sc, inp, T, f'{pcs:.6g}', f'{ppy:.6g}', f'{d:.3g}'])
    k = (mat, sc); worst[k] = max(worst.get(k, 0), abs(d) if not math.isnan(d) else float('inf'))
for k, v in worst.items(): print(f'{k[0]:8s} {k[1]:14s} max|cs-py| = {v:.3g}')
