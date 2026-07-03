namespace PressureCalculator;

// 260704Cl 追加: 多言語化。Localizable=false の FormMain の可視ラベル訳テーブル (全11言語)。
// 共有 Crystallography.Localization の中央レジストリへ app-local provider として登録 (Program.Main 冒頭で Register())。
// CodeLocalizer が FullName キー ("PressureCalculator.FormMain") で引き、FormBase.OnLoad で実行時に差し替える。
// en は Designer 原文ママ (Measurement/Calculate のタイポ修正のみ)。IPAnalyzer/IPAnalyzerLocalizationData.cs と同方式。
// 物理記号 (ν, K0, GPa 等)・文献ラベル (Yokoo (09) 等)・元素記号 (Ar/Re/Mo/Pb)・NaCl B1/B2 は言語非依存のため対象外。
internal static class PressureCalculatorLocalizationData
{
    /// <summary>フォーム生成前に1回呼ぶこと (Program.Main 冒頭)。</summary>
    public static void Register() => Crystallography.Localization.AddProvider(Populate);

    private static void Populate(System.Collections.Generic.Dictionary<string, Crystallography.Localization.Entry[]> reg)
    {
        reg["PressureCalculator.FormMain"] = new Crystallography.Localization.Entry[]
        {
            // メニュー
            new("fileToolStripMenuItem", "Text", "File", "ファイル", "Datei", "Fichier", "Archivo", "Arquivo", "File", "Файл", "文件", "檔案", "파일"),
            new("readToolStripMenuItem", "Text", "Load", "読み込み", "Laden", "Charger", "Cargar", "Carregar", "Carica", "Загрузить", "加载", "載入", "불러오기"),
            new("exportAsCSVToolStripMenuItem", "Text", "Export as CSV", "CSV形式でエクスポート", "Als CSV exportieren", "Exporter en CSV", "Exportar como CSV", "Exportar como CSV", "Esporta come CSV", "Экспорт в CSV", "导出为 CSV", "匯出為 CSV", "CSV로 내보내기"),
            new("watchNewFileToolStripMenuItem", "Text", "Reload the file if it is updated", "ファイルが更新されたら再読み込み", "Datei bei Aktualisierung neu laden", "Recharger le fichier s'il est mis à jour", "Recargar el archivo si se actualiza", "Recarregar o arquivo se for atualizado", "Ricarica il file se viene aggiornato", "Перезагружать файл при обновлении", "文件更新时重新加载", "檔案更新時重新載入", "파일이 갱신되면 다시 불러오기"),
            new("helpToolStripMenuItem", "Text", "Help", "ヘルプ", "Hilfe", "Aide", "Ayuda", "Ajuda", "Aiuto", "Справка", "帮助", "說明", "도움말"),
            new("helpOnlineManualToolStripMenuItem", "Text", "Online manual", "オンラインマニュアル", "Online-Handbuch", "Manuel en ligne", "Manual en línea", "Manual on-line", "Manuale online", "Онлайн-руководство", "在线手册", "線上手冊", "온라인 매뉴얼"),
            // モード選択
            new("radioButtonDiamondRaman", "Text", "Diamond Raman", "ダイヤモンドラマン", "Diamant-Raman", "Raman du diamant", "Raman del diamante", "Raman do diamante", "Raman del diamante", "Раман алмаза", "金刚石拉曼", "鑽石拉曼", "다이아몬드 라만"),
            new("radioButtonRubyFluorescence", "Text", "Ruby Fluorescence", "ルビー蛍光", "Rubin-Fluoreszenz", "Fluorescence du rubis", "Fluorescencia del rubí", "Fluorescência do rubi", "Fluorescenza del rubino", "Флуоресценция рубина", "红宝石荧光", "紅寶石螢光", "루비 형광"),
            // スペクトル表示部
            new("label14", "Text", "Original spectrum", "元のスペクトル", "Originalspektrum", "Spectre original", "Espectro original", "Espectro original", "Spettro originale", "Исходный спектр", "原始光谱", "原始光譜", "원본 스펙트럼"),
            new("label2", "Text", "Running Average", "移動平均", "Gleitender Mittelwert", "Moyenne glissante", "Promedio móvil", "Média móvel", "Media mobile", "Скользящее среднее", "移动平均", "移動平均", "이동 평균"),
            new("label13", "Text", "Running Average", "移動平均", "Gleitender Mittelwert", "Moyenne glissante", "Promedio móvil", "Média móvel", "Media mobile", "Скользящее среднее", "移动平均", "移動平均", "이동 평균"),
            new("label12", "Text", "Gaussian blur σ", "ガウスぼかし σ", "Gaußsche Unschärfe σ", "Flou gaussien σ", "Desenfoque gaussiano σ", "Desfoque gaussiano σ", "Sfocatura gaussiana σ", "Гауссово размытие σ", "高斯模糊 σ", "高斯模糊 σ", "가우시안 블러 σ"),
            new("label1", "Text", "Gaussian blur σ", "ガウスぼかし σ", "Gaußsche Unschärfe σ", "Flou gaussien σ", "Desenfoque gaussiano σ", "Desfoque gaussiano σ", "Sfocatura gaussiana σ", "Гауссово размытие σ", "高斯模糊 σ", "高斯模糊 σ", "가우시안 블러 σ"),
            new("labelBottomTitle", "Text", "First Differentiation", "一次微分", "Erste Ableitung", "Dérivée première", "Primera derivada", "Primeira derivada", "Derivata prima", "Первая производная", "一阶微分", "一階微分", "1차 미분"),
            new("label25", "Text", "Fitting Information", "フィッティング情報", "Fitting-Informationen", "Informations d'ajustement", "Información del ajuste", "Informações do ajuste", "Informazioni sul fitting", "Информация о подгонке", "拟合信息", "擬合資訊", "피팅 정보"),
            new("label10", "Text", "Fitting Range", "フィッティング範囲", "Fitting-Bereich", "Plage d'ajustement", "Rango de ajuste", "Intervalo de ajuste", "Intervallo di fitting", "Диапазон подгонки", "拟合范围", "擬合範圍", "피팅 범위"),
            // ルビー蛍光グループ
            new("groupBoxMao", "Text", "Pressure calculation from the ruby fluorescence", "ルビー蛍光からの圧力計算", "Druckberechnung aus der Rubin-Fluoreszenz", "Calcul de la pression à partir de la fluorescence du rubis", "Cálculo de la presión a partir de la fluorescencia del rubí", "Cálculo da pressão a partir da fluorescência do rubi", "Calcolo della pressione dalla fluorescenza del rubino", "Расчет давления по флуоресценции рубина", "由红宝石荧光计算压力", "由紅寶石螢光計算壓力", "루비 형광으로부터 압력 계산"),
            new("groupBox4", "Text", "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "圧力計算 (x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀)", "Druckberechnung mit x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "Calcul de la pression, où x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "Cálculo de la presión, donde x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "Cálculo da pressão, onde x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "Calcolo della pressione, dove x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "Расчет давления, где x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "压力计算，其中 x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "壓力計算，其中 x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀", "압력 계산 (x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀)"),
            new("groupBox2", "Text", "Temperature dependency (Ragan et al., 1992) (Applicable in the range of 50-600K)", "温度依存性 (Ragan et al., 1992) (適用範囲 50-600K)", "Temperaturabhängigkeit (Ragan et al., 1992) (gültig im Bereich 50-600 K)", "Dépendance en température (Ragan et al., 1992) (applicable de 50 à 600 K)", "Dependencia de la temperatura (Ragan et al., 1992) (aplicable en el rango de 50-600 K)", "Dependência da temperatura (Ragan et al., 1992) (aplicável na faixa de 50-600 K)", "Dipendenza dalla temperatura (Ragan et al., 1992) (applicabile nell'intervallo 50-600 K)", "Температурная зависимость (Ragan et al., 1992) (применимо в диапазоне 50-600 К)", "温度依赖性 (Ragan et al., 1992)（适用范围 50-600K）", "溫度相依性 (Ragan et al., 1992)（適用範圍 50-600K）", "온도 의존성 (Ragan et al., 1992) (적용 범위 50-600K)"),
            new("groupBox3", "Text", "Measurement condition", "測定条件", "Messbedingung", "Condition de mesure", "Condición de medición", "Condição de medição", "Condizione di misura", "Условия измерения", "测量条件", "測量條件", "측정 조건"),
            new("checkBoxRubyR1_0CalculatedFromRagan", "Text", "Calculate from\r\n Ragan's equation", "Ragan の式から計算", "Aus Ragans Gleichung berechnen", "Calculer avec l'équation de Ragan", "Calcular con la ecuación de Ragan", "Calcular pela equação de Ragan", "Calcola dall'equazione di Ragan", "Вычислить по уравнению Рагана", "由 Ragan 方程计算", "由 Ragan 方程式計算", "Ragan 식으로 계산"),
            new("checkBoxRubyTemeratureSameAsRef", "Text", "Same as reference", "参照条件と同じ", "Wie Referenz", "Identique à la référence", "Igual que la referencia", "Igual à referência", "Uguale al riferimento", "Как эталонное", "与参考条件相同", "與參考條件相同", "기준 조건과 동일"),
            new("groupBox1", "Text", "Reference condition", "参照条件", "Referenzbedingung", "Condition de référence", "Condición de referencia", "Condição de referência", "Condizione di riferimento", "Эталонные условия", "参考条件", "參考條件", "기준 조건"),
            new("buttonRubyRefR1Set", "Text", "Set the current R1", "現在の R1 を設定", "Aktuelles R1 übernehmen", "Définir le R1 actuel", "Establecer el R1 actual", "Definir o R1 atual", "Imposta l'R1 corrente", "Задать текущее R1", "设为当前 R1", "設為目前 R1", "현재 R1로 설정"),
            new("label17", "Text", "Temperature unit", "温度単位", "Temperatureinheit", "Unité de température", "Unidad de temperatura", "Unidade de temperatura", "Unità di temperatura", "Единица температуры", "温度单位", "溫度單位", "온도 단위"),
            // ラマンエッジグループ
            new("groupBoxAkahama2006", "Text", "Pressure calculation from the Raman edge", "ラマンエッジからの圧力計算", "Druckberechnung aus der Raman-Kante", "Calcul de la pression à partir du bord Raman", "Cálculo de la presión a partir del borde Raman", "Cálculo da pressão a partir da borda Raman", "Calcolo della pressione dal bordo Raman", "Расчет давления по краю рамановской полосы", "由拉曼边计算压力", "由拉曼邊緣計算壓力", "라만 에지로부터 압력 계산"),
            // EOS 物質グループ (元素記号 Ar/Re/Mo/Pb・NaCl B1/B2 は言語非依存のため対象外)
            new("groupBoxGold", "Text", "Gold", "金", "Gold", "Or", "Oro", "Ouro", "Oro", "Золото", "金", "金", "금"),
            new("groupBoxPlatinum", "Text", "Platinum", "白金", "Platin", "Platine", "Platino", "Platina", "Platino", "Платина", "铂", "鉑", "백금"),
            new("groupBoxPericlase", "Text", "Periclase", "ペリクレース", "Periklas", "Périclase", "Periclasa", "Periclásio", "Periclasio", "Периклаз", "方镁石", "方鎂石", "페리클레이스"),
            new("groupBoxCorundum", "Text", "Corundum", "コランダム", "Korund", "Corindon", "Corindón", "Coríndon", "Corindone", "Корунд", "刚玉", "剛玉", "강옥"),
        };
    }
}
