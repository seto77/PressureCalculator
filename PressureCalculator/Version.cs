namespace PressureCalculator
{
    class Version
    {

        static public string Software =
            "Pressure Calculator"
            ;
        //static public string VersionAndDate { get => History[10..][..20]; } //260805Cl 変更前: 括弧前スペース無し前提の20文字固定長
        static public string VersionAndDate { get => History[10..(History.IndexOf(')') + 1)]; } //260805Cl 版番と日付括弧の間のスペース挿入に伴い ')' までを取り出す (長さ非依存)

        //260805Cl 表記統一: 版番と日付括弧の間にスペースを1つ入れる。⚠この行より上の行(このコメント含む)に半角スペース直後の『ver』を書かない (CI と旧版の更新チェックが History 先頭行より先に拾う)
        static public string History =
           "History" +
           "\r\n ver1.009 (2025/10/16) Improved the EOS functions." +
           "\r\n ver1.008 (2025/10/16) Improved the EOS functions." +
           "\r\n ver1.007 (2023/10/11) Improved the export functions." +
           "\r\n ver1.004 (2023/10/07) Fitting information is now displayed. Target framework has been changed to .Net Desktop Runtime 7.0." +
           "\r\n ver1.003 (2021/09/01) Fixed a bug in Celsius degree mode. The lower limit of temperature was set." +
           "\r\n ver1.002 (2021/07/03) Changed a framework to .Net 5.0; Added two diamond Raman equations (Fratanduono et al., 2021)." +
           "\r\n ver1.001 (2021/05/15) Distribution site is changed to GitHub." +
           "\r\n ver0.000 (???/??/??)  "
           ;
    }
}
