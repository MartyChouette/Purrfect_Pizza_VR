using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;

/*
Note: The tool is still being work as it is having issue creating and saving meshes.
*/

public class CreateDoughPrefabs : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private GameObject _ingredients;
    private Object[] _originalPrefabs;

    private void Awake()
    {
        loadOriginalPrefabs();
        create();
    }

    private void loadOriginalPrefabs()
    {
        _originalPrefabs = Resources.LoadAll("Pizza Constructor/Prefab/Pizza Doughs", typeof(GameObject));
        if (_originalPrefabs.Length > 0)
        {
            Debug.Log("Succesfully Loaded: " + _originalPrefabs.Length + " Original Prefabs");
            foreach (var prefab in _originalPrefabs)
            {
                Debug.Log(prefab.name);
            }
        }
        else
        {
            Debug.Log("Unable to load objects.");
        }
    }

    private void create()
    {
        foreach (var prefab in _originalPrefabs)
        {
            // Instantiate an instance of the prefab and destroy an existing mesh collider
            GameObject go = Instantiate((GameObject)prefab);
            Destroy(go.GetComponent<MeshCollider>());

            // Add mesh filter and set the mesh to the combined meshes of all the pizza slices of this prefab instance
            var meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.mesh = CombinedSlicesMesh(go, prefab.name);

            // Add mesh renderer and set the material to the proper material
            var meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.material = _material;

            // Add mesh collider and set its convex to true
            var meshCollider = go.AddComponent<MeshCollider>();
            meshCollider.convex = true;

            // Add rigidbody
            go.AddComponent<Rigidbody>();

            /*
            // Instantiate an instance of the Ingredients prefab as the child of this instance prefab
            GameObject goChild = Instantiate(_ingredients, go.transform);

            // Add ingredient detector script and set detector and container properties
            var ingreDetector = go.AddComponent<IngredientsDetector>();
            ingreDetector.detector = goChild.transform.GetChild(0).gameObject;
            ingreDetector.container = goChild.transform.GetChild(1).gameObject;
            */

            // Add XR grab interactable script and set use dynamic attach property to true
            var xrGrab = go.AddComponent<XRGrabInteractable>();
            xrGrab.useDynamicAttach = true;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                Destroy(go.transform.GetChild(i).gameObject);
            }

            // Locate and save the instance as a prefab
            string prefabLocalPath = "Assets/Prefabs/Pizzas/Pizza Doughs";
            prefabLocalPath += "/" + prefab.name + ".prefab";
            //bool successful;
            //PrefabUtility.SaveAsPrefabAsset(go, prefabLocalPath, out successful);
            //if (successful)
            //{
            //    Debug.Log(prefab.name + " new prefab was successfully created.");
            //}
            //else
            //{
            //    Debug.Log(prefab.name + " new prefab was NOT successfully created.");
            //}

            /*
            if (AssetDatabase.FindAssets(prefab.name, new[] {localPath}).Length == 0)
            {

            }
            else
            {
                Debug.Log(prefab.name + " is already existed.");
            }*/
        }
        //AssetDatabase.SaveAssets();
    }

    //private Mesh CombinedSlicesMesh(GameObject go, string name)
    //{
    //    var meshFilters = new List<MeshFilter>();
    //    for (int i = 0; i < go.transform.childCount; i++)
    //    {
    //        meshFilters.Add(go.transform.GetChild(i).gameObject.GetComponent<MeshFilter>());
    //        //Destroy(go.transform.GetChild(i).gameObject);
    //    }

    //    var combine = new CombineInstance[meshFilters.Count];

    //    for (int i = 0; i < meshFilters.Count; i++)
    //    {
    //        combine[i].mesh = meshFilters[i].sharedMesh;
    //        combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
    //    }

    //    var mesh = new Mesh();
    //    mesh.name = name;
    //    mesh.CombineMeshes(combine);

    //    string meshLocalPath = "Assets/Meshes/Pizza Doughs/" + name;
    //    AssetDatabase.CreateAsset(mesh, meshLocalPath);

    //    return mesh;
    //}

    private Mesh CombinedSlicesMesh(GameObject go, string name)
    {
        var meshFilters = new List<MeshFilter>();
        for (int i = 0; i < go.transform.childCount; i++)
        {
            meshFilters.Add(go.transform.GetChild(i).gameObject.GetComponent<MeshFilter>());
        }

        var combine = new CombineInstance[meshFilters.Count];

        for (int i = 0; i < meshFilters.Count; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.name = name;
        mesh.CombineMeshes(combine, true);

        return mesh;
    }
}


//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class CreateDoughPrefabs : MonoBehaviour
//{
//    [SerializeField] private Material _material;
//    [SerializeField] private GameObject _ingredients;
//    [SerializeField] private GameObject[] _originalPrefabs; // Assign these in the inspector

//    private void Awake()
//    {
//        if (_originalPrefabs.Length > 0)
//        {
//            Debug.Log("Successfully Loaded: " + _originalPrefabs.Length + " Original Prefabs");
//            foreach (var prefab in _originalPrefabs)
//            {
//                Debug.Log("Instantiating prefab: " + prefab.name);
//                CreatePrefabInstance(prefab);
//            }
//        }
//        else
//        {
//            Debug.Log("No prefabs assigned.");
//        }
//    }

//    private void CreatePrefabInstance(GameObject prefab)
//    {
//        GameObject go = Instantiate(prefab);
//        DestroyImmediate(go.GetComponent<MeshCollider>());

//        // Add mesh filter and set the mesh to the combined meshes of all the pizza slices of this prefab instance
//        var meshFilter = go.AddComponent<MeshFilter>();
//        meshFilter.mesh = CombinedSlicesMesh(go, prefab.name);

//        // Add mesh renderer and set the material
//        var meshRenderer = go.AddComponent<MeshRenderer>();
//        meshRenderer.material = _material;

//        // Add mesh collider and set its convex to true
//        var meshCollider = go.AddComponent<MeshCollider>();
//        meshCollider.convex = true;

//        // Add rigidbody
//        go.AddComponent<Rigidbody>();

//        // Add XR grab interactable script and set use dynamic attach property to true
//        var xrGrab = go.AddComponent<XRGrabInteractable>();
//        xrGrab.useDynamicAttach = true;

//        for (int i = 0; i < go.transform.childCount; i++)
//        {
//            Destroy(go.transform.GetChild(i).gameObject);
//        }

//        // Additional logic to manipulate the instantiated prefab can go here
//    }

//    private Mesh CombinedSlicesMesh(GameObject go, string name)
//    {
//        var meshFilters = new List<MeshFilter>();
//        for (int i = 0; i < go.transform.childCount; i++)
//        {
//            meshFilters.Add(go.transform.GetChild(i).gameObject.GetComponent<MeshFilter>());
//        }

//        var combine = new CombineInstance[meshFilters.Count];

//        for (int i = 0; i < meshFilters.Count; i++)
//        {
//            combine[i].mesh = meshFilters[i].sharedMesh;
//            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
//        }

//        var mesh = new Mesh();
//        mesh.name = name;
//        mesh.CombineMeshes(combine, true);

//        return mesh;
//    }
//}
