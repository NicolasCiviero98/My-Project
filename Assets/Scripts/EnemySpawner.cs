using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject bigSlimePrefab;

    [SerializeField] private float slimeInterval = 3.5f;
    [SerializeField] private float bigSlimeInterval = 8f;
    [SerializeField] private float spawnDistance = 5;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(spawnEnemy(slimeInterval, slimePrefab));
        StartCoroutine(spawnEnemy(bigSlimeInterval, bigSlimePrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        var start = player.transform.position;

        Vector3 pos = new Vector3();
        float angle = Random.Range(0, 2f) * Mathf.PI;
        pos.x = start.x + Mathf.Sin(angle) * spawnDistance;
        pos.y = start.y + Mathf.Cos(angle) * spawnDistance;

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
