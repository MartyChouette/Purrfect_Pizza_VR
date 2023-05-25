using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner
{
    //[SerializeField] private List<MeshFilter> _sourceMeshFilters;
    //[SerializeField] private MeshFilter _targetMeshFilter;

    //[ContextMenu(itemName: "Combine Meshes")]
    public static Mesh CombineMeshes(List<MeshFilter> meshFilters, string name)
    {
        var combine = new CombineInstance[meshFilters.Count];

        for (int i = 0; i < meshFilters.Count; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.name = name;
        mesh.CombineMeshes(combine);

        return mesh;
    }
}
