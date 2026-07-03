using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PressureCalculator;

// 260704Cl 追加: マニュアル用スクリーンショットの非対話生成 (兄弟アプリの --capture と同方式)。
//   使い方: PressureCalculator.exe --capture <出力フォルダ> <カルチャ名 (en/ja/de/...)>
//   FormMain を指定カルチャで起動し、3 モード (ruby/diamond/eos) の PNG を逐次保存して終了する。
//   キャプチャ中は無操作必須 (フォーカス喪失でツールチップ等が乱れるため)。
internal static class GuiCapture
{
    /// <summary>Program.Main の --capture 分岐から呼ぶ。戻り値は exit code。</summary>
    public static int Run(string[] args)
    {
        var outDir = args.Length >= 2 ? args[1] : "capture";
        var culture = args.Length >= 3 ? args[2] : "en";
        Directory.CreateDirectory(outDir);

        var ci = new CultureInfo(Crystallography.SupportedCultures.Resolve(culture).Name);
        Thread.CurrentThread.CurrentUICulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;

        PressureCalculatorLocalizationData.Register();

        Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetDefaultFont(Crystallography.Controls.FontHelper.GetUIFont());

        int exitCode = 0;
        var f = new FormMain();
        f.Shown += (s, e) =>
        {
            try
            {
                foreach (var mode in new[] { "ruby", "diamond", "eos" })
                {
                    f.SetCaptureMode(mode);
                    Application.DoEvents(); // レイアウト・CheckedChanged を反映させてから撮る
                    using var bmp = new Bitmap(f.Width, f.Height);
                    f.DrawToBitmap(bmp, new Rectangle(0, 0, f.Width, f.Height));
                    bmp.Save(Path.Combine(outDir, $"FormMain-{mode}.png"), ImageFormat.Png); // PNG は逐次保存 (途中クラッシュ対策)
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"capture failed: {ex}");
                exitCode = 1;
            }
            finally
            {
                f.Close();
            }
        };
        Application.Run(f);
        return exitCode;
    }
}
