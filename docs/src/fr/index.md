# PressureCalculator

PressureCalculator est une application Windows gratuite pour la détermination de la pression dans les expériences sous haute pression (telles que les expériences en cellule à enclumes de diamant). Elle prend en charge trois méthodes complémentaires :

- **[Fluorescence du rubis](1-ruby-fluorescence.md)** : pression déduite du déplacement de la raie de fluorescence R1 du rubis, avec plusieurs échelles rubis publiées et une correction en température.
- **[Bord Raman du diamant](2-diamond-raman.md)** : pression déduite du bord haute fréquence de la bande Raman de l'enclume en diamant.
- **[Équation d'état (EOS)](3-equation-of-states.md)** : pression déduite du paramètre de maille (ou du volume de la maille) mesuré de matériaux étalons tels que l'or, le platine, le NaCl, la périclase, etc.

Les spectres mesurés peuvent être chargés à partir de fichiers texte, lissés et ajustés directement dans l'application ; voir [Spectres et ajustement](4-spectra-and-fitting.md).

![Fenêtre principale (mode fluorescence du rubis)](../assets/cap-fr-auto/FormMain-ruby.png){width=700px}

## Installation

Téléchargez la dernière version depuis la [page GitHub Releases](https://github.com/seto77/PressureCalculator/releases/latest).

| Fichier | Description |
|---|---|
| `PressureCalculator-setup.msi` | **Recommandé.** Programme d'installation pour les PC Windows ordinaires (x64). |
| `PressureCalculator-setup_arm64.msi` | Programme d'installation pour Windows on Arm (PC Snapdragon, Mac Apple Silicon exécutant Windows en virtualisation, etc.). |
| `PressureCalculator-v.X.zip` | Version portable (x64) : sans installation, autonome. Convient aux PC où vous ne disposez pas de droits d'administrateur. |
| `PressureCalculator-v.X_arm64.zip` | Version portable pour Windows on Arm. |

Le programme d'installation MSI nécessite le .NET Desktop Runtime 10 ; s'il n'est pas installé, Windows affiche au premier lancement une boîte de dialogue contenant un lien de téléchargement. Les archives ZIP portables embarquent le runtime, aucune installation séparée n'est donc nécessaire : il suffit d'extraire le ZIP dans un dossier accessible en écriture et de lancer `PressureCalculator.exe`.

PressureCalculator s'installe pour l'utilisateur courant (aucun droit d'administrateur requis) et enregistre ses paramètres sous `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`.

## Langue d'affichage

L'interface utilisateur est disponible en 11 langues. Choisissez **Language** dans la barre de menus et sélectionnez une langue ; PressureCalculator redémarre alors dans la nouvelle langue. Lorsqu'il est ouvert depuis l'application, ce manuel en ligne suit la même sélection de langue.

## Aide en ligne

Appuyez sur ++f1++ (ou choisissez **Aide → Manuel en ligne**) dans l'application pour ouvrir la page du manuel correspondant au mode en cours.
