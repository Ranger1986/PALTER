using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResizeTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int minCoordinates;
    public Vector3Int maxCoordinates;
    void Start()
    {
        Resize();
    }

    void Resize()
    {
        tilemap.origin = minCoordinates;
        tilemap.size = maxCoordinates - minCoordinates;
    }
}



