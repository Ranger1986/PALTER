using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using UnityEngine;

public class ItemGeneratorScript : MonoBehaviour
{
    [SerializeField] prefabBag obstacleBag;
    [SerializeField] prefabBag ennemyBag;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject montreChemin;
    [SerializeField] bool faireChemin;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.nbEnnemy = 0;

        // Random seeds
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        //UnityEngine.Random.InitState(0);
        //UnityEngine.Random.InitState(1);
        //UnityEngine.Random.InitState(2);

        // Ground information extraction
        Bounds meshBounds = ground.GetComponent<MeshFilter>().mesh.bounds;
        Vector3 meshLimit = meshBounds.max-meshBounds.min;
        Vector3 groundLowestPoint = new Vector3(meshBounds.min.x*ground.transform.localScale.x,meshBounds.min.y*ground.transform.localScale.y,meshBounds.min.z*ground.transform.localScale.z);
        int width = (int)Math.Floor(meshLimit.x*ground.transform.localScale.x);
        int height = (int)Math.Floor(meshLimit.z*ground.transform.localScale.z);
        
        // Mise en place du PathFinding
        GameObject[,] objectMap = new GameObject[width, height];
        Pathfinding map = new Pathfinding(width, height, true);
        List<Vector2Int> noSpawnPosition = new List<Vector2Int> {};
        
        GameObject door = Resources.Load<GameObject>("GameObject/Porte");
        if (door == null)
        {
            Debug.LogError("Impossible de charger la porte depuis le dossier Resources/GameObject.");
        }
        Vector2Int doorPosition = new Vector2Int(width/2, height-1);

        GameObject player = Resources.Load<GameObject>("GameObject/Player");
        if (door == null)
        {
            Debug.LogError("Impossible de charger la porte depuis le dossier Resources/GameObject.");
        }
        Vector2Int playerPosition = new Vector2Int(width/2, 0);

