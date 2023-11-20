using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))] // 
public class MainScript : MonoBehaviour
{

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
 
    private Mesh mesh;
    public float size = 1f;
    private void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };
        
        mesh.vertices = new Vector3[]
        {
            new Vector3(0f, 0f), new Vector3(size, 0f), new Vector3(0f, size), new Vector3(size, size),
            new Vector3(0f, 0f, size), new Vector3(size, 0f, size), new Vector3(0f, size, size), new Vector3(size, size, size),
            new Vector3(size, size, size), new Vector3(0f, size, size), new Vector3(0f, size, size), new Vector3(0f, 0f, size),
            new Vector3(0f, 0f, size), new Vector3(size, 0f, size),
        };

        mesh.triangles = new int[]
        {
            0, 2, 1, 1, 2, 3,
            5, 7, 4, 4, 7, 6,
            1, 3, 5, 5, 3, 7,
            2, 9, 3, 3, 9, 8,
            11, 10, 0, 0, 10, 2,
            12, 0, 13, 13, 0, 1
        };
        
        mesh.normals = new Vector3[]
        {
            Vector3.back, Vector3.back, Vector3.back, new Vector3(1f, 1f, 1f),
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
            Vector3.forward, Vector3.forward,
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(0.25f, 0.25f), new Vector2(0.50f, 0.25f), new Vector2(0.25f, 0.50f), new Vector2(0.5f, 0.5f),
            new Vector2(1f, 0.25f), new Vector2(0.75f, 0.25f), new Vector2(1f, 0.5f), new Vector2(0.75f, 0.50f),
            new Vector2(0.5f, 0.75f), new Vector2(0.25f, 0.75f), new Vector2(0f, 0.50f), new Vector2(0f, 0.25f),
            new Vector2(0.25f, 0.0f), new Vector2(0.50f, 0.0f),
        };

        mesh.tangents = new Vector4[]
        {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
            new Vector4(1f, 0f, 0f, 1f),
        };
        
        GetComponent<MeshFilter>().mesh = mesh;

    }
}
