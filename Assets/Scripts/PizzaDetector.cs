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
        if (other.tag == "Pizza")
        {
            GameObject parent = this.transform.parent.gameObject;
            if (_serviceTable)
            {
                parent.GetComponent<ServiceSurface>().onPizzaDetected(other.gameObject);
            }
            
            if(_prepTable)
            {
                parent.GetComponent<PrepSurface>().onPizzaDetected(other.gameObject);
                StartCoroutine(FreezeRotation(parent));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pizza")
        {
            GameObject parent = this.transform.parent.gameObject;
            if (_serviceTable)
            {
                parent.GetComponent<ServiceSurface>().onPizzaUndetected(other.gameObject);
            }

            if (_prepTable)
            {
                parent.GetComponent<PrepSurface>().onPizzaUndetected(other.gameObject);
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
