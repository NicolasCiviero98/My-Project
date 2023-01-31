using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Health>().SetHealth(hp, hp);
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Debug.Log("Collision with player");
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().Damage(damage);
                this.GetComponent<Health>().Damage(10000);
            }
        }
    }

    public void OnDeath()
    {
        var ExpPrefab = (GameObject)Resources.Load("Exp", typeof(GameObject));
        Instantiate(ExpPrefab, this.transform.position, Quaternion.identity);
    }

}
