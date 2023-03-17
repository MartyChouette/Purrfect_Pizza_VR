using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
