using UnityEngine;
using Unity.Collections;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Advanced : MonoBehaviour {

    void OnEnable () 
    {
        Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
        Mesh.MeshData meshData = meshDataArray[0];
        
        var mesh = new Mesh {
            name = "Procedural Mesh"
        };

        Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
}