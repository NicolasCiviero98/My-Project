using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField] private float Cooldown = 2;
    [SerializeField] private float Range = 6;
    [SerializeField] private int HealAmount = 2;

    private float _cooldown;
    void Start() {
        
    }

    void Update() {
        _cooldown -= Time.deltaTime;
        var dist = Vector2.Distance(gameObject.transform.position, Player.Instance.gameObject.transform.position);
        if (dist < Range && _cooldown < 0) {
            Player.Instance.GetComponent<Health>().Heal(HealAmount);
            _cooldown = Cooldown;
        }
    }
}