        // Prevision de la creation des obstacles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = obstacleBag.getRandomPrefab();
                // Vérifie que l'objet existe et vérifie que le point actuel est un point où on ne peut pas faire apparaitre d'objets
                if (obj && !noSpawnPosition.Contains(new Vector2Int(x, y)))
                {
                    objectMap[x, y] = obj;
                    map.grid[x, y].isWalkable = false;
                }
            }
        }

        // Force position de porte
        objectMap[doorPosition.x, doorPosition.y] = door;
        map.grid[doorPosition.x, doorPosition.y].isWalkable = true;

        objectMap[playerPosition.x, playerPosition.y] = player;
        map.grid[playerPosition.x, playerPosition.y].isWalkable = true;

        // Verification d'un chemin et creation d'un chemin au besoin
        if (faireChemin)
        {
            //makePathOneAstar(map, objectMap, playerPosition, doorPosition, groundLowestPoint);
            //applyPredefinedChemin(map,objectMap);
            makeRandomChemin(map, objectMap, playerPosition, doorPosition, groundLowestPoint);
            //makeRandomCheminAlt(map, objectMap); // Ne marche pas
        }

        // Instanciation des obstacles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isInBound=x>0 && y>0 && x< width-2 && y< height-2 ;
                bool aVoisin=isInBound && !objectMap[x+1, y]&&!objectMap[x-1, y]&&!objectMap[x, y+1]&&!objectMap[x, y-1];
                GameObject ennemy = ennemyBag.getRandomPrefab();
                if(!objectMap[x, y] && ennemy && (GlobalVariables.nbEnnemy < 10))
                {
                    objectMap[x, y] = ennemy;
                    GlobalVariables.nbEnnemy++;
                }
                if (objectMap[x, y] && !aVoisin)
                {
                    Instantiate(objectMap[x, y], new Vector3(x + 0.5f, objectMap[x, y].transform.position.y, y + 0.5f) + groundLowestPoint, Quaternion.identity);
                }
            }
        }
    }
    void makePathOneAstar(Pathfinding map, GameObject[,] objectMap, Vector2Int A,Vector2Int B, Vector3 offset)
    {
        List<PathNode> chemin = map.FindPath(A.x, A.y, B.x, B.y);
        if (chemin == null)
        {
            map.withObstacles = false;
            chemin = map.FindPath(A.x, A.y, B.x, B.y);
            foreach (PathNode node in chemin)
            {
                objectMap[node.x, node.y] = null;
            }
        }
        if (montreChemin != null)
        {
            foreach (PathNode node in chemin)
            {
                Debug.Log(node);
                Instantiate(montreChemin, new Vector3(node.x + 0.5f, montreChemin.transform.position.y, node.y + 0.5f) + offset, Quaternion.identity);
            }
        }
    }

    void applyPredefinedChemin(Pathfinding map, GameObject[,] objectMap)
    {
        for (int i = 0; i < 10; i++)
        {
            map.grid[i, 0].isWalkable = true;
            map.grid[i, 9].isWalkable = true;
            map.grid[0, i].isWalkable = true;
            map.grid[9, i].isWalkable = true;
            objectMap[i, 0] = null;
            objectMap[i, 9] = null;
            objectMap[0, i] = null;
            objectMap[9, i] = null;
        }
        List<PathNode> chemin = map.FindPath(0, 5, 9, 5);
        if (montreChemin != null)
        {
            foreach (PathNode node in chemin)
            {
                //Debug.Log(node);
                Instantiate(montreChemin, new Vector3(node.x + 0.5f, montreChemin.transform.position.y, node.y + 0.5f), Quaternion.identity);
            }
        }
    }

    // Détruit des obstacles jusqu'à ce que le chemin soit accecible
    void makeRandomChemin(Pathfinding map, GameObject[,] objectMap, Vector2Int A,Vector2Int B, Vector3 offset)
    {
        List<PathNode> chemin = map.FindPath(A.x, A.y, B.x, B.y);
        while (chemin == null)
        {
            int blockDestroyed = 0;
            while (blockDestroyed < 3)
            {
                int taillemap = (int)Math.Sqrt(objectMap.Length);
                int x = UnityEngine.Random.Range(0, taillemap);
                int y = UnityEngine.Random.Range(0, taillemap);
                //Debug.Log("Le node vérifié est le node : " + "("+x+","+y+")");
                if (!map.grid[x, y].isWalkable)
                {
                    //Debug.Log("Le node détruit est le node : " + "("+x+","+y+")");
                    map.grid[x, y].isWalkable = !map.grid[x, y].isWalkable;
                    objectMap[x, y] = null;
                    blockDestroyed++;
                }
            }
            chemin = map.FindPath(A.x, A.y, B.x, B.y);
        }
        if (montreChemin != null)
        {
            foreach (PathNode node in chemin)
            {
                Debug.Log(node);
                Instantiate(montreChemin, new Vector3(node.x + 0.5f, montreChemin.transform.position.y, node.y + 0.5f)+offset, Quaternion.identity);
            }
        }
    }
    void makeRandomCheminAlt(Pathfinding map, GameObject[,] objectMap)
    {
        List<PathNode> chemin = map.FindPath(0, 5, 9, 5);
        int startX=0;
        int startY=5;
        int currentX=startX;
        int currentY=startY;

        int blockDestroyed = 0;
        while (blockDestroyed < 3)
        {
            bool xory = UnityEngine.Random.Range(0,1)<0.5;
            if (xory)
            {
                int pastx=currentX;
                currentX+=Math.Sign(UnityEngine.Random.Range(-1, 1));
                if (!(0<=currentX&&currentX<10))
                {
                    currentX=pastx;
                }
            }
            else
            {
                int pasty=currentY;
                currentY+=Math.Sign(UnityEngine.Random.Range(-1, 1));
                if (!(0<=currentY&&currentY<10))
                {
                    currentY=pasty;
                }
            }
            Debug.Log("Le node vérifié est le node : " + "("+currentX+","+currentY+")");
            if (!map.grid[currentX, currentY].isWalkable)
            {
                //Debug.Log("Le node détruit est le node : " + "("+x+","+y+")");
                map.grid[currentX, currentY].isWalkable = true;
                objectMap[currentX, currentY] = null;
                blockDestroyed++;
            }
        }
        chemin = map.FindPath(0, 5, 9, 5);

        if (montreChemin != null && chemin != null)
        {
            foreach (PathNode node in chemin)
            {
                Debug.Log(node);
                Instantiate(montreChemin, new Vector3(node.x + 0.5f, montreChemin.transform.position.y, node.y + 0.5f), Quaternion.identity);
            }
        }
    }
}
