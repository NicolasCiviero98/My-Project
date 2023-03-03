using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float smoothTime = 0.25f;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        var targetPosition = lookAt.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void aLateUpdate()
    {
        //var deltaX = lookAt.position.x - transform.position.x;
        //var deltaY = lookAt.position.y - transform.position.y;

        //if (Math.Abs(deltaX) < 1 && Math.Abs(deltaY) < 1) return;
        //if (Math.Abs(deltaX) > 1) {
        //    transform.position = new Vector3(lookAt.position.x + deltaX / Math.Abs(deltaX), lookAt.position.y, 0);
        //    return;
        //}
        

        //transform.position += new Vector3(deltaX * Math.Abs(deltaX) * speed * Time.deltaTime, deltaY * Math.Abs(deltaY) * speed * Time.deltaTime, 0);
    }
}
