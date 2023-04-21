using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameObject _UIContent;
    [SerializeField] private GameObject _spawnPoint;
    
    void Start()
    {
        createSpawnButtons();
    }

    private void createSpawnButtons()
    {
        foreach (GameObject ingredient in OrderManager.Instance.allIngredients)
        {
            Button button = Instantiate(_buttonPrefab, _UIContent.transform);
            button.GetComponentInChildren<Text>().text = ingredient.name;
            button.onClick.AddListener(() => instantiateIngredient(ingredient));
        }
    }

    private void instantiateIngredient(GameObject ingredient)
    {
        GameObject go = Instantiate(ingredient, _spawnPoint.transform);
        go.name = ingredient.name;
    }
}
