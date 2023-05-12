using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    [SerializeField] private ConveyorPath _waypoints;
    private float _speed = 12f;
    //private Transform _currentWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            if (other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint == null && other.gameObject.GetComponent<IngredientsDetector>().previousWaypoint == null)
            {
                other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint = _waypoints.GetNextWaypoint(other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint);
            }
            else if (other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint == null)
            {
                other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint = other.gameObject.GetComponent<IngredientsDetector>().previousWaypoint;
            }

            other.gameObject.GetComponent<IngredientsDetector>().previousWaypoint = other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint;
            other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint = _waypoints.GetNextWaypoint(other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint);

            if (other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint != null)
            {
                other.transform.LookAt(other.gameObject.GetComponent<IngredientsDetector>().currentWaypoint);

                if (other.gameObject.GetComponent<ConstantForce>() == null)
                {
                    ConstantForce move = other.gameObject.AddComponent<ConstantForce>();
                    move.relativeForce = new Vector3(0, 0, _speed);
                }
            }
            else
            {
                if (other.gameObject.GetComponent<ConstantForce>() != null)
                {
                    other.gameObject.GetComponent<ConstantForce>().relativeForce = Vector3.zero;
                }
            }
        }
    }
}
