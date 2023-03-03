using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSummoner : MonoBehaviour
{
    public float Damage = 12;
    [SerializeField] private float Cooldown = 1f;
    [SerializeField] private float projectileSpeed = 15;
    [SerializeField] private float criticalChance;
    [SerializeField] private float Range;
    
    [SerializeField] private float Distance;
    public float Angle;

    [SerializeField] private GameObject Bolt;
    public GameObject Player;
    [SerializeField] private Transform spawnPoint;

    private float _cooldown;
    
    void Start() {
    }
    
    void Update() {
        _cooldown -= Time.deltaTime;
        if (_cooldown < 0) Shoot();

        var start = Player.transform.position;
        var pos = new Vector3();
        pos.x = start.x + Mathf.Sin(Angle) * Distance;
        pos.y = start.y + Mathf.Cos(Angle) * Distance;

        this.gameObject.transform.position = pos;
    }

    private void Shoot() {
        var target = FindTarget();
        var direction = Aim(target);
        if (direction == null) return;
        Fire((Vector2)direction);
        _cooldown = Cooldown;
    }
    private GameObject FindTarget() {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (allTargets == null || allTargets.Length == 0) return null;
        GameObject target = null;
        var minDist = Range;
        foreach (GameObject tmpTarget in allTargets) {
            var dist = Vector2.Distance(spawnPoint.position, tmpTarget.transform.position);
            if (dist < minDist) {
                target = tmpTarget;
                minDist = dist;
            }
        }
        return target;
    }
    private Vector2? Aim(GameObject target) {
        if (target == null) return null;

        var direction = target.transform.position - spawnPoint.position;
        direction.y += target.GetComponent<BoxCollider2D>().size.y / 2;
        return direction;
    }
    private void Fire(Vector2 direction) {
        if (direction == null) return;

        var isCritical = Random.Range(0, 100) < criticalChance;
        var projectile = Instantiate(isCritical ? Bolt : Bolt, spawnPoint.position, spawnPoint.rotation);
        var damageSource = projectile.GetComponent<DamageSource>();
        projectile.transform.right = direction;
        damageSource.Source = Player;
        damageSource.damage = (int)(Damage);
        if (isCritical) {
            damageSource.damage *= 2; 
            damageSource.Critical = true;
        }
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.SetActive(true);
    }
}
