using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    [SerializeField] GameObject sphereObj;
    float spherePosRelativeX, spherePosRelativeY, spherePosRelativeZ;
    void Start()
    {
        GetCameraPositionRelative();
    }

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        Vector3 spherePos = sphereObj.transform.position;
        transform.position = new Vector3(spherePos.x + spherePosRelativeX, spherePos.y + spherePosRelativeY, spherePos.z +spherePosRelativeZ);
    }

    void GetCameraPositionRelative()
    {
        spherePosRelativeX = transform.position.x;
        spherePosRelativeY = transform.position.y;
        spherePosRelativeZ = transform.position.z;
    }
}
