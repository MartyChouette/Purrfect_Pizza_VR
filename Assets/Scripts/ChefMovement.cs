using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefMovement : MonoBehaviour
{
    public Transform player;
    public Transform station;
    public float turnSpeed = 1.0f;
    public bool cooking;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = station.position - transform.position;

        if (cooking == true)
        {
            targetDirection = station.position - transform.position;
        } else
        {
            targetDirection = player.position - transform.position;
        }

        float singleStep = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
