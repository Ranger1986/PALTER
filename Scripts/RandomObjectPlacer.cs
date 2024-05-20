using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomObjectPlacer : MonoBehaviour
{
    public prefabBag prefabListEnnemies;
    public prefabBag prefabListObjects;
    public int numberOfObjects = 10;
    public Tilemap tilemap;

    void Start()
    {
        PlaceRandomObjects();
    }

    // Détruit des obstacles jusqu'à ce que le chemin soit accecible
    void makeRandomChemin(Pathfinding map, int tailleMap, List<GameObject> objectsList, List<Vector3> positionObjectsList, int startX, int startY, int endX, int endY)
    {
        List<PathNode> chemin = map.FindPath(startX, startY, endX, endY);
        while (chemin == null)
        {
            int blockDestroyed = 0;
            while (blockDestroyed < 3)
            {
                int x = Random.Range(0, tailleMap);
                int y = Random.Range(0, tailleMap);
                //Debug.Log("Le node vérifié est le node : " + "("+x+","+y+")");
                if (!map.grid[x, y].isWalkable)
                {
                    //Debug.Log("Le node détruit est le node : " + "("+x+","+y+")");
                    map.grid[x, y].isWalkable = !map.grid[x, y].isWalkable;
                    for (int i = 0; i < objectsList.Count; i++)
                    {
                        if ((int)positionObjectsList[i].x == x && (int)positionObjectsList[i].y == y)
                        {
                            objectsList.RemoveAt(i);
                            positionObjectsList.RemoveAt(i);
                        }
                            
                    }
                    
                    blockDestroyed++;
                }
            }
            chemin = map.FindPath(startX, startY, endX, endY);
        }
    }

    void PlaceRandomObjects()
    {
        BoundsInt bounds = tilemap.cellBounds;
        // Largeur de la Tilemap
        int widthTilemap = bounds.size.x;
        // Hauteur de la Tilemap
        int heightTilemap = bounds.size.y;

        List<Vector3Int> availableCells = new List<Vector3Int>();

        // Données pour l'A* (vérification de chemin)
        List<GameObject> objectsList = new List<GameObject>(); // Liste des gameObjects (Objets)
        List<Vector3> positionObjectsList = new List<Vector3>(); // position des gameObjects

        // Recherche des cellules disponibles
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(cell) == null) // Vérifie si la case est libre
                {
                    availableCells.Add(cell);
                }
            }
        }

        
        // Charger le GameObject depuis le dossier Resources/GameObject
        GameObject player = Resources.Load<GameObject>("GameObject/Player");

        // Vérifier si le chargement a réussi
        if (player == null)
        {
            Debug.LogError("Impossible le joueur depuis le dossier Resources/GameObject.");
        }

        // Placer le joueur dans la tilemap
        Vector3 playerPosition = tilemap.CellToWorld(availableCells[247]);
        //Instantiate(player, playerPosition, Quaternion.identity);
        availableCells.RemoveAt(247);


        // Charger la porte
        GameObject door = Resources.Load<GameObject>("GameObject/Porte");

        // Vérifier si le chargement a réussi
        if (door == null)
        {
            Debug.LogError("Impossible de charger la porte depuis le dossier Resources/GameObject.");
        }

        // Placer la porte dans la tilemap
        Vector3 doorPosition = tilemap.CellToWorld(availableCells[240]);
        doorPosition.y += 1f; // Déplacer la porte d'une unité vers le haut
        //Instantiate(door, doorPosition, Quaternion.identity);
        availableCells.RemoveAt(240);

        // Place les objets
        for (int i = 0; i < numberOfObjects; i++)
        {
            if (availableCells.Count == 0)
            {
                Debug.LogWarning("Toutes les cases sont occupées !");
                return;
            }

            int randomIndex = Random.Range(0, availableCells.Count);
            Vector3Int randomCell = availableCells[randomIndex];
            Vector3 randomPosition = tilemap.CellToWorld(randomCell);

            GameObject objectToPlace;
            float randomValue = Random.value;
            if (randomValue < 0.7f) // Supposons que les objets ont une probabilité de 70% et les ennemis 30%
            {
                objectToPlace = prefabListObjects.getRandomPrefab();
                objectsList.Add(objectToPlace);

                positionObjectsList.Add(randomPosition);
            }
            else
            {
                objectToPlace = prefabListEnnemies.getRandomPrefab();
                Instantiate(objectToPlace, randomPosition, Quaternion.identity);
            }

            
            availableCells.RemoveAt(randomIndex);
        }

        //Vérifier si le chemin est faisable
        Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);
        int playerGridX = playerCellPosition.x;
        int playerGridY = playerCellPosition.y;

        Vector3Int doorCellPosition = tilemap.WorldToCell(doorPosition);
        int doorGridX = doorCellPosition.x;
        int doorGridY = doorCellPosition.y;
        
        Pathfinding map = new Pathfinding(widthTilemap, heightTilemap, true);
        int tailleMap = widthTilemap * heightTilemap;

        for (int i = 0; i < positionObjectsList.Count; i++)
        {
            Debug.Log(positionObjectsList[i]);
            Vector3Int cellPosition = tilemap.WorldToCell(positionObjectsList[i]);
            int cellX = cellPosition.x;
            int cellY = cellPosition.y;
            Debug.Log(cellX);
            Debug.Log(cellY);
            map.grid[cellX, cellY].isWalkable = false;
        }

        makeRandomChemin(map, tailleMap, objectsList, positionObjectsList, playerGridX, playerGridY, doorGridX, doorGridY);

        //Instancier les objets
        for (int i = 0; i < objectsList.Count; i++)
        {
            Instantiate(objectsList[i], positionObjectsList[i], Quaternion.identity);
        }
    }
}
