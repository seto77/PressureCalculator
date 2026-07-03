using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PressureCalculator
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        // private static void Main() // 260703Cl 旧シグネチャ (--smoke 引数対応のため string[] args を追加)
        [STAThread]
        static void Main(string[] args)
        {
            // 260703Cl 追加: 軽量 smoke テスト (WiX 移行 / arm64 CI 用、CSManager 同型)。
            //   arm64 の「ビルド緑・実行時死亡」型故障を CI で検出する。引数なし通常起動には一切影響しない。
            if (args.Length >= 1 && args[0] == "--smoke")
            {
                System.IO.File.WriteAllLines(args.Length >= 2 ? args[1] : "smoke-result.txt",
                [
                    $"arch={System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture}",
                    $"baseDir={AppContext.BaseDirectory}",
                ]);
                return;
            }

            // 260704Cl 追加: 多言語化 (CSManager 同型)。言語別 UI フォント (FontHelper.GetUIFont) と CodeLocalizer が
            //   CurrentUICulture を参照するため、フォーム生成 (SetDefaultFont/Application.Run) より前に、
            //   レジストリ保存値からカルチャを確定させる。未知カルチャは SupportedCultures.Resolve が既定 (英語) へ解決。
            try
            {
                using var regKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\PressureCalculator");
                var culture = (string)regKey?.GetValue("Culture", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                var ci = new System.Globalization.CultureInfo(Crystallography.SupportedCultures.Resolve(culture).Name);
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = ci;
            }
            catch { }

            // 260704Cl 追加: アプリ固有の Localizable=false 訳テーブルを共有レジストリへ登録 (フォーム生成前に 1 回)
            PressureCalculatorLocalizationData.Register();

            // 260704Cl 変更: SystemAware → DpiUnawareGdiScaled (CSManager/IPAnalyzer 同型)。
            //   固定レイアウトの単一フォームのため、高 DPI では OS の GDI スケーリングで拡大する (レイアウト崩れなし)。
            //旧: Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetDefaultFont(Crystallography.Controls.FontHelper.GetUIFont());// 260704Cl 追加: 言語別 UI フォント (Designer 未指定コントロールの既定)
            Application.Run(new FormMain());
        }
    }
}
