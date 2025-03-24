using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Touch touch;
    float touchPosXStart, timeSwipe, velocitySwipe;
    public GameObject sphereGO;
    Sphere sphereScript;
    void Start()
    {
        sphereScript = sphereGO.GetComponent<Sphere>();
        sphereScript.MoveSphere(0);
    }
    
    void Update()
    {
        Swipe();
        PsudeSwipe();
    }

    void PsudeSwipe() //
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosXStart = Input.mousePosition.x;
            timeSwipe = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Main(Input.mousePosition.x);    
        }  
    }
    void Swipe()
    {
        if (Input.touchCount > 0 )
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPosXStart = touch.position.x;
                timeSwipe = 0;
            }
            Main(touch.position.x);  
        }            
    }

    void Main(float touchPosX)
    {
        timeSwipe += Time.deltaTime;
        if (timeSwipe == 0) return;
        velocitySwipe = (touchPosX - touchPosXStart) / timeSwipe;
        sphereScript.MoveSphere(velocitySwipe * 15);
    }
}
