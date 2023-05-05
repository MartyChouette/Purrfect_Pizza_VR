using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;
    private Pizza _spawnPizza;

    void Start()
    {
        delegateDropdown();
        delegateButton();
    }

    private void delegateDropdown()
    {
        var dropdown = this.gameObject.GetComponentInChildren<Dropdown>();
        
        // Assign the pizza names to the dropdown options
        foreach (string pizzaName in OrderManager.Instance.allPizzaTypes.Keys)
        {
            dropdown.options.Add(new Dropdown.OptionData() {text = pizzaName});
        }
        dropdown.RefreshShownValue();
        dropdownItemSelected(dropdown); // Assign the default selected item
        dropdown.onValueChanged.AddListener(delegate {dropdownItemSelected(dropdown);}); // Assign listener for when the selection is changed.
    }

    private void dropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        _spawnPizza = OrderManager.Instance.allPizzaTypes[dropdown.options[index].text];
    }

    private void delegateButton()
    {
        var button = this.gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(onButtonClicked);
    }

    private void onButtonClicked()
    {
        _spawnPizza.instantiatePizza(_spawnPoint.transform);
    }
}
