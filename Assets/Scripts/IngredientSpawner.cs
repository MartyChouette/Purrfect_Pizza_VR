using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameObject _UIContent;
    [SerializeField] private GameObject _spawnPoint;

    private void OnValidate()
    {
        if (_buttonPrefab == null)
        {
            Debug.Log("Ingredient Spawner is missing a spawn button prefab.", this.gameObject);
        }
        if (_UIContent == null)
        {
            Debug.Log("Ingredient Sapwner is missing a UI content gameobject.", this.gameObject);
        }
        if (_spawnPoint == null)
        {
            Debug.Log("Ingredient Spawner is missing a spawn point prefab.", this.gameObject);
        }
    }
    
    void Start()
    {
        createSpawnButtons();
    }

    private void createSpawnButtons()
    {
        foreach (GameObject ingredient in OrderManager.Instance.allIngredients.Values)
        {
            Button button = Instantiate(_buttonPrefab, _UIContent.transform);
            button.GetComponentInChildren<Text>().text = ingredient.name;
            button.onClick.AddListener(() => Ingredient.instantiate(ingredient, _spawnPoint.transform));
        }
    }

    // private void instantiateIngredient(GameObject ingredient)
    // {
    //     GameObject go = Instantiate(ingredient, _spawnPoint.transform);
    //     go.name = ingredient.name;
    //     go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); // Change ingredient size
    // }
}