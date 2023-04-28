using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServiceSurface : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private GameObject _correctOrderAnimPrefab;
    [SerializeField] private GameObject _incorrectOrderAnimPrefab;
    [SerializeField] private Messages _checkOrderMessages;
    private GameObject _incompleteOrderMessage;

    private void OnValidate()
    {
        if (_correctOrderAnimPrefab == null)
        {
            Debug.Log("Service Surface is missing Correct Order Animation prefab.", this.gameObject);
        }
        if (_correctOrderAnimPrefab == null)
        {
            Debug.Log("Service Surface is missing Incorrect Order Animation prefab.", this.gameObject);
        }
        if (_checkOrderMessages == null)
        {
            Debug.Log("Service Surface is missing Check Order Messages scriptable object.", this.gameObject);
        }
    }

    public void onPizzaDetected(GameObject pizzaChildObject)
    {
        checkOrder(pizzaChildObject);
    }

    public void onPizzaUndetected()
    {
        Destroy(_incompleteOrderMessage);
    }

    private void checkOrder(GameObject pizzaIngredients)
    {
        bool isMissingIngredient = false;
        GameObject pizza = pizzaIngredients.transform.parent.gameObject;
        Dictionary<string, int> addedIngredients = pizzaIngredients.GetComponent<IngredientsDetector>().addedIngredients;

        if (OrderManager.Instance.allPizzaTypes.ContainsKey(pizza.name))
        {
            // Go through the ingredients added on the pizza by checking Ingredient object child of the pizza game object
            foreach (KeyValuePair<string, int> requiredIngredient in pizzaIngredients.GetComponent<IngredientsDetector>().recipe)
            {
                if (addedIngredients.ContainsKey(requiredIngredient.Key))
                {
                    if (addedIngredients[requiredIngredient.Key] < requiredIngredient.Value)
                    {
                        Debug.Log("Short of " + requiredIngredient.Key);
                        isMissingIngredient = true;
                    }
                }
                else
                {
                    Debug.Log("Missing " + requiredIngredient.Key);
                    isMissingIngredient = true;
                }
            }
        }
        else
        {
            Debug.Log("The Pizza is not on the order list.", pizza);
            isMissingIngredient = true;
        }

        if (!isMissingIngredient)
        {
            Timer.Create(() => onPizzaCompleted(pizza), _timeToDestroy, null);
        }
        else
        {
            onPizzaIncomplete();
        }
    }

    private void onPizzaCompleted(GameObject pizza)
    { 
        string[] messages = _checkOrderMessages.correctOrderMessages;
        GameObject go = Instantiate(_correctOrderAnimPrefab, this.transform); // As soon as this object is instantiated the animation is displayed.
        float animDuration = go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length; // Obtain the duration of the animation.
        go.GetComponentInChildren<TextMeshProUGUI>().text = messages[Random.Range(0, messages.Length)]; // Change the display text to random messages from the scriptable object
        Destroy(pizza);
        LevelManager.Instance.addScore();
        Timer.Create(() => Destroy(go), animDuration, null);
    }

    private void onPizzaIncomplete()
    {
        string[] messages = _checkOrderMessages.wrongOrderMessages;
        _incompleteOrderMessage = Instantiate(_incorrectOrderAnimPrefab, this.transform);
        _incompleteOrderMessage.GetComponentInChildren<TextMeshProUGUI>().text = messages[Random.Range(0, messages.Length)];
    }
}
