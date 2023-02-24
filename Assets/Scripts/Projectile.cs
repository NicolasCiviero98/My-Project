using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public int pierces = 0;
    
    void Awake() {
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<Health>() == null) return;
        if (pierces <= 0) Destroy(gameObject);
        pierces--;
    }

}
