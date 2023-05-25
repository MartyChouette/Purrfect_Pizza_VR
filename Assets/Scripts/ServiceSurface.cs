using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServiceSurface : MonoBehaviour
{
    public static ServiceSurface Instance;
    [SerializeField] private float _checkingTime;
    [SerializeField] private GameObject _correctOrderAnimPrefab;
    [SerializeField] private GameObject _incorrectOrderAnimPrefab;
    [SerializeField] private Messages _checkOrderMessages;
    [SerializeField] private AudioSource _orderCorrectSFX;
    [SerializeField] private AudioSource _orderIncorrectSFX;
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
        if (_orderCorrectSFX == null)
        {
            Debug.Log("Service Surface is missing Order Correct SFX.", this.gameObject);
        }
        if (_orderIncorrectSFX == null)
        {
            Debug.Log("Service Surface is missing Order Incorrect SFX.", this.gameObject);
        }
    }

    private void Awake() => Instance = this;

    private void Start()
    {
        _incompleteOrderMessage = null;
    }

    public void onPizzaDetected(GameObject pizza)
    {
        pizza.GetComponent<IngredientsDetector>().onServiceTable = true;
        if (_incompleteOrderMessage != null)
        {
            disableIncompleteOrderMessage();
        }
        checkOrder(pizza);
    }

    public void onPizzaUndetected(GameObject pizza)
    {
        pizza.GetComponent<IngredientsDetector>().onServiceTable = false;
        if (_incompleteOrderMessage != null)
        {
            disableIncompleteOrderMessage();
        }
    }

    public void onPizzaUpdated(GameObject pizza)
    {
        if (_incompleteOrderMessage != null)
        {
            disableIncompleteOrderMessage();
        }
        checkOrder(pizza);
    }

    private void checkOrder(GameObject pizza)
    {
        bool isMissingIngredient = false;
        Dictionary<string, int> addedIngredients = pizza.GetComponent<IngredientsDetector>().addedIngredients;

        if (OrderManager.Instance.allPizzaTypes.ContainsKey(pizza.name))
        {
            // Go through the ingredients added on the pizza by checking Ingredient object child of the pizza game object
            foreach (KeyValuePair<string, int> requiredIngredient in pizza.GetComponent<IngredientsDetector>().recipe)
            {
                if (addedIngredients.ContainsKey(requiredIngredient.Key))
                {
                    if (addedIngredients[requiredIngredient.Key] < requiredIngredient.Value)
                    {
                        Debug.Log("Missing " + (requiredIngredient.Value - addedIngredients[requiredIngredient.Key]) + " " + requiredIngredient.Key);
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
            Timer.Create(() => onPizzaCompleted(pizza), _checkingTime);
        }
        else
        {
            //Timer.Create(() => onPizzaIncomplete(), _checkingTime); //<<<=========================== TO ENABLE INCORRECT ORDER POPUP MESSAGE
        }
    }

    private void onPizzaCompleted(GameObject pizza)
    {
        // Instantiate popup message gameobject
        string[] messages = _checkOrderMessages.correctOrderMessages;
        GameObject go = Instantiate(_correctOrderAnimPrefab, this.transform); // As soon as this object is instantiated the animation is displayed.
        go.GetComponentInChildren<TextMeshProUGUI>().text = messages[Random.Range(0, messages.Length)]; // Change the default display text to random messages from the scriptable object
        
        // Play SFX
        _orderCorrectSFX.Play();

        // Destroy popup message gameobject after the animation is over
        float animDuration = go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length; // Obtain the duration of the animation.
        Timer.Create(() => Destroy(go), animDuration);

        // Add score
        LevelManager.Instance.addScore();

        // Destroy the pizza gameobject
        Destroy(pizza);
    }

    private void onPizzaIncomplete()
    {
        // Instantiate popup message gameobject
        string[] messages = _checkOrderMessages.wrongOrderMessages;
        GameObject go = Instantiate(_incorrectOrderAnimPrefab, this.transform); // As soon as this object is instantiated the animation is displayed.
        go.GetComponentInChildren<TextMeshProUGUI>().text = messages[Random.Range(0, messages.Length)];// Change the default display text to random messages from the scriptable object

        // Play SFX
        _orderIncorrectSFX.Play();

        // Destroy popup message gameobject after the animation is over
        float animDuration = go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length; // Obtain the duration of the animation.
        Timer.Create(() => Destroy(go), animDuration);

        // Assign the popup message gameobject to the class data, in case if the gameobject needs to be disable before the animation is over.
        _incompleteOrderMessage = go;
    }

    private void disableIncompleteOrderMessage()
    {
        _incompleteOrderMessage.gameObject.SetActive(false);
        _incompleteOrderMessage = null;
    }
}
