using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSource : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileCritPrefab;
    [SerializeField] private float projectileSpeed;
    [Range(1, 100)] [SerializeField] private float criticalChance;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnPanelClicked();
        }
    }

    public void OnPanelClicked()
    {
        var target = FindTarget();
        var direction = Aim(target);
        Fire(direction);
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
        if (target == null) return GetComponent<Player>().movement;

        var direction = target.transform.position - spawnPoint.position;
        direction.y += target.GetComponent<BoxCollider2D>().size.y / 2;
        return direction;
    }
    private void Fire(Vector2 direction) {

        var isCritical = Random.Range(0, 100) < criticalChance;
        var projectile = Instantiate(isCritical ? projectileCritPrefab : projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        projectile.transform.right = direction;
        projectile.GetComponent<Projectile>().Source = gameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.SetActive(true);
    }

}
