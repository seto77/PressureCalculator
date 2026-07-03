# 4. Spectra and fitting

This page describes how to load, smooth, and fit measured spectra in the ruby fluorescence and diamond Raman modes.

## Loading a spectrum

- **File → Load** : open a spectrum file.
- **Drag & drop** : drop a spectrum file onto the main window.

PressureCalculator reads plain-text files consisting of numeric columns separated by commas, spaces, or tabs (the separator is detected automatically). The first numeric column is used as the horizontal axis (wavelength in nm, or wavenumber in cm⁻¹) and the following column as the intensity. The file extension does not matter.

## Watching the data file

When **File → Reload the file if it is updated** is checked, PressureCalculator monitors the loaded file and reloads it automatically whenever the file changes. This is convenient during a measurement session: the spectrum, fit, and pressure reading refresh as new data are saved by the spectrometer software.

## Smoothing

The spectrum can be smoothed before fitting with two independent filters:

- **Running Average** : moving-average window width (in data points).
- **Gaussian blur σ** : Gaussian smoothing width.

In diamond Raman mode, the original spectrum (upper graph) and its first derivative (lower graph) have separate smoothing settings.

## Fitting

- **Fitting Range** : width of the region around the peak (ruby R1/R2) or the derivative minimum (diamond Raman edge) used for fitting, in the axis unit (nm or cm⁻¹).
- The fitted peak position, width (FWHM), and background parameters are displayed in the **Fitting Information** box.
- **Number of decimal places** controls how many decimal places are displayed.

## Graphs

Drag a rectangle with the left mouse button to zoom into that region; a simple click (a very small drag) zooms out step by step. Double-clicking copies the displayed profile data to the clipboard as text. The X/Y readouts above each graph show the pointer position.

## Exporting

**File → Export as CSV** saves the displayed spectra (original, smoothed, and fitted curves) as a CSV file.
