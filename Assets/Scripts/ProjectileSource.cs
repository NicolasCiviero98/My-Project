using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSource : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public GameObject projectileCritPrefab;
    public float projectileSpeed = 15;
    [Range(1, 100)] [SerializeField] public float criticalChance = 15;

    private GameObject target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindTarget();
            Fire();
        }
    }

    public void OnPanelClicked()
    {
        FindTarget();
        Fire();
    }

    private void FindTarget()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (allTargets != null && allTargets.Length > 0)
        {
            target = allTargets[0];
            //look for the closest
            foreach (GameObject tmpTarget in allTargets)
            {
                if (Vector2.Distance(spawnPoint.position, tmpTarget.transform.position) < Vector2.Distance(spawnPoint.position, target.transform.position))
                {
                    target = tmpTarget;
                }
            }
        }
    }

    private void Fire()
    {
        Vector2 direction = target != null 
            ? target.transform.position - spawnPoint.position
            : new Vector2(0,1);

        var isCritical = Random.Range(0, 100) < criticalChance;
        var projectile = Instantiate(isCritical ? projectileCritPrefab : projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        projectile.transform.right = direction;
        projectile.GetComponent<Projectile>().Source = gameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.SetActive(true);
    }

}
