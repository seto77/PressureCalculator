PressureCalculator portable ZIP package (260703Cl)
===================================================

The MSI installer is the recommended installation method for PressureCalculator.
This portable ZIP package is provided as an alternative for managed Windows
PCs where MSI installation, administrator approval, or separate .NET Desktop
Runtime installation is difficult. In this document, "portable" means
"no installer required".

How to run
----------

1. Extract the ZIP file to a user-writable folder.
   Example: Documents\PressureCalculator or Desktop\PressureCalculator

2. Run PressureCalculator.exe from the extracted PressureCalculator folder.

3. Do not run PressureCalculator.exe directly from inside the ZIP viewer.
   Extract the full folder first so that the bundled DLLs and localization
   files remain next to PressureCalculator.exe.

Runtime
-------

This portable package is self-contained for Windows x64 (and a separate
arm64 package for Windows on Arm). A separate .NET Desktop Runtime 10
installation is not required; the required .NET runtime files are bundled in
this folder. When Microsoft releases .NET runtime security updates, this
package should be rebuilt and redistributed so that the bundled runtime is
also updated.

Notes for managed PCs
---------------------

- Administrator privileges are not required by PressureCalculator itself.
- PressureCalculator stores per-user options under HKCU
  (HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator).
- Windows Defender SmartScreen or institutional security software may still
  warn about newly downloaded unsigned research software. Download
  PressureCalculator only from the official GitHub Releases page:
  https://github.com/seto77/PressureCalculator/releases/latest

Verification
------------

If SHA256SUMS.txt is provided with the release, you can verify the downloaded
ZIP file in PowerShell:

  Get-FileHash .\PressureCalculator-*.zip -Algorithm SHA256
