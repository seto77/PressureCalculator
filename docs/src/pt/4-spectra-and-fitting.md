# 4. Espectros e ajuste

Esta página descreve como carregar, suavizar e ajustar espectros medidos nos modos de fluorescência do rubi e Raman do diamante.

## Carregando um espectro

- **Arquivo → Carregar** : abre um arquivo de espectro.
- **Arrastar e soltar** : solte um arquivo de espectro sobre a janela principal.

O PressureCalculator lê arquivos de texto simples compostos por colunas numéricas separadas por vírgulas, espaços ou tabulações (o separador é detectado automaticamente). A primeira coluna numérica é usada como eixo horizontal (comprimento de onda em nm ou número de onda em cm⁻¹) e a coluna seguinte, como intensidade. A extensão do arquivo é irrelevante.

## Monitorando o arquivo de dados

Quando **Arquivo → Recarregar o arquivo se for atualizado** está marcado, o PressureCalculator monitora o arquivo carregado e o recarrega automaticamente sempre que ele é modificado. Isso é conveniente durante uma sessão de medição: o espectro, o ajuste e a leitura de pressão são atualizados à medida que novos dados são salvos pelo software do espectrômetro.

## Suavização

O espectro pode ser suavizado antes do ajuste com dois filtros independentes:

- **Média móvel** : largura da janela da média móvel (em pontos de dados).
- **Desfoque gaussiano σ** : largura da suavização gaussiana.

No modo Raman do diamante, o espectro original (gráfico superior) e sua primeira derivada (gráfico inferior) têm configurações de suavização independentes.

## Ajuste

- **Intervalo de ajuste** : largura da região em torno do pico (R1/R2 do rubi) ou do mínimo da derivada (borda Raman do diamante) usada no ajuste, na unidade do eixo (nm ou cm⁻¹).
- A posição do pico ajustada, a largura (FWHM) e os parâmetros de fundo são exibidos na caixa **Informações do ajuste**.
- **Number of decimal places** controla quantas casas decimais são exibidas.

## Gráficos

Arraste um retângulo com o botão esquerdo do mouse para ampliar essa região; um clique simples (um arrasto muito pequeno) reduz o zoom passo a passo. Um clique duplo copia os dados do perfil exibido para a área de transferência como texto. Os indicadores X/Y acima de cada gráfico mostram a posição do ponteiro.

## Exportação

**Arquivo → Exportar como CSV** salva os espectros exibidos (curvas original, suavizada e ajustada) como um arquivo CSV.
