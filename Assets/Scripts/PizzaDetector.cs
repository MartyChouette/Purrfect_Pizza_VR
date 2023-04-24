using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaDetector : MonoBehaviour
{
    [SerializeField] private bool _prepTable;
    [SerializeField] private bool _serviceTable;

    private void OnValidate()
    {
        if (_prepTable && _serviceTable)
        {
            _prepTable = false;
            _serviceTable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Pizza" & other.name == "Ingredients")
        {
            GameObject parent = this.transform.parent.gameObject;
            if (_serviceTable)
            {
                parent.GetComponent<ServiceSurface>().onPizzaDetected(other.gameObject);
            }
            
            if(_prepTable)
            {
                parent.GetComponent<PrepSurface>().onPizzaDetected(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "Pizza" & other.name == "Ingredients")
        {
            GameObject parent = this.transform.parent.gameObject;
            if (_prepTable)
            {
                parent.GetComponent<PrepSurface>().onPizzaUndetected();
            }
        }
    }
}
