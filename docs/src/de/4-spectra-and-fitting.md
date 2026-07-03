# 4. Spektren und Fitting

Diese Seite beschreibt, wie gemessene Spektren im Rubin-Fluoreszenz- und im Diamant-Raman-Modus geladen, geglättet und gefittet werden.

## Laden eines Spektrums

- **Datei → Laden** : eine Spektrendatei öffnen.
- **Drag & Drop** : eine Spektrendatei auf das Hauptfenster ziehen.

PressureCalculator liest reine Textdateien mit numerischen Spalten, die durch Kommas, Leerzeichen oder Tabulatoren getrennt sind (das Trennzeichen wird automatisch erkannt). Die erste numerische Spalte wird als horizontale Achse verwendet (Wellenlänge in nm oder Wellenzahl in cm⁻¹), die darauf folgende Spalte als Intensität. Die Dateiendung spielt keine Rolle.

## Überwachen der Datendatei

Wenn **Datei → Datei bei Aktualisierung neu laden** aktiviert ist, überwacht PressureCalculator die geladene Datei und lädt sie automatisch neu, sobald sich die Datei ändert. Das ist während einer Messkampagne praktisch: Spektrum, Fit und Druckanzeige aktualisieren sich, sobald die Spektrometer-Software neue Daten speichert.

## Glättung

Das Spektrum kann vor dem Fitten mit zwei unabhängigen Filtern geglättet werden:

- **Gleitender Mittelwert** : Fensterbreite des gleitenden Mittelwerts (in Datenpunkten).
- **Gaußsche Unschärfe σ** : Breite der Gauß-Glättung.

Im Diamant-Raman-Modus haben das Originalspektrum (oberer Graph) und seine erste Ableitung (unterer Graph) getrennte Glättungseinstellungen.

## Fitting

- **Fitting-Bereich** : Breite des Bereichs um den Peak (Rubin R1/R2) bzw. das Minimum der Ableitung (Diamant-Raman-Kante), der für den Fit verwendet wird, in der Einheit der Achse (nm oder cm⁻¹).
- Die gefittete Peakposition, die Breite (FWHM) und die Untergrundparameter werden im Feld **Fitting-Informationen** angezeigt.
- **Number of decimal places** legt fest, wie viele Nachkommastellen angezeigt werden.

## Graphen

Ziehen Sie mit der linken Maustaste ein Rechteck auf, um in diesen Bereich hineinzuzoomen; ein einfacher Klick (ein sehr kleines Ziehen) zoomt schrittweise heraus. Ein Doppelklick kopiert die angezeigten Profildaten als Text in die Zwischenablage. Die X/Y-Anzeigen über jedem Graphen zeigen die Position des Mauszeigers an.

## Exportieren

**Datei → Als CSV exportieren** speichert die angezeigten Spektren (Original-, geglättete und gefittete Kurven) als CSV-Datei.
