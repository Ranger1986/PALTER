using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] gameObjectsToPlace;
    public int numObjectsToPlace;

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        for (int i = 0; i < numObjectsToPlace; i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(tilemap.cellBounds.min.x, tilemap.cellBounds.max.x),
                                                      Random.Range(tilemap.cellBounds.min.y, tilemap.cellBounds.max.y),
                                                      0);
            TileBase tile = tilemap.GetTile(randomPosition);
            if (tile != null) // Check if the tile exists at the random position
            {
                int randomIndex = Random.Range(0, gameObjectsToPlace.Length);
                GameObject gameObjectToPlace = gameObjectsToPlace[randomIndex];
                Instantiate(gameObjectToPlace, tilemap.CellToWorld(randomPosition), Quaternion.identity);
            }
        }
    }
}
