using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    [Tooltip("Automatically spawn pizzas with completed ingredients one by one of all pizza types from the level order.")]
    [SerializeField] private bool _spawnPizzaAutomatically;
    [SerializeField] private Pizza[] _pizzas;
    [SerializeField] private GameObject _spawnPoint;
    
    private Button _button;
    private Pizza _spawnPizza;
    private int _pizzaIndex;
    private Coroutine _coroutine;
    private bool _isSpawning;

    void Start()
    {
        _isSpawning = false;
        var dropdown = this.gameObject.GetComponentInChildren<Dropdown>();

        if (_spawnPizzaAutomatically)
        {
            dropdown.gameObject.SetActive(false);
        }
        else
        {
            delegateDropdown(dropdown);
        }
        delegateButton();
    }

    private void delegateButton()
    {
        _button = this.gameObject.GetComponentInChildren<Button>();
        _button.onClick.AddListener(onButtonClicked);
    }

    private void onButtonClicked()
    {
        if (_spawnPizzaAutomatically)
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                _button.GetComponentInChildren<Text>().text = "Stop Spawning";
                _coroutine = StartCoroutine(autoSpawnPizzas());
            }
            else
            {
                _button.GetComponentInChildren<Text>().text = "Cont. Spawning";
                StopCoroutine(_coroutine);
                _isSpawning = false;
            }
        }
        else
        {
            _spawnPizza.instantiate(_spawnPoint.transform, this.transform.root);
        }
    }

    /*
        Spawn Pizzas manuelly from created list (_pizzas).
    */

    public Pizza[] pizzas
    {
        get
        {
            return _pizzas;
        }
    }

    private void delegateDropdown(Dropdown dropdown)
    {
        // Assign the pizza names to the dropdown options
        foreach (Pizza pizza in _pizzas)
        {
            dropdown.options.Add(new Dropdown.OptionData() {text = pizza.name});
        }
        dropdown.RefreshShownValue();
        dropdownItemSelected(dropdown); // Assign the default selected item
        dropdown.onValueChanged.AddListener(delegate {dropdownItemSelected(dropdown);}); // Assign listener for when the selection is changed.
    }

    private void dropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        _spawnPizza = _pizzas[index];
    }

    /*
        Automatically spawn pizzas with completed ingredients one by one of all pizza types from the level order.
    */

    IEnumerator autoSpawnPizzas()
    {
        foreach (Pizza pizza in OrderManager.Instance.allPizzaTypes.Values)
        {
            GameObject go = pizza.instantiate(_spawnPoint.transform, this.transform.root);
            go.GetComponent<Rigidbody>().isKinematic = true;
            foreach (var item in pizza.recipe)
            {
                for (int i = 0; i < item.amount; i++)
                {
                    GameObject ingreGO = Ingredient.instantiateOnPizza(item.ingredientPrefab, go.GetComponent<IngredientsDetector>(), true);
                    //ingreGO.GetComponent<Rigidbody>().isKinematic = false;
                    yield return new WaitForSeconds(0.5f);
                }
            }
            go.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(6);
        }
        yield return null;
    }
}
