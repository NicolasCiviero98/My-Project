using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public FloatingJoystick joystick;

    private Rigidbody2D body;
    public Vector3 movement { get; private set; }
    public Vector3 facing { get; private set; }
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        GetComponent<Health>().SetHealth(Health, Health);
        facing = new Vector3(1,0,0);
    }
    
    void Update() {
        // Compute input
        if (joystick.Horizontal == 0 && joystick.Vertical == 0) movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f); 
        else movement = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        if (movement.magnitude > 0.1) facing = movement;
        if (movement.x > 0.05) GetComponent<SpriteRenderer>().flipX = false;
        if (movement.x < -0.05) GetComponent<SpriteRenderer>().flipX = true;
    }

    
    public void OnDeath() {
        Time.timeScale = 0;

        //var prefab = (GameObject)Resources.Load("GameOverPanel", typeof(GameObject));
        //Instantiate(prefab, this.transform.position, Quaternion.identity);
        //prefab.GetComponent<GameOverUI>()?.Activate();
    }

    private void FixedUpdate() {
        transform.position = transform.position + movement * Speed * Time.fixedDeltaTime;
        //body.velocity = movement.normalized * Speed;
    }

    public void IncreseDamage() {
        Damage += 2;
    }

}
