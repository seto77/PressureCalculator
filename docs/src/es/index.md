# PressureCalculator

PressureCalculator es una aplicación gratuita para Windows destinada a determinar la presión en experimentos de alta presión (como los experimentos con celda de yunques de diamante). Admite tres métodos complementarios:

- **[Fluorescencia del rubí](1-ruby-fluorescence.md)** : presión a partir del desplazamiento de la línea de fluorescencia R1 del rubí, con varias escalas de rubí publicadas y una corrección de temperatura.
- **[Borde Raman del diamante](2-diamond-raman.md)** : presión a partir del borde de alta frecuencia de la banda Raman del yunque de diamante.
- **[Ecuación de estado (EOS)](3-equation-of-states.md)** : presión a partir de la constante de red (o del volumen de la celda unidad) medida de materiales estándar como el oro, el platino, el NaCl, la periclasa y otros.

Los espectros medidos pueden cargarse desde archivos de texto, suavizarse y ajustarse directamente en la aplicación; véase [Espectros y ajuste](4-spectra-and-fitting.md).

![Ventana principal (modo de fluorescencia del rubí)](../assets/cap-es-auto/FormMain-ruby.png){width=700px}

## Instalación

Descargue la versión más reciente desde la [página de Releases de GitHub](https://github.com/seto77/PressureCalculator/releases/latest).

| Archivo | Descripción |
|---|---|
| `PressureCalculator-setup.msi` | **Recomendado.** Instalador para PC con Windows convencionales (x64). |
| `PressureCalculator-setup_arm64.msi` | Instalador para Windows on Arm (PC con Snapdragon, Mac con Apple Silicon que ejecutan Windows mediante virtualización, etc.). |
| `PressureCalculator-v.X.zip` | Versión portátil (x64): sin instalación, autocontenida. Adecuada para PC en los que no dispone de derechos de administrador. |
| `PressureCalculator-v.X_arm64.zip` | Versión portátil para Windows on Arm. |

El instalador MSI requiere el .NET Desktop Runtime 10; si no está instalado, Windows muestra en el primer arranque un cuadro de diálogo con un enlace de descarga. Los paquetes ZIP portátiles incluyen el runtime, por lo que no se necesita ninguna instalación adicional: basta con extraer el ZIP en una carpeta con permisos de escritura y ejecutar `PressureCalculator.exe`.

PressureCalculator se instala por usuario (sin necesidad de derechos de administrador) y guarda su configuración en `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`.

## Idioma de la interfaz

La interfaz de usuario está disponible en 11 idiomas. Elija **Language** en la barra de menús y seleccione un idioma; PressureCalculator se reiniciará con el nuevo idioma. Cuando se abre desde la aplicación, este manual en línea sigue la misma selección de idioma.

## Ayuda en línea

Pulse ++f1++ (o elija **Ayuda → Manual en línea**) en la aplicación para abrir la página del manual correspondiente al modo actual.
