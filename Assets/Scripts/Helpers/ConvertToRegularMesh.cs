using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour
{
    // Converts SkinnedMeshRenderer to regular Mesh render

    [ContextMenu("Convert to regular mesh")] // this will show up in Unity editor as a component, where we can click  and run it
    public void Convert() 
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;

        DestroyImmediate(skinnedMeshRenderer); // remove from editor skinnedMeshRenderer
        DestroyImmediate(this); // we'll then also this destory this script(ConvertToRegularMesh)
    }
}
