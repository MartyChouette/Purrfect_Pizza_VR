using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsDetector : MonoBehaviour
{
    public GameObject detector;
    public GameObject container;
    [HideInInspector] public bool onPrepTable;
    [HideInInspector] public bool onServiceTable;
    //[HideInInspector] public bool onConveyorBelt;
    [HideInInspector] public Dictionary<string, int> recipe {get;} = new Dictionary<string, int>();
    [HideInInspector] public Dictionary<string, int> addedIngredients {get;} = new Dictionary<string, int>();
    [HideInInspector] public Transform currentWaypoint;
    [HideInInspector] public Transform previousWaypoint;
    public const float cylinderColliderRadius = 0.45f;
    private Dictionary<int, FixedJoint> _fixedJoints {get;} = new Dictionary<int, FixedJoint>();
    
    private void Start()
    {
        if (container == null)
        {
            Debug.Log("Ingredient Detector is missing a container gameobject.", this.gameObject);
        }
        onPrepTable = false;
        onServiceTable = false;
        currentWaypoint = null;
        previousWaypoint = null;
        //onConveyorBelt = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            //
            //Debug.Log(other.name + " added");
            //
            OnIngredientAdded(other.gameObject);

            // Update the recipe UI if the pizza is on the prep table and the added ingredient is in the recipe.
            if (onPrepTable & recipe.ContainsKey(other.name))
            {
                PrepSurface.Instance.updateRecipeUI(other.name, this.gameObject);
            }

            // Check when ingredients are added while the pizza is on the service table.
            if (onServiceTable)
            {
                //ServiceSurface.Instance.onPizzaUpdated(pizzaIngredients);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Added Ingredient"))
        {
            OnIngredientRemoved(other.gameObject);

            // Update the recipe UI if the pizza is on the prep table and the removed ingredient is in the recipe.
            if (onPrepTable & recipe.ContainsKey(other.name))
            {
                PrepSurface.Instance.updateRecipeUI(other.name, this.gameObject);
            }
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor Belt"))
        {
            onConveyorBelt = true;
        }
    }*/

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyor Belt"))
        {
            //onConveyorBelt = false;
            if (this.gameObject.GetComponent<ConstantForce>() != null)
            {
                this.gameObject.GetComponent<ConstantForce>().relativeForce = Vector3.zero;
                Destroy(this.gameObject.GetComponent<ConstantForce>());
            }
        }
    }

    public void OnIngredientAdded(GameObject ingredient)
    {
        // Fixed Joint components to keep the ingredients sticking to the pizza
        _fixedJoints.Add(ingredient.GetInstanceID(), this.gameObject.AddComponent<FixedJoint>() as FixedJoint);
        _fixedJoints[ingredient.GetInstanceID()].connectedBody = ingredient.GetComponent<Rigidbody>();
        //_fixedJoints[ingredient.GetInstanceID()].breakForce = 100f;
        //_fixedJoints[ingredient.GetInstanceID()].breakTorque = 100f;
        
        ingredient.transform.SetParent(container.transform);
        ingredient.tag = "Added Ingredient";
        ingredient.GetComponent<Rigidbody>().mass = 0f;
        ingredient.GetComponent<Rigidbody>().useGravity = false;
        //ingredient.GetComponent<Rigidbody>().angularDrag = 0f;
        //ingredient.GetComponent<Rigidbody>().isKinematic = true;

        if (addedIngredients.ContainsKey(ingredient.name))
        {
            addedIngredients[ingredient.name]++;
        }
        else
        {
            addedIngredients.Add(ingredient.name, 1);
        }
    }

    private void OnIngredientRemoved(GameObject ingredient)
    {
        //
        //Debug.Log("Removed " + ingredient.name + "; " + _fixedJoints.Count);
        //
        // Destroy the Fixed Joint components
        Destroy(_fixedJoints[ingredient.GetInstanceID()]);
        _fixedJoints.Remove(ingredient.GetInstanceID());
        //
        ingredient.transform.SetParent(this.gameObject.transform.root.parent); // Set the ingredient gameobject's parent to the root scene
        ingredient.tag = "Ingredient";
        ingredient.GetComponent<Rigidbody>().mass = 1f;
        ingredient.GetComponent<Rigidbody>().useGravity = true;
        //ingredient.GetComponent<Rigidbody>().angularDrag = 0.05f;
        //ingredient.GetComponent<Rigidbody>().isKinematic = false;

        addedIngredients[ingredient.name]--;
        if (addedIngredients[ingredient.name] <= 0)
        {
            addedIngredients.Remove(ingredient.name);
        }
    }

    public int numberOfIngredientsLeftToAdd(string recipeIngreName)
    {
        int recipeIngreAmount = recipe[recipeIngreName];
        // Ensure the added ingredients contain an ingredient from the recipe
        if (addedIngredients.ContainsKey(recipeIngreName))
        {
            // The amount of added ingredients may exceed the require amount from the recipe
            if (addedIngredients[recipeIngreName] < recipeIngreAmount)
            {
                return recipeIngreAmount - addedIngredients[recipeIngreName];
            }
            return 0;
        }
        return recipeIngreAmount;
    }
}
