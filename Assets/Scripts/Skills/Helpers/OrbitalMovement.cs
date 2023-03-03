using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
  
    public GameObject Center;
    public float RotationalSpeed;
    public float Distance;
    public float Angle;


    void Start()
    {
        
    }

    void Update()
    {
        var step = 0.08f;
        var start = Center.transform.position;
        Angle = (Angle + RotationalSpeed * Time.deltaTime) % (2f * Mathf.PI);
        var pos = new Vector3();
        pos.x = start.x + Mathf.Sin(Angle) * Distance;
        pos.y = start.y + Mathf.Cos(Angle) * Distance;

        var currentPos = this.gameObject.transform.position;
        var newPos = currentPos * (1-step) + pos * step;
        this.gameObject.transform.position = newPos;
    }
}
