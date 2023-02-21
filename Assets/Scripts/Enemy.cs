using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] public int power; // used for: wave cost, score, experience calculation...

    private GameObject player;
    private Rigidbody2D body;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        GetComponent<Health>().SetHealth(hp, hp);
    }

    void FixedUpdate() {
        if (player != null) transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, Speed * Time.fixedDeltaTime);

        //var movement = player.transform.position - this.transform.position;
        //body.velocity = movement.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var collider = collision.collider;
        if (collider.CompareTag("Player")) {
            if (collider.GetComponent<Health>() != null) {
                collider.GetComponent<Health>().Damage(damage);
            }
        }
    }

    public void OnDeath() {
        SummonExperienceOrb();
        GameController.Statistics.OnEnemyDeath(this);
    }

    private void SummonExperienceOrb() {
        var expPrefab = (GameObject)Resources.Load("Exp", typeof(GameObject));
        expPrefab.GetComponent<ExperienceObject>().expCount = power;
        Instantiate(expPrefab, this.transform.position, Quaternion.identity);
    }

}
