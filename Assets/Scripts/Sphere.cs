using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Sphere: MonoBehaviour
{
    public GameController gc;
    public Rigidbody rb;

    const string _state = "Regular";
    const float _speed = 35;
    Material _material;
    public string state;
    public float speed;
 
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public void MoveSphere(float velocitySwipe)
    {   
        rb.AddForce(velocitySwipe * Time.deltaTime,
            -10 * speed / (1 + Math.Abs(rb.velocity.y)),
            20 * speed / (1 + Math.Abs(rb.velocity.z)));
    }

    void OnCollisionEnter(Collision col)
    {
        GameObject go = col.gameObject;
        if (state == "Destroyer" && go.tag == "Destroyable") 
        {
            Destroy(col.collider);
            go.GetComponent<Animator>().SetTrigger("Destroy");
            rb.AddForce(0, -6000, 10000);
            gc.PlaySound("S Destroy");
            if (go.name == "Tree") gc.ReduceStars();

            Countdown timer = Countdown.CreateInstance;
            timer.StartTimer(3);
            timer.Completed += () => 
            {    
                Destroy(go);         
            };
        }      
    }
    public void Uncontrolable(int countdownTime)
    {
        int rand = UnityEngine.Random.Range(-1, 2);
        if (countdownTime % 3 == 0)
        {
            rb.AddForce(1000 * rand, 0, 0);
        }
    }

    public void Jump()
    {
        rb.AddForce(0, 10000, 2000);
    }

    void OnTriggerEnter(Collider col)
    {
        switch(col.tag)
        {
            case "TriggerOptimization":
                col.GetComponentInParent<GenerateLocation>().OptimizationLoop(gameObject);
                break;
            case "Location":
                Destroy(gc.timer);
                break;
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Location")
        {
            gc.MenuActivateOnDed();
        }
    }

    public void Reset()
    {
        speed = _speed;
        state = _state;
        GetComponent<MeshRenderer>().material = _material;
    }
}
