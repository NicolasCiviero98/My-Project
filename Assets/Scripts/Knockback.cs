using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [Tooltip("Divisor for knockback received")]
    [SerializeField] private float Weight = 1f;
    [Tooltip("Multiplier for knockback dealt")]
    [SerializeField] private float Strength = 1f;

    public UnityEvent OnBegin, OnDone;
    private const float defaultStrength = 2f;
    private Entity entity;
    private float delay = 0.5f;

    void Start() {
        entity = GetComponent<Entity>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        var damageSource = collider.GetComponent<DamageSource>();
        if (damageSource == null || damageSource.Source == this.gameObject) return;
        
        Vector3 direction = collider.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        PlayFeedback(collider.gameObject, direction);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var other = collision.collider.gameObject;
        if (this.gameObject.CompareTag("Enemy") && other.CompareTag("Enemy")) return;

        Vector3 direction = this.gameObject.transform.position - other.transform.position;
        PlayFeedback(other, direction);
    }
    
    public void PlayFeedback(GameObject sender, Vector3 direction) {
        StopAllCoroutines();
        OnBegin?.Invoke();

        var senderKnockback = sender.GetComponent<Knockback>();
        var dealerMultiplier = senderKnockback == null ? 1f : senderKnockback.Strength;
        var strength = defaultStrength * entity.Speed * dealerMultiplier / Weight;
        
        body.velocity = Vector3.zero;
        body.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        yield return new WaitForSeconds(delay);
        body.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
