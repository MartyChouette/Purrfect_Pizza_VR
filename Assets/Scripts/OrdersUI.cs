using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private GameObject _Content;
    [SerializeField] private Text _OrderDetailTextPrefab;
    
    
    //private List<string> _orderNames = OrderManager.orderNames;
    private List<Text> _orderObjectList = new List<Text>();

    void Start()
    {
        displayOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: function that display text when start
    private void displayOnStart()
    {
        foreach (Pizza order in OrderManager.Instance.orderList)
        {
            _OrderDetailTextPrefab.text = order.name;
            _orderObjectList.Add(Instantiate(_OrderDetailTextPrefab, _Content.transform));
        }
    }
}
