# 4. Spectres et ajustement

Cette page décrit le chargement, le lissage et l'ajustement des spectres mesurés dans les modes fluorescence du rubis et Raman du diamant.

## Chargement d'un spectre

- **Fichier → Charger** : ouvre un fichier de spectre.
- **Glisser-déposer** : déposez un fichier de spectre sur la fenêtre principale.

PressureCalculator lit des fichiers texte constitués de colonnes numériques séparées par des virgules, des espaces ou des tabulations (le séparateur est détecté automatiquement). La première colonne numérique sert d'axe horizontal (longueur d'onde en nm, ou nombre d'onde en cm⁻¹) et la colonne suivante d'intensité. L'extension du fichier est sans importance.

## Surveillance du fichier de données

Lorsque **Fichier → Recharger le fichier s'il est mis à jour** est coché, PressureCalculator surveille le fichier chargé et le recharge automatiquement à chaque modification. C'est pratique pendant une session de mesure : le spectre, l'ajustement et la valeur de pression se rafraîchissent au fur et à mesure que le logiciel du spectromètre enregistre de nouvelles données.

## Lissage

Le spectre peut être lissé avant l'ajustement au moyen de deux filtres indépendants :

- **Moyenne glissante** : largeur de la fenêtre de la moyenne glissante (en points de données).
- **Flou gaussien σ** : largeur du lissage gaussien.

En mode Raman du diamant, le spectre original (graphique supérieur) et sa dérivée première (graphique inférieur) disposent de réglages de lissage distincts.

## Ajustement

- **Plage d'ajustement** : largeur de la région, autour du pic (R1/R2 du rubis) ou du minimum de la dérivée (bord Raman du diamant), utilisée pour l'ajustement, dans l'unité de l'axe (nm ou cm⁻¹).
- La position du pic ajusté, sa largeur (FWHM) et les paramètres de fond sont affichés dans la zone **Informations d'ajustement**.
- **Décimales** contrôle le nombre de décimales affichées.

## Graphiques

Tracez un rectangle avec le bouton gauche de la souris pour zoomer sur cette région ; un simple clic (un très petit déplacement) effectue un zoom arrière pas à pas. Un double-clic copie les données du profil affiché dans le presse-papiers sous forme de texte. Les indicateurs X/Y au-dessus de chaque graphique montrent la position du pointeur.

## Exportation

**Fichier → Exporter en CSV** enregistre les spectres affichés (courbes originale, lissée et ajustée) dans un fichier CSV.
