using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected string pickupType;
    protected GameController gc;

    void Awake()
    {
        gc = GameObject.Find("Game Controller").GetComponent<GameController>();
    }
    
    protected virtual void Picking()
    {
        gc.AddStars();
        Destroy(gameObject);
        gc.PlaySound("S Pickup");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Picking();          
        }
    }

}
