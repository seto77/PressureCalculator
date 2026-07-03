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

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
