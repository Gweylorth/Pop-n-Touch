Le Proto1 est organis� de la mani�re suivante :

 - Dans le dossier "View", les deux �lements graphiques principaux en XAML et C# (rappel pour afficher le C#, d�rouler le XAML en cliquant sur la petite fl�che � sa gauche dans l'explorateur de solutions):
		-> "SurfaceWindow1" qui d�finit la vue principale ainsi que l'enchainement des diff�rents �crans
		-> "Tab" qui d�finit la vue d�pla�able d'un seul joueur
		
 - Dans le dossier "ViewModel", l'affichage et l'animation d'une seule note de musique.
 
 - Dans le dossier "Model", la partie qui reprend le plus du code de l'an dernier, l� on s'occupe de la logique de la lecture de la musique
		-> "Melody" qui poss�de la liste des notes (classe "Note" et "NotesView pour la repr�sentation) � jouer et avec quel instrument (class "Instrument")
		-> "AudioController", tr�s d�pendant de ce qui a �t� fait l'an dernier, lit ce qui est dans les banque de sons (utilise XNA Audio)

 - Le dossier "Resources" enfin contient uniquement les diff�rentes textures et les banques de sons lue par XNA


Tout �a est un mix (relativement propre au final) entre le projet de l'an dernier et ma petite sauce � moi... Mais la route est encore longue ;).