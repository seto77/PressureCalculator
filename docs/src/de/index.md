# PressureCalculator

PressureCalculator ist eine kostenlose Windows-Anwendung zur Druckbestimmung in Hochdruckexperimenten (z. B. Diamantstempelzellen-Experimenten). Sie unterstützt drei komplementäre Methoden:

- **[Rubin-Fluoreszenz](1-ruby-fluorescence.md)** : Druck aus der Verschiebung der R1-Fluoreszenzlinie des Rubins, mit mehreren publizierten Rubinskalen und einer Temperaturkorrektur.
- **[Diamant-Raman-Kante](2-diamond-raman.md)** : Druck aus der hochfrequenten Kante der Raman-Bande des Diamantstempels.
- **[Zustandsgleichung (EOS)](3-equation-of-states.md)** : Druck aus der gemessenen Gitterkonstante (oder dem Elementarzellenvolumen) von Standardmaterialien wie Gold, Platin, NaCl, Periklas und anderen.

Gemessene Spektren können aus Textdateien geladen, geglättet und direkt in der Anwendung gefittet werden; siehe [Spektren und Fitting](4-spectra-and-fitting.md).

![Hauptfenster (Rubin-Fluoreszenz-Modus)](../assets/cap-de-auto/FormMain-ruby.png){width=700px}

## Installation

Laden Sie die neueste Version von der [GitHub-Releases-Seite](https://github.com/seto77/PressureCalculator/releases/latest) herunter.

| Datei | Beschreibung |
|---|---|
| `PressureCalculator-setup.msi` | **Empfohlen.** Installer für gewöhnliche (x64) Windows-PCs. |
| `PressureCalculator-setup_arm64.msi` | Installer für Windows on Arm (Snapdragon-PCs, Apple-Silicon-Macs mit Windows in einer Virtualisierungsumgebung usw.). |
| `PressureCalculator-v.X.zip` | Portable Version (x64): keine Installation nötig, vollständig eigenständig. Geeignet für PCs, auf denen Sie keine Administratorrechte haben. |
| `PressureCalculator-v.X_arm64.zip` | Portable Version für Windows on Arm. |

Der MSI-Installer benötigt die .NET Desktop Runtime 10; ist sie nicht installiert, zeigt Windows beim ersten Start einen Dialog mit einem Download-Link an. Die portablen ZIP-Pakete enthalten die Runtime bereits, sodass keine separate Installation erforderlich ist: Entpacken Sie das ZIP einfach in einen beschreibbaren Ordner und starten Sie `PressureCalculator.exe`.

PressureCalculator wird pro Benutzer installiert (keine Administratorrechte erforderlich) und speichert seine Einstellungen unter `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`.

## Anzeigesprache

Die Benutzeroberfläche ist in 11 Sprachen verfügbar. Wählen Sie **Language** in der Menüleiste und dann eine Sprache; PressureCalculator startet mit der neuen Sprache neu. Dieses Online-Handbuch folgt derselben Sprachauswahl, wenn es aus der Anwendung heraus geöffnet wird.

## Online-Hilfe

Drücken Sie ++f1++ in der Anwendung (oder wählen Sie **Hilfe → Online-Handbuch**), um die dem aktuellen Modus entsprechende Handbuchseite zu öffnen.
