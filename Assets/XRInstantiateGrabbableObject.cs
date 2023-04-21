using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRInstantiateGrabbableObject : XRBaseInteractable
{

    public GameObject grabbableObject;

    public Transform transformToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        //Instantiate object
        GameObject newObject = Instantiate(grabbableObject, transformToInstantiate.position + new Vector3(0, 0, 1), Quaternion.identity);

        //Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();

        //Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }


}
