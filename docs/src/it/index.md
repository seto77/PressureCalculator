# PressureCalculator

PressureCalculator è un'applicazione Windows gratuita per la determinazione della pressione negli esperimenti ad alta pressione (come gli esperimenti con cella a incudini di diamante). Supporta tre metodi complementari:

- **[Fluorescenza del rubino](1-ruby-fluorescence.md)** : pressione dallo spostamento della riga di fluorescenza R1 del rubino, con diverse scale del rubino pubblicate e una correzione di temperatura.
- **[Bordo Raman del diamante](2-diamond-raman.md)** : pressione dal bordo ad alta frequenza della banda Raman dell'incudine di diamante.
- **[Equazione di stato (EOS)](3-equation-of-states.md)** : pressione dalla costante reticolare misurata (o dal volume della cella unitaria) di materiali standard come oro, platino, NaCl, periclasio e altri.

Gli spettri misurati possono essere caricati da file di testo, lisciati e fittati direttamente nell'applicazione; si veda [Spettri e fitting](4-spectra-and-fitting.md).

![Finestra principale (modalità fluorescenza del rubino)](../assets/cap-it-auto/FormMain-ruby.png){width=700px}

## Installazione

Scaricare l'ultima versione dalla [pagina GitHub Releases](https://github.com/seto77/PressureCalculator/releases/latest).

| File | Descrizione |
|---|---|
| `PressureCalculator-setup.msi` | **Consigliato.** Programma di installazione per i normali PC Windows (x64). |
| `PressureCalculator-setup_arm64.msi` | Programma di installazione per Windows on Arm (PC Snapdragon, oppure Mac Apple Silicon che eseguono Windows in virtualizzazione, ecc.). |
| `PressureCalculator-v.X.zip` | Versione portable (x64): nessuna installazione, autosufficiente. Adatta ai PC su cui non si dispone di diritti di amministratore. |
| `PressureCalculator-v.X_arm64.zip` | Versione portable per Windows on Arm. |

Il programma di installazione MSI richiede il .NET Desktop Runtime 10; se non è installato, al primo avvio Windows mostra una finestra di dialogo con un link per il download. I pacchetti ZIP portable includono il runtime, quindi non è necessaria alcuna installazione separata: basta estrarre lo ZIP in una cartella scrivibile dall'utente ed eseguire `PressureCalculator.exe`.

PressureCalculator si installa a livello di utente (non sono richiesti diritti di amministratore) e memorizza le impostazioni in `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`.

## Lingua dell'interfaccia

L'interfaccia utente è disponibile in 11 lingue. Scegliere **Language** nella barra dei menu e selezionare una lingua; PressureCalculator si riavvia con la nuova lingua. Questo manuale online segue la stessa selezione della lingua quando viene aperto dall'applicazione.

## Guida online

Premere ++f1++ (oppure scegliere **Aiuto → Manuale online**) nell'applicazione per aprire la pagina del manuale corrispondente alla modalità corrente.
