using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour {
 
    public int xSize;
    public int ySize;
 
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private Vector3[] normals;
    private Vector4[] tangents;
 
    private Mesh mesh;
   
    void Update () {
        createGrid();
    }
 
    private void createGrid()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
 
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        normals = new Vector3[(xSize + 1) * (ySize + 1)];
        uvs = new Vector2[(xSize + 1) * (ySize + 1)];
        tangents = new Vector4[(xSize + 1) * (ySize + 1)];

        float xCut = 1f / xSize;
        float yCut = 1f / ySize;
        
        for(int i = 0, y = 0; y <= ySize; y += 1)
        {
            for(int x = 0; x <= xSize; x += 1, i += 1)
            {
                vertices[i] = new Vector3(x, y, 0);
                uvs[i] = new Vector2(x * xCut, y * yCut);
                normals[i] = new Vector3(0, 0, -1);
                tangents[i] = new Vector4(1f, 0f, 0f, -1f);
            }
        }
 
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        triangles = new int[xSize * ySize * 6];

        for (int i = 0, t = 0; i < ySize; i += 1)
        {
            for (int j = 0; j < xSize; j += 1, t += 6)
            {
                triangles[t] = j + 1 + (i) * (xSize + 1);
                triangles[t + 1] = j + (i) * (xSize + 1);
                triangles[t + 2] = j + (i + 1) * (xSize + 1);
                triangles[t + 3] = j + 1 + (i) * (xSize + 1);
                triangles[t + 4] = j + (i + 1) * (xSize + 1);
                triangles[t + 5] = j + 1 + (i + 1) * (xSize + 1);
            }
        }

        
        mesh.triangles = triangles;
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
 
    }
 
}