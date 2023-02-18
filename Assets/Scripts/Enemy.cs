using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] public float speed;
    [SerializeField] public int power; // used for: wave cost, score, experience calculation...

    private GameObject player;
    private Rigidbody2D body;
    private Vector3? _force;
    private DateTime? _forceStop;



    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        GetComponent<Health>().SetHealth(hp, hp);
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);

        if (_force != null) {
            if (DateTime.Now > _forceStop) {
                _force = null;
                _forceStop = null;
            }
            else {
                transform.position += (Vector3)_force * Time.fixedDeltaTime;
            }
        }


        //var movement = player.transform.position - this.transform.position;
        //body.velocity = movement.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            //Debug.Log("Collision with player");
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().Damage(damage);
                //this.GetComponent<Health>().Damage(10000);
            }
        }
    }

    public void OnDeath() {
        var expPrefab = (GameObject)Resources.Load("Exp", typeof(GameObject));
        expPrefab.GetComponent<ExperienceObject>().expCount = power;
        Instantiate(expPrefab, this.transform.position, Quaternion.identity);
    }

    public void ApplyKnockback(Vector3 force, double milliseconds) {
        //_force = force;
        //_forceStop = DateTime.Now.AddMilliseconds(milliseconds);
    }




}
