import csv, collections, sys
rows = list(csv.DictReader(open('compare.csv')))
D = collections.OrderedDict()
for r in rows:
    D.setdefault((r['material'], r['scale']), {})[(float(r['input']), float(r['T_K']))] = (float(r['P_cs']), float(r['P_py']), float(r['diff']))
def maxdiff(k): return max(abs(v[2]) for v in D[k].values())
def f(v): return f'{v:.3f}'
out = []
def tbl(title, keys, cols, hdr):
    out.append(f'\n**{title}**\n')
    out.append('| scale | ' + ' | '.join(h for h in hdr) + ' | max\\|C#−Py\\| |')
    out.append('|---|' + '---:|'*len(hdr) + '---:|')
    for k in keys:
        if k not in D: continue
        cells = [f(D[k][c][0]) if c in D[k] else '—' for c in cols]
        out.append(f'| {k[0]} {k[1]} | ' + ' | '.join(cells) + f' | {maxdiff(k):.1e} |')
vr_mats = ['Au','Pt','NaClB1','MgO','Al2O3','Re','Mo','Pb']
keys = [k for k in D if k[0] in vr_mats]
cols = [(0.95,300.0),(0.90,300.0),(0.85,300.0),(0.80,300.0)]
tbl('V/V0 dependence at T = 300 K (P in GPa)', keys, cols, ['V/V₀=0.95','0.90','0.85','0.80'])
keysT = [k for k in keys if k[0] != 'Pb']
cols = [(1.0,1000.0),(0.90,1000.0),(1.0,2000.0),(0.90,2000.0)]
tbl('T dependence (P in GPa); V₀ = UI default (300 K value)', keysT, cols, ['V/V₀=1.00, 1000 K','0.90, 1000 K','1.00, 2000 K','0.90, 2000 K'])
tbl('Pb (T₀ = 300 K, a₀ = 4.95059 Å)', [k for k in keys if k[0]=='Pb'], [(1.0,10.0),(0.9,10.0),(1.0,100.0),(0.9,100.0),(1.0,400.0)], ['V/V₀=1.00, 10 K','0.90, 10 K','1.00, 100 K','0.90, 100 K','1.00, 400 K'])
k2 = [k for k in D if k[0]=='NaClB2']
tbl('NaCl B2: input a (Å), T = 300 K / 2000 K', k2, [(3.0,300.0),(2.93,300.0),(2.85,300.0),(2.75,300.0),(2.93,2000.0)], ['a=3.00','2.93','2.85','2.75','2.93 @2000 K'])
k3 = [k for k in D if k[0]=='Ar']
tbl('Ar: input a (Å)', k3, [(4.8,300.0),(4.4,300.0),(4.0786,300.0),(3.6,300.0),(4.0786,1000.0)], ['a=4.80, 300 K','4.40, 300 K','4.0786, 300 K','3.60, 300 K','4.0786, 1000 K'])
k4 = [k for k in D if k[0]=='Ruby']
tbl('Ruby: input R1 (nm), R1₀ = 694.25 nm, A = UI default', k4, [(697.0,297.15),(700.0,297.15),(705.0,297.15),(710.0,297.15),(720.0,297.15)], ['R1=697','700','705','710','720'])
k5 = [k for k in D if k[0]=='Diamond']
tbl('Diamond: input ν (cm⁻¹), ν₀ = 1334 cm⁻¹, coefficients = UI default', k5, [(1334.0,300.0),(1400.0,300.0),(1500.0,300.0),(1600.0,300.0),(1800.0,300.0),(2000.0,300.0)], ['ν=1334','1400','1500','1600','1800','2000'])
k6 = [k for k in D if k==('Ragan','R1_0')]
tbl('Ragan: R1₀(T) (nm) after calibrating with R1 = 694.25 nm at 297.15 K', k6, [(694.25,50.0),(694.25,100.0),(694.25,200.0),(694.25,400.0),(694.25,600.0)], ['50 K','100 K','200 K','400 K','600 K'])
print('\n'.join(out))
