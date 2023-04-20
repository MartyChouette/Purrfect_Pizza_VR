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
    public static TaskManager Instance;
    [SerializeField] private TaskUI _xTaskUI;
    [SerializeField] private TaskUI _yTaskUI;
    [SerializeField] private GameObject _selectablePrefab;
    [HideInInspector] public List<Dough> _doughs {get; private set;} = new List<Dough>();

    void Awake() => Instance = this;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void collectPizzaDoughs()
    {
        foreach (Pizza type in OrderManager.Instance.pizzaTypes)
        {   
            bool isExisted = false;
            foreach (Dough dough in _doughs)
            {
                if (dough.name == type.dough.name)
                {
                    isExisted = true;
                }
            }
            if (!isExisted)
            {
                _doughs.Add(type.dough);
            }
        }
    }

    private void displayTasksAtStart()
    {
        for (int i = 0; i < _doughs.Count; i++)
        {
            GameObject x = Instantiate(_selectablePrefab, _xTaskUI.content.transform);
            x.GetComponent<Selectable>().Init("x"+i, _doughs[i]);
            GameObject y = Instantiate(_selectablePrefab, _yTaskUI.content.transform);
            y.GetComponent<Selectable>().Init("y"+i, _doughs[i]);
        }
    }

    public event Action<string, Dough> onTaskSelected;
    public void TaskSelected(string id, Dough dough)
    {
        onTaskSelected?.Invoke(id, dough);
    }

    public event Action<string> onTaskUnselected;
    public void TaskUnselected(string id)
    {
        onTaskUnselected?.Invoke(id);
    }
    */
}
