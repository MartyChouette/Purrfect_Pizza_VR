using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private const float size = 0.7f;
    private const float _ingredientSpawnHeight = 0.4f;
    public static GameObject instantiate(GameObject ingredient, Transform parentTransform)
    {
        GameObject go = Instantiate(ingredient, parentTransform);
        go.name = ingredient.name;
        go.transform.localScale = new Vector3(size, size, size); // Change ingredient size
        return go;
    }

    public static GameObject instantiateOnPizza(GameObject ingredient, IngredientsDetector ingredientsDetectorSC, bool spawnedAboveSurface = false)
    {
        if (!spawnedAboveSurface) ingredientsDetectorSC.detector.SetActive(false);
        // Instantiate the ingredient on the pizza
        GameObject go = instantiate(ingredient, ingredientsDetectorSC.container.transform);

        // Set random position for the ingredient on the pizza surface inside the crust (GameObject: detector area)
        go.transform.position += randomIngredientPosition(spawnedAboveSurface);
        
        if (!spawnedAboveSurface)
        {
            ingredientsDetectorSC.OnIngredientAdded(go);
            ingredientsDetectorSC.detector.SetActive(true);
        }
        return go;
    }

    private static Vector3 randomIngredientPosition(bool spawnedAboveSurface)
    {
        float x = UnityEngine.Random.Range(-1*IngredientsDetector.cylinderColliderRadius, IngredientsDetector.cylinderColliderRadius);
        float maxZ = Mathf.Sqrt(IngredientsDetector.cylinderColliderRadius*IngredientsDetector.cylinderColliderRadius - x*x);
        float z = UnityEngine.Random.Range(-1*maxZ, maxZ);
        return new Vector3(x, (spawnedAboveSurface)? _ingredientSpawnHeight : 0, z);
    }
}
