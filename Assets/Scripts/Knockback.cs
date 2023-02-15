using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float ReceivingMultiplier = 1f;
    [SerializeField] private float DealingMultiplier = 1f;
    [SerializeField] private float delay = 0.2f;

    public UnityEvent OnBegin, OnDone;
    private const float defaultStrength = 2f;


    private void OnTriggerEnter2D(Collider2D collider) {
        var projectile = collider.GetComponent<Projectile>();
        if (projectile == null || projectile.Source == this.gameObject) return;
        
        Vector3 direction = collider.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        PlayFeedback(collider.gameObject, direction);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (this.gameObject.CompareTag("Enemy")) return;
        var other = collision.collider.gameObject;
        
        Vector3 direction = this.gameObject.transform.position - other.transform.position;
        PlayFeedback(other, direction);
    }
    
    public void PlayFeedback(GameObject sender, Vector3 direction) {
        StopAllCoroutines();
        OnBegin?.Invoke();

        var senderKnockback = sender.GetComponent<Knockback>();
        var dealerMultiplier = senderKnockback == null ? 1f : senderKnockback.DealingMultiplier;
        var strength = 2 * GetSpeed() * ReceivingMultiplier * dealerMultiplier;
        
        body.velocity = Vector3.zero;
        body.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private float GetSpeed() {
        var enemy = this.GetComponent<Enemy>();
        if (enemy != null) return enemy.speed;
        var player = this.GetComponent<Player>();
        if (player != null) return player.speed;
        return 1;
    }


    private IEnumerator Reset() {
        yield return new WaitForSeconds(delay);
        body.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
