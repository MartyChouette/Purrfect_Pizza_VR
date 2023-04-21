using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreateToppingPrefabs : MonoBehaviour
{
    [SerializeField] private Material _material;
    private Object[] _originalPrefabs;

    private void Awake()
    {
        loadOriginalPrefabs();
        create();
    }

    private void loadOriginalPrefabs()
    {
        _originalPrefabs = Resources.LoadAll("Pizza Constructor/Prefab/Ingredients", typeof(GameObject));
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
            GameObject go = Instantiate((GameObject)prefab);
            go.AddComponent(typeof(Rigidbody));
            go.GetComponent<MeshRenderer>().material = _material;
            go.GetComponent<MeshCollider>().convex = true;

            string localPath = "Assets/Prefabs/Pizzas/Ingredients";
            if (AssetDatabase.FindAssets(prefab.name, new[] {localPath}).Length > 0)
            {
                localPath += "/" + prefab.name + ".prefab";
                bool successful;
                PrefabUtility.SaveAsPrefabAsset(go, localPath, out successful);
                if (successful)
                {
                    Debug.Log(go.name + " new prefab was successfully created.");
                }
                else
                {
                    Debug.Log(go.name + " new prefab was NOT successfully created.");
                }
            }
        }
    }
}
