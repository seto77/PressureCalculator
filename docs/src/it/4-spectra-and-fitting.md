# 4. Spettri e fitting

Questa pagina descrive come caricare, lisciare e fittare gli spettri misurati nelle modalità fluorescenza del rubino e Raman del diamante.

## Caricamento di uno spettro

- **File → Carica** : apre un file di spettro.
- **Trascinamento (drag & drop)** : trascinare un file di spettro sulla finestra principale.

PressureCalculator legge file di testo semplice composti da colonne numeriche separate da virgole, spazi o tabulazioni (il separatore viene rilevato automaticamente). La prima colonna numerica è usata come asse orizzontale (lunghezza d'onda in nm, oppure numero d'onda in cm⁻¹) e la colonna successiva come intensità. L'estensione del file è irrilevante.

## Monitoraggio del file di dati

Quando **File → Ricarica il file se viene aggiornato** è selezionato, PressureCalculator monitora il file caricato e lo ricarica automaticamente ogni volta che il file cambia. È comodo durante una sessione di misura: lo spettro, il fit e la lettura della pressione si aggiornano man mano che il software dello spettrometro salva nuovi dati.

## Lisciamento

Prima del fitting lo spettro può essere lisciato con due filtri indipendenti:

- **Media mobile** : larghezza della finestra della media mobile (in punti dati).
- **Sfocatura gaussiana σ** : larghezza del lisciamento gaussiano.

Nella modalità Raman del diamante, lo spettro originale (grafico superiore) e la sua derivata prima (grafico inferiore) hanno impostazioni di lisciamento separate.

## Fitting

- **Intervallo di fitting** : larghezza della regione attorno al picco (R1/R2 del rubino) o al minimo della derivata (bordo Raman del diamante) utilizzata per il fitting, nell'unità dell'asse (nm o cm⁻¹).
- La posizione del picco fittato, la larghezza (FWHM) e i parametri del fondo sono visualizzati nel riquadro **Informazioni sul fitting**.
- **Number of decimal places** controlla quante cifre decimali vengono visualizzate.

## Grafici

Trascinare un rettangolo con il pulsante sinistro del mouse per ingrandire quella regione; un semplice clic (un trascinamento molto piccolo) riduce lo zoom passo dopo passo. Un doppio clic copia negli appunti, come testo, i dati del profilo visualizzato. Gli indicatori X/Y sopra ciascun grafico mostrano la posizione del puntatore.

## Esportazione

**File → Esporta come CSV** salva gli spettri visualizzati (curve originale, lisciata e fittata) come file CSV.
