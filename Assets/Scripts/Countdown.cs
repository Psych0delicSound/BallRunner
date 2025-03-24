using System;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public static Countdown CreateInstance
    {
        get
        {
            var go = new GameObject(name:"Timer");
            var timer = go.AddComponent<Countdown>();
            return timer;
        }
    }
    public event Action Completed;

    float countdownTime;
    Sphere sphereScript;

    void Start()
    {
        sphereScript = GameObject.Find("Main Sphere").GetComponent<Sphere>();
    }

    public void StartTimer(float targetTime)
    {
        countdownTime = targetTime;
    }

    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            ModifierSelection();
        }
        else
        {
            Completed?.Invoke();
            Destroy(gameObject);
        }
    }

    void ModifierSelection()
    {
        switch (sphereScript.state)
        {
            case "Uncontrolable":
                sphereScript.Uncontrolable((int)countdownTime);
                break;
        }
    }
}
