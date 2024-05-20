using UnityEngine;

public class MeshGeneratorScript : MonoBehaviour
{
    Mesh mesh;    
    Vector3[] vertices;
    int[] triangle;
    int xSize;
    int zSize;
    // Start is called before the first frame update
    void Start()
    {
        xSize=10;
        zSize=10;
        mesh = new Mesh();

        gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        gameObject.AddComponent<MeshRenderer>();
        Material yourMaterial = Resources.Load("Materials/Black", typeof(Material)) as Material;
        GetComponent<MeshRenderer>().material=yourMaterial;

        CreateShape();
        UpdateMesh();

        gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().providesContacts=true;
    }

    void CreateShape(){
        vertices = new Vector3[(xSize+1)*(zSize+1)];
        for (int i = 0, z=0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = GetComponent<Transform>().position + new Vector3(x-xSize/2,0,z-xSize/2);
                i++;
            }
        }
        
        triangle = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, z = 0; z < zSize; z++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
			triangle[ti] = vi;
			triangle[ti + 3] = triangle[ti + 2] = vi + 1;
			triangle[ti + 4] = triangle[ti + 1] = vi + xSize + 1;
			triangle[ti + 5] = vi + xSize + 2;
            }
		}
    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangle;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {      
    }
}
