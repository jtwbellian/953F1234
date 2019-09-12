using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatalogVerticies : MonoBehaviour
{
    public GameObject targetMesh;

[ContextMenu("Bake Vertex Catalog")]
    void Catalog()
    {
    MeshFilter mf = targetMesh.GetComponent<MeshFilter>();
    Matrix4x4 localToWorld = transform.localToWorldMatrix;
 
    for(int i = 0; i<mf.sharedMesh.vertices.Length; ++i)
        {
        Vector3 world_v = localToWorld.MultiplyPoint3x4(mf.sharedMesh.vertices[i]);
        Debug.Log("Index: " + i.ToString() + "  Worldspace Location:" + world_v.ToString());
        }
    
    }
}
