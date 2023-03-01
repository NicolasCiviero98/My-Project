using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightiningBolt : Skill
{
    public static LightiningBolt Instance;

    [SerializeField] private float BaseDamage = 12;
    [SerializeField] private float BaseCooldown = 5.0f;
    [SerializeField] private float projectileSpeed = 15;
    [SerializeField] private float criticalChance;
    [SerializeField] private GameObject Bolt;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform spawnPoint;

    private float[] _damageMultiplier = {0, 1, 1.4f, 1.4f, 1.8f, 1.8f, 2f, 2.3f};
    private float[] _attackSpeedMultiplier = {1, 1, 1, 1.3f, 1.3f, 1.6f, 1.6f, 1.8f};
    
    public float DamageMultiplier => _damageMultiplier[Level];
    public float AttackSpeedMultiplier => _attackSpeedMultiplier[Level];

    private float _cooldown;
    
    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        MaxLevel = _damageMultiplier.Length - 1;
    }
    void Update() {
        _cooldown -= Time.deltaTime;
        if (_cooldown < 0 && ActionInputController.Instance.ActionInputActive()) {
            var target = FindTarget();
            var direction = Aim(target);
            Fire(direction);
            _cooldown = BaseCooldown / AttackSpeedMultiplier;
        }
    }

    private GameObject FindTarget() {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (allTargets == null || allTargets.Length == 0) return null;
        var target = allTargets[0];
        var minDist = Vector2.Distance(spawnPoint.position, target.transform.position);
        foreach (GameObject tmpTarget in allTargets) {
            var dist = Vector2.Distance(spawnPoint.position, tmpTarget.transform.position);
            if (dist < minDist) {
                target = tmpTarget;
                minDist = dist;
            }
        }
        return target;
    }
    private Vector2 Aim(GameObject target) {
        if (target == null) return Player.GetComponent<Player>().facing;

        var direction = target.transform.position - spawnPoint.position;
        direction.y += target.GetComponent<BoxCollider2D>().size.y / 2;
        return direction;
    }
    private void Fire(Vector2 direction) {

        var isCritical = Random.Range(0, 100) < criticalChance;
        var projectile = Instantiate(isCritical ? Bolt : Bolt, spawnPoint.position, spawnPoint.rotation);
        var damageSource = projectile.GetComponent<DamageSource>();
        projectile.transform.right = direction;
        damageSource.Source = Player;
        damageSource.damage = (int)(BaseDamage * DamageMultiplier);
        if (isCritical) {
            damageSource.damage *= 2; 
            damageSource.Critical = true;
        }
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.SetActive(true);
    }

}
