using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public FloatingJoystick joystick;
    public int health;

    private Rigidbody2D body;
    public Vector3 movement { get; private set; }
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        GetComponent<Health>().SetHealth(health, health);
    }
    
    void Update() {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0) movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f); 
        else movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        if (movement.x > 0.05) GetComponent<SpriteRenderer>().flipX = false;
        if (movement.x < -0.05) GetComponent<SpriteRenderer>().flipX = true;
    }

    private void FixedUpdate() {
        transform.position = transform.position + movement * speed * Time.fixedDeltaTime;
        //body.velocity = movement.normalized * speed;
    }


}
