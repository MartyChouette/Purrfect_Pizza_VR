using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaDetector : MonoBehaviour
{
    [SerializeField] private bool _prepTable;
    [SerializeField] private bool _serviceTable;
    //private bool _pizzaDetected;

    private void OnValidate()
    {
        if (_prepTable && _serviceTable)
        {
            _prepTable = false;
            _serviceTable = false;
        }
    }

    private void Start()
    {
        //_pizzaDetected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Pizza")) //& !_pizzaDetected)
        {
            //_pizzaDetected = true;
            GameObject surface = this.transform.parent.gameObject;
            //GameObject pizza = other.transform.parent.parent.gameObject;

            if (_serviceTable)
            {
                surface.GetComponent<ServiceSurface>().onPizzaDetected(other.gameObject);
            }
            
            if(_prepTable)
            {
                surface.GetComponent<PrepSurface>().onPizzaDetected(other.gameObject);
                //StartCoroutine(FreezeRotation(pizza));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pizza")) //& _pizzaDetected)
        {
            //_pizzaDetected = false;
            GameObject surface = this.transform.parent.gameObject;
            //GameObject pizza = other.transform.parent.parent.gameObject;

            if (_serviceTable)
            {
                surface.GetComponent<ServiceSurface>().onPizzaUndetected(other.gameObject);
            }

            if (_prepTable)
            {
                surface.GetComponent<PrepSurface>().onPizzaUndetected(other.gameObject);
            }
        }
    }


    private IEnumerator FreezeRotation(GameObject pizza)
    {
        yield return new WaitForSeconds(0.5f);
        Rigidbody rb = pizza.GetComponent<Rigidbody>();
        //if (rb != null) rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        if (rb != null) rb.isKinematic = true;
    }
}
