using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Modifier : Pickup
{
    [SerializeField] protected bool isInstant;
    [SerializeField] protected float timeActivity;
    [SerializeField] protected Material tempMaterial;
    Sphere sphereScript;
    Countdown timer;
    
    void Start()
    {
        sphereScript = GameObject.Find("Main Sphere").GetComponent<Sphere>();
    }

    protected override void Picking() 
    {
        if (isInstant)
        {        
            ModifierActivation();
            Destroy(gameObject);
        }
        else if (gc.panelModifiers.transform.childCount < 3)
        {
            gc.AddButton(gameObject);
            gc.PlaySound("S Modifier Pickup");
        }   
    }

    public void ModifierActivation()
    {
        if (sphereScript.state != "Regular") 
        {
            Destroy(GameObject.Find("Timer Modificator"));
            sphereScript.Reset();
        }

        gc.PlaySound("S Modifier Activation");
        sphereScript.state = pickupType;
        sphereScript.GetComponent<MeshRenderer>().material = tempMaterial;
        ModifierSelection();

        timer = Countdown.CreateInstance;
        timer.transform.name = "Timer Modificator";
        timer.StartTimer(timeActivity);
        timer.Completed += () => sphereScript.Reset();
    }
    void ModifierSelection()
    {
        switch (pickupType)
        {           
            case "SpeedUp":
                sphereScript.speed = 150;
                break;
            case "Jump":
                sphereScript.Jump();
                break;
            case "Rotate":
                GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("Rotate");
                break;
        }
    }
}

