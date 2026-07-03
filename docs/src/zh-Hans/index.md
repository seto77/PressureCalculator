# PressureCalculator

PressureCalculator 是一款免费的 Windows 应用程序，用于确定高压实验（如金刚石压砧实验）中的压力。它支持三种互补的方法：

- **[红宝石荧光](1-ruby-fluorescence.md)** : 由红宝石 R1 荧光线的移动计算压力，内置多种已发表的红宝石压标，并支持温度校正。
- **[金刚石拉曼边](2-diamond-raman.md)** : 由金刚石压砧拉曼谱带的高频边计算压力。
- **[状态方程 (EOS)](3-equation-of-states.md)** : 由金、铂、NaCl、方镁石等标准物质的晶格常数（或晶胞体积）测量值计算压力。

测量光谱可从文本文件加载，并在应用程序内直接进行平滑和拟合；参见[光谱与拟合](4-spectra-and-fitting.md)。

![主窗口（红宝石荧光模式）](../assets/cap-zh-Hans-auto/FormMain-ruby.png){width=700px}

## 安装

请从 [GitHub Releases 页面](https://github.com/seto77/PressureCalculator/releases/latest)下载最新版本。

| 文件 | 说明 |
|---|---|
| `PressureCalculator-setup.msi` | **推荐。** 适用于普通 (x64) Windows PC 的安装程序。 |
| `PressureCalculator-setup_arm64.msi` | 适用于 Windows on Arm（Snapdragon PC，或通过虚拟化运行 Windows 的 Apple Silicon Mac 等）的安装程序。 |
| `PressureCalculator-v.X.zip` | 便携版 (x64)：无需安装、自包含。适合没有管理员权限的 PC。 |
| `PressureCalculator-v.X_arm64.zip` | Windows on Arm 便携版。 |

MSI 安装版需要 .NET Desktop Runtime 10；如果尚未安装，首次启动时 Windows 会显示带有下载链接的对话框。便携 ZIP 版已捆绑运行时，无需额外安装：只需将 ZIP 解压到用户可写的文件夹并运行 `PressureCalculator.exe` 即可。

PressureCalculator 按用户安装（无需管理员权限），设置保存在 `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator` 中。

## 显示语言

用户界面支持 11 种语言。在菜单栏中选择 **Language** 并选取语言后，PressureCalculator 会以新语言重新启动。从应用程序中打开本在线手册时，手册也会跟随相同的语言设置。

## 在线帮助

在应用程序中按 ++f1++（或选择 **帮助 → 在线手册**），即可打开与当前模式对应的手册页面。
