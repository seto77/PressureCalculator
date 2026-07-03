# 1. Fluorescência do rubi

A pressão é determinada a partir do deslocamento da linha de fluorescência R1 do rubi, o medidor de pressão mais utilizado em experimentos com célula de bigorna de diamante. Selecione **Fluorescência do rubi** na parte superior da janela principal.

![Modo de fluorescência do rubi](../assets/cap-pt-auto/FormMain-ruby.png){width=700px}

## Fluxo de trabalho

1. Carregue um espectro de fluorescência (consulte [Espectros e ajuste](4-spectra-and-fitting.md)) ou digite o comprimento de onda da linha R1 diretamente na caixa **R1** (em nm).
2. Quando um espectro é carregado, o PressureCalculator ajusta as linhas R1 e R2 dentro do **Intervalo de ajuste** (perfis pseudo-Voigt com fundo linear) e exibe as posições e larguras de pico ajustadas na caixa **Informações do ajuste**.
3. Defina a condição de referência (o comprimento de onda **R1₀** da linha R1 à pressão zero) no grupo **Condição de referência**. **Definir o R1 atual** copia o R1 ajustado no momento para a caixa de referência.
4. A pressão calculada com cada escala de rubi é exibida no grupo **Cálculo da pressão** (em GPa).

## Escalas de rubi

A pressão é calculada como

$$P = \frac{A}{y}\left[\left(\frac{R1}{R1_0}\right)^{y} - 1\right]$$

com os seguintes conjuntos de parâmetros (os coeficientes são editáveis):

| Escala | Observação |
|---|---|
| Mao (1978) | A = 1904 GPa, y = 5 |
| Mao-quasi (1986) | condições quase hidrostáticas, y = 7.665 |
| Mao-hydro (2000) | condições hidrostáticas, y = 7.715 |
| Shen et al. (2020) | escala prática internacional de rubi, P = 1870 × Δ(1 + 5.63 Δ), Δ = (R1 − R1₀)/R1₀ |

## Correção de temperatura

A linha R1 também se desloca com a temperatura. O grupo **Dependência da temperatura (Ragan et al., 1992)** corrige esse efeito usando a temperatura medida e a temperatura de referência; a correção é aplicável na faixa de 50-600 K.

- **Unidade de temperatura** : Kelvin ou Celsius.
- **Igual à referência** : assume que a temperatura de medição é igual à temperatura de referência.
- **Calcular pela equação de Ragan** : calcula R1₀ na temperatura dada pela equação de Ragan, em vez de digitá-lo manualmente.
