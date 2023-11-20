using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircleMesh : MonoBehaviour
{

    public int edges = 3;
 
    private Vector3[] vertices;
    private Vector3[] normals;
    private List<int> triangles;
    private Vector2[] uvs;
 
    private Mesh mesh;

    private List<Vector2> testPoints;

    private void Start()
    {
        float angle = 360f / (edges);
        triangles = new List<int>(edges);
        
        vertices = new Vector3[edges + 1];
        normals = new Vector3[edges + 1];
        uvs = new Vector2[edges + 1];

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";


        
        var pos = new Vector2(1, 0);
        var startPos = new Vector2(0, 0);


        vertices[0] = new Vector3(0, 0, 0);
        normals[0] = new Vector3(0, 0, -1);
        
        Vector2 center = new Vector2(0.5f, 0.5f);
        uvs[0] = center;
        
        //Pos Center
        testPoints = new List<Vector2>(edges);
        testPoints.Add(new Vector2(0, 0));
        
        for (int i = 0; i < edges; i += 1)
        {
            vertices[i + 1] = pos;
            normals[i + 1] = new Vector3(0, 0, -1); // Back
            //uvs[i + 1] = new Vector2(Mathf.Cos((i + 1) * angle)/2 + center.x, Mathf.Sin((i + 1) * angle)/2 + center.y);

            uvs[i + 1] = new Vector2(pos.x / 2, pos.y / 2) + center;
            
            if (i >= 1)
            {
                triangles.Add(0);
                triangles.Add(i + 1);
                triangles.Add(i);
            }
            
            pos = Rotate(pos, angle * Mathf.Deg2Rad);
        }
        
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(vertices.Length - 1);
        
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles.ToArray();
    }


    void Update ()
    {
        //var pos = new Vector3(0, 1, 0);
        //var angle = 360 / edges;
        //var mesh = new Mesh
        //{
        //    name = "Procedural Mesh"
        //};
//
        //vertices = new Vector3[edges + 1];
        //
        ////Pos Center
        //
        //for (int i = 1; i < edges; i++)
        //{
        //    vertices[i] = new Vector3(pos.x, pos.y, 0);
        //    Rotate(pos, angle);
        //}
        


        
    }

    public static Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            (v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta)),
            (v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta))
        );
    }

    public void OnDrawGizmos()
    {

    }
}