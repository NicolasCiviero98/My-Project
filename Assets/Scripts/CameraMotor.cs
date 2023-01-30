using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float speed;

    void LateUpdate()
    {
        var deltaX = lookAt.position.x - transform.position.x;
        var deltaY = lookAt.position.y - transform.position.y;

        if (Math.Abs(deltaX) < 0.5 && Math.Abs(deltaY) < 0.5) return;


        transform.position += new Vector3(deltaX * Math.Abs(deltaX) * speed * Time.deltaTime, deltaY * Math.Abs(deltaY) * speed * Time.deltaTime, 0);
    }
}
