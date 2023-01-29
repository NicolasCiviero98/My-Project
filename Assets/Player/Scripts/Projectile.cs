using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Source;
    public int pierces = 0;
    public int damage;
    
    void Awake()
    {
        Destroy(gameObject, 1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Source) return;
        //collider.gameObject. Destroy(collider.gameObject);// change for dealing damage
        if (collider.GetComponent<Health>() != null)
        {
            collider.GetComponent<Health>().Damage(damage);
        }
        if (pierces <= 0) Destroy(gameObject);
        pierces--;
    }
}
