# 4. Espectros y ajuste

Esta página describe cómo cargar, suavizar y ajustar los espectros medidos en los modos de fluorescencia del rubí y de Raman del diamante.

## Carga de un espectro

- **Archivo → Cargar** : abre un archivo de espectro.
- **Arrastrar y soltar** : suelte un archivo de espectro sobre la ventana principal.

PressureCalculator lee archivos de texto plano formados por columnas numéricas separadas por comas, espacios o tabuladores (el separador se detecta automáticamente). La primera columna numérica se usa como eje horizontal (longitud de onda en nm, o número de onda en cm⁻¹) y la columna siguiente como intensidad. La extensión del archivo es indiferente.

## Vigilancia del archivo de datos

Cuando **Archivo → Recargar el archivo si se actualiza** está marcado, PressureCalculator vigila el archivo cargado y lo recarga automáticamente cada vez que cambia. Esto resulta práctico durante una sesión de medición: el espectro, el ajuste y la lectura de presión se actualizan a medida que el software del espectrómetro guarda nuevos datos.

## Suavizado

El espectro puede suavizarse antes del ajuste con dos filtros independientes:

- **Promedio móvil** : anchura de la ventana del promedio móvil (en puntos de datos).
- **Desenfoque gaussiano σ** : anchura del suavizado gaussiano.

En el modo Raman del diamante, el espectro original (gráfico superior) y su primera derivada (gráfico inferior) tienen parámetros de suavizado independientes.

## Ajuste

- **Rango de ajuste** : anchura de la región en torno al pico (R1/R2 del rubí) o al mínimo de la derivada (borde Raman del diamante) que se utiliza para el ajuste, en la unidad del eje (nm o cm⁻¹).
- La posición del pico ajustado, su anchura (FWHM) y los parámetros del fondo se muestran en el cuadro **Información del ajuste**.
- **Number of decimal places** controla cuántos decimales se muestran.

## Gráficos

Arrastre un rectángulo con el botón izquierdo del ratón para ampliar esa región; un simple clic (un arrastre muy pequeño) reduce la ampliación paso a paso. Un doble clic copia los datos del perfil mostrado al portapapeles como texto. Los indicadores X/Y situados sobre cada gráfico muestran la posición del puntero.

## Exportación

**Archivo → Exportar como CSV** guarda los espectros mostrados (curvas original, suavizada y ajustada) como archivo CSV.
