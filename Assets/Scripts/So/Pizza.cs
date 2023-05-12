using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza")]
public class Pizza : ScriptableObject
{
    [Serializable]
    public class Ingredient
    {
        public int amount;
        public GameObject ingredientPrefab;
    }
    public GameObject doughPrefab;
    public Ingredient[] recipe;
    [Tooltip("Include one of each ingredient from the recipe.")]
    public bool includeSomeIngredients;
    private Mesh _combinedMesh;

    private void OnValidate()
    {
        if (doughPrefab == null)
        {
            Debug.Log(this.name + " scriptable object is missing a dough prefab.", this);
        }

        foreach (var item in recipe)
        {
            if (item.ingredientPrefab == null)
            {
                Debug.Log(this.name + " scriptable object is missing an ingredient prefab.", this);
            }
        }
    }

    private void OnEnable()
    {
        createCombinedMesh();
    }

    public void instantiatePizza(Transform spawnTransform, Transform parentTransform = null)
    {
        GameObject pizzaGO = Instantiate(doughPrefab, spawnTransform);
        if (parentTransform != null)
        {
            pizzaGO.transform.SetParent(parentTransform);
        }
        pizzaGO.name = this.name;
        pizzaGO.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        //pizzaGO.GetComponent<Rigidbody>().mass = 1;

        addMeshComponents(pizzaGO);

        IngredientsDetector ingredientsDetectorSC = pizzaGO.GetComponent<IngredientsDetector>();
        foreach (Ingredient ingredient in recipe)
        {
            ingredientsDetectorSC.recipe.Add(ingredient.ingredientPrefab.name, ingredient.amount);

            if (includeSomeIngredients)
            {
                instantiateIngredient(ingredient.ingredientPrefab, ingredientsDetectorSC);
            }
        } 
    }

    private void instantiateIngredient(GameObject ingredient, IngredientsDetector ingredientsDetectorSC)
    {
        ingredientsDetectorSC.detector.SetActive(false);
        GameObject go = Instantiate(ingredient, ingredientsDetectorSC.container.transform);
        go.name = ingredient.name;
        go.transform.position += randomIngredientPosition;
        go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); // Change ingredient size
        ingredientsDetectorSC.OnIngredientAdded(go);
        ingredientsDetectorSC.detector.SetActive(true);
    }

    private Vector3 randomIngredientPosition
    {
        get
        {
            float x = UnityEngine.Random.Range(-1*IngredientsDetector.cylinderColliderRadius, IngredientsDetector.cylinderColliderRadius);
            float maxZ = Mathf.Sqrt(IngredientsDetector.cylinderColliderRadius*IngredientsDetector.cylinderColliderRadius - x*x);
            float z = UnityEngine.Random.Range(-1*maxZ, maxZ);
            return new Vector3(x, 0, z);
        }
    }

    private void createCombinedMesh()
    {
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        foreach (var sliceMeshFilter in doughPrefab.GetComponentsInChildren<MeshFilter>())
        {
            meshFilters.Add(sliceMeshFilter);
        }
        _combinedMesh = MeshCombiner.CombineMeshes(meshFilters, doughPrefab.name);
    }

    private void addMeshComponents(GameObject pizzaGO)
    {
        MeshFilter meshFilter = pizzaGO.AddComponent<MeshFilter>();
        meshFilter.mesh = _combinedMesh;
        Material mat = pizzaGO.GetComponentInChildren<MeshRenderer>().material; // Must reference the child material before creating MeshRenderer componenet on the root object (pizzaGO).
        MeshRenderer meshRenderer = pizzaGO.AddComponent<MeshRenderer>();
        meshRenderer.material = mat;
        MeshCollider meshCollider = pizzaGO.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        pizzaGO.transform.Find("Slices").gameObject.SetActive(false);
    }
}
