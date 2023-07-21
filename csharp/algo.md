```
createRooms(Nombre_Piece, Longueur_cote_minimal, Longueur_cote_maximal)
    For Nombre_Piece
        tant que la piece cree n'est pas valide
            initialiser les 4 angles de la Nombre_Piece
            la piece est valide si la piece n'ecrase pas une autre piece 
        cree un objet piece avec ces 4 angles
        ajoute la piece a la liste des pieces
        mettre a jour la grille pour indiqué les mur et l'interieur de la piece
``` 
```
createDoors(nombre de porte)
    For toute les pieces
        For le nombre de porte
            tant que la porte n'est pas valide
                prent une position alleatoire sur un des murs de la piece
                la porte est valide si elle n'est ni sur une autre porte, ni su un angle 
            met a jour la grille en ajoutant la porte
            cree l'objet porte
            ajoute la porte a la liste des portes
```

```
cleanDoorWall()
    For parcourire les porte de la liste
        pop la premiere porte de la liste
        SI la porte es horizontal 
            met a jour la grille comme porte horizontal
            Si la porte n'est pas sur le bord gauche de la grille
                Si la gauche de la porte est un mur
                    met a jour la grille pour changer le mur en sol
                    cree une nouvelle porte dans la 2 eme piece lie a la prmiere porte au meme coordonnee 
                    ajoute les deux portes a la liste des porte entre les murs
                Si la gauche de la porte est un angle
                    Si le haut de la porte est un mur
                        deplacer la porte au dessus
                        remettre la porte au debut de la liste des portes
                    Sinon le bas de la porte est un mur
                        deplacer la porte au dessous
                        remettre la porte au debut de la liste des portes
                    Sinon
                        suprime la porte
                Si la gauche de la porte est une porte
                    pop la deuxieme porte de la liste des porte
                    lie les deux porte enssemble
                    deplace la premiere porte au coordonee de la deuxieme
                    ajoute les deux porte a la liste des portes entre les murs
            Si la porte n'est pas sur le bord droit de la grille
                Si la droite de la porte est un mur
                    met a jour la grille pour changer le mur en sol
                    cree une nouvelle porte dans la 2 eme piece lie a la prmiere porte au meme coordonnee 
                    ajoute les deux portes a la liste des porte entre les murs
                Si la droite de la porte est un angle
                    Si le haut de la porte est un mur
                        deplacer la porte au dessus
                        remettre la porte au debut de la liste des portes
                    Sinon le bas de la porte est un mur
                        deplacer la porte au dessous
                        remettre la porte au debut de la liste des portes
                    Sinon
                        suprime la porte
                Si la droite de la porte est une porte
                    pop la deuxieme porte de la liste des portes
                    lie les deux portes enssemble
                    deplace la premiere porte au coordonee de la deuxieme
                    ajoute les deux portes a la liste des portes entre les murs
            Si la porte a du sol de chaque coté 
                cree une nouvelle porte pour la piece en face
                relie la nouvelle porte avec la premiere porte
                rajoute les deux portes a la liste des porte entre les murs
        Sinon 
            met a jour la grille comme porte horizontal  
            Si la porte n'est pas sur le bord haut de la grille
                Si le haut de la porte est un mur
                    met a jour la grille pour changer le mur en sol
                    cree une nouvelle porte dans la 2 eme piece lie a la prmiere porte au meme coordonnee 
                    ajoute les deux portes a la liste des porte entre les murs
                Si le haut de la porte est un angle
                    Si la droite de la porte est un mur
                        deplacer la porte a droite
                        remettre la porte au debut de la liste des portes
                    Sinon la gauche de la porte est un mur
                        deplacer la porte a gauche
                        remettre la porte au debut de la liste des portes
                    Sinon
                        suprime la porte
                Si le haut de la porte est une porte
                    pop la deuxieme porte de la liste des porte
                    lie les deux porte enssemble
                    deplace la premiere porte au coordonee de la deuxieme
                    ajoute les deux porte a la liste des portes entre les murs
            Si la porte n'est pas sur le bord bas de la grille
                Si le bas de la porte est un mur
                    met a jour la grille pour changer le mur en sol
                    cree une nouvelle porte dans la 2 eme piece lie a la prmiere porte au meme coordonnee 
                    ajoute les deux portes a la liste des porte entre les murs
                Si le bas de la porte est un angle
                    Si la droite de la porte est un mur
                        deplacer la porte a droite
                        remettre la porte au debut de la liste des portes
                    Sinon la gauche de la porte est un mur
                        deplacer la porte a gauche
                        remettre la porte au debut de la liste des portes
                    Sinon
                        suprime la porte
                Si le bas de la porte est une porte
                    pop la deuxieme porte de la liste des porte
                    lie les deux porte enssemble
                    deplace la premiere porte au coordonee de la deuxieme
                    ajoute les deux porte a la liste des portes entre les murs
            Si la porte a du sol de chaque coté 
                cree une nouvelle porte pour la piece en face
                relie la nouvelle porte avec la premiere porte
                rajoute les deux portes a la liste des porte entre les murs
```

```
buildcorridors()
    Si il n'y a que une porte dans la liste des portes
        suprime la porte
        RETURN
    initialise une nouvelle grille ou le void et les couloires sont a 0, sinon a -1
    Tant Que il y a plus de 1 porte dans la liste des portes
        pop la premiere des porte de la liste des portes
        prend une deuxieme porte de la liste des portes ou des porte deja lié
        cree la route entre les deux portes (corridorV2)
        ajouter les deux portes a la liste des portes liees
        metre a jour la grille 
    si il reste une dernier porte seul dans la liste des portes
        selectionné une porte deja liee au hazard
        cree la route entre les deux portes (corridorV2)
        ajouter les deux portes a la liste des portes liees
        metre a jour la grille
```

```
corridorV2(start_coor_X, start_coor_Y, direction_Hori_Vert, arrivee_coor_X, arrivee_coor_Y, grille)
    initialise une nouvelle grille d'element
    met la grille au coordonnee de depart et d'arrivee a la valleur de 0
    note la grille d'element au coordonnee de depart comme vidité
    cree la liste des element a visité avec l'emement au coordonnee de depart comme seul element
    initialisation de liste de valeur drift pour visiter tout les voisins d'un point
    TANT que la liste des point a visiter n'est pas null
        pop le premiere element
        FOR tout les elements de la liste drift
            SI coor voisin dans la grille
                SI voisin deja visite
                    Si pas de changement de direction 
                        SI le voisin est deja un couloire
                            diminuer le cout du chemin
                        SI le voisin est un chemin plus interesant
                            definir le voisin comment nouveau procain saut 
                            ajouter le voisin dans la lste des element a voir
                    SINON
                        SI le chemin n'est pas un couloire
                            augmenter le cout du chemin
                        SI nouveau chemin
                            definir le voisin comment nouveau procain saut 
                            ajouter le voisin dans la lste des element a voir
                SINON
                    SI pas de changement de direction
                        donner un poid faible a la route
                    SINON
                        donner un poid fort a la route
                    ajouter la route au point a voir
    faire la liste de la route en partant du point d'arrivee et en remontant au point de depart
```