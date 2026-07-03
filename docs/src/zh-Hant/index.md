# PressureCalculator

PressureCalculator 是一款免費的 Windows 應用程式，用於高壓實驗（如鑽石壓砧實驗）中的壓力測定。它支援三種互補的方法：

- **[紅寶石螢光](1-ruby-fluorescence.md)** : 由紅寶石 R1 螢光線的位移求得壓力，內建多種已發表的紅寶石壓力標度與溫度修正。
- **[鑽石拉曼邊緣](2-diamond-raman.md)** : 由鑽石壓砧拉曼帶的高波數側邊緣求得壓力。
- **[狀態方程式 (EOS)](3-equation-of-states.md)** : 由金、鉑、NaCl、方鎂石等標準物質的晶格常數（或單位晶胞體積）測量值求得壓力。

測得的光譜可從文字檔載入，並直接在應用程式中進行平滑化與擬合；詳見[光譜與擬合](4-spectra-and-fitting.md)。

![主視窗（紅寶石螢光模式）](../assets/cap-zh-Hant-auto/FormMain-ruby.png){width=700px}

## 安裝

請從 [GitHub Releases 頁面](https://github.com/seto77/PressureCalculator/releases/latest) 下載最新版本。

| 檔案 | 說明 |
|---|---|
| `PressureCalculator-setup.msi` | **建議使用。** 適用於一般 (x64) Windows PC 的安裝程式。 |
| `PressureCalculator-setup_arm64.msi` | 適用於 Windows on Arm（Snapdragon PC，或以虛擬化方式執行 Windows 的 Apple Silicon Mac 等）的安裝程式。 |
| `PressureCalculator-v.X.zip` | 可攜版 (x64)：免安裝、自我完備。適合沒有系統管理員權限的 PC。 |
| `PressureCalculator-v.X_arm64.zip` | Windows on Arm 用可攜版。 |

MSI 安裝程式需要 .NET Desktop Runtime 10；若尚未安裝，首次啟動時 Windows 會顯示附下載連結的對話方塊。可攜 ZIP 版已內含執行階段，無需另行安裝：只要將 ZIP 解壓縮到可寫入的資料夾並執行 `PressureCalculator.exe` 即可。

PressureCalculator 以使用者為單位安裝（不需系統管理員權限），設定儲存於 `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`。

## 顯示語言

使用者介面支援 11 種語言。在功能表列的 **Language** 中選擇語言後，PressureCalculator 會以新語言重新啟動。從應用程式開啟本線上手冊時，也會顯示相同的語言版本。

## 線上說明

在應用程式中按 ++f1++（或選擇 **說明 → 線上手冊**），即可開啟對應目前模式的手冊頁面。
