using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float speed;
    public FloatingJoystick joystick;
    public int health;

    private Rigidbody2D body;
    private Vector3 movement;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        GetComponent<Health>().SetHealth(health, health);
    }
    
    void Update()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (joystick.Horizontal == 0 && joystick.Vertical == 0) movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f); 
        else movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        if (movement.x > 0.05) GetComponent<SpriteRenderer>().flipX = false;
        if (movement.x < -0.05) GetComponent<SpriteRenderer>().flipX = true;

        if (anim != null) UpdateAnimator();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Internal methods
    private void Move()
    {
        transform.position = transform.position + movement * speed * Time.deltaTime;
        //body.velocity = movement.normalized * speed;
    }

    private void UpdateAnimator()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        if (Math.Abs(movement.x) < 0.01 && Math.Abs(movement.y) < 0.01) return;

        if (Math.Abs(movement.x) > Math.Abs(movement.y))
        { //Horizontal movement
            if (movement.x < 0) anim.SetFloat("Side", 0);
            else anim.SetFloat("Side", 1);
        }
        else
        { // Vertical movement
            if (movement.y < 0) anim.SetFloat("Side", 2);
            else anim.SetFloat("Side", 3);
        }
    }

}
