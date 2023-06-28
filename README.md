# theDungeonRebirths
rebirths of a old abandoned prototype that I want remake and finish

## map generation
prototype de génération de la map par code python

```
Cree X salle de regtangle aléatoire
Cree Y porte par salle
SI porte ne relie pas deja 2 salles 
    ALORS rajouter a la liste des portes a relier
TANT QUE la liste n'est pas vide
    (prend 2 porte de la liste des portes non relier)|(prend une porte non relier et une autre porte au hazard)
    relie ces deux portes par algo PATH
```

### Algo PATH

cette lago de pathfinding doit insiter les long couloire plutot que les chemin les plus court

```
TANT QUE liste a treter non vide
    pop le premier élément
    POUR tous ses voisins
        Si non visité
        ALORS 
            les definir comme visité
            attribué leur parent au point courrent
            SI le voisin induit un virage 
            ALORS
                attribuer comment poid le poid du courrent + 4
            SINON
                attribuer comment poid le poid du courrent + 1
            ajouter voisin a la liste a traiter
        SINON
            SI le voisin induit un virage 
            ALORS
                SI pondération voisin plus grand que courrent + 4
                ALORS 
                    attribué leur parent au point courrent
                    attribuer comment poid le poid du courrent + 4
                    ajouter voisin a la liste a traiter
            SINON 
                SI pondération voisin plus grand que courrent + 1
                ALORS 
                    attribué leur parent au point courrent
                    attribuer comment poid le poid du courrent + 1
                    ajouter voisin a la liste a traiter
remonter les parents du point d'arriver pour recréer le chemin
```