using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class TaskUI
{
    public GameObject content;
    public GameObject button;
}

public class TaskManager : MonoBehaviour
{
    /*
    public string[] tasks = new string[] {
        "Cut Onions",
        "Grill Meat",
        "Cook Eggs",
        "Mash Potatoes",
        "Sear Veggies"
    };

    public ChefTaskY chefY;
    public ChefTaskZ chefZ;

    public GameObject uiPrefab;
    protected Canvas taskScreen;
    protected GameObject taskui;
    */
    public static TaskManager Instance;
    [SerializeField] private TaskUI _xTaskUI;
    [SerializeField] private TaskUI _yTaskUI;
    [SerializeField] private GameObject _selectablePrefab;
    private List<Base> pizzaBases = new List<Base>();
    public Dictionary<GameObject, Base> _chefXTasks {get; private set;} = new Dictionary<GameObject, Base>();
    public Dictionary<GameObject, Base> _chefYTasks {get; private set;} = new Dictionary<GameObject, Base>();

    void Awake() => Instance = this;
    
    void Start()
    {
        collectSauceBases();
        displayTasksAtStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void collectSauceBases()
    {
        foreach (Pizza type in OrderManager.Instance.pizzaTypes)
        {   
            bool isExisted = false;
            foreach (Base pizzaBase in pizzaBases)
            {
                if (pizzaBase.name == type.pizzaBase.name)
                {
                    isExisted = true;
                }
            }
            if (!isExisted)
            {
                pizzaBases.Add(type.pizzaBase);
            }
        }
    }

    private void displayTasksAtStart()
    {
        foreach (Base pizzaBase in pizzaBases)
        {
            _selectablePrefab.GetComponentInChildren<Text>().text = pizzaBase.name;
            _chefXTasks.Add(Instantiate(_selectablePrefab, _xTaskUI.content.transform), pizzaBase);
            _chefYTasks.Add(Instantiate(_selectablePrefab, _yTaskUI.content.transform), pizzaBase);
        }
    }
}
