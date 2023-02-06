using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject Prefab0;
    [SerializeField] private GameObject Prefab1;
    [SerializeField] private GameObject Prefab2;
    [SerializeField] private GameObject Prefab3;
    [SerializeField] private GameObject Prefab4;

    [SerializeField] private int Rarity0;
    [SerializeField] private int Rarity1;
    [SerializeField] private int Rarity2;
    [SerializeField] private int Rarity3;
    [SerializeField] private int Rarity4;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistance = 18;

    private GameObject player;
    private float[] probabilities;
    private float probabilitiesSum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        probabilities = new float[5];
        probabilities[0] = 1f / Rarity0;
        probabilities[1] = 1f / Rarity1;
        probabilities[2] = 1f / Rarity2;
        probabilities[3] = 1f / Rarity3;
        probabilities[4] = 1f / Rarity4;

        probabilitiesSum = probabilities[0] + probabilities[1] + 
            probabilities[2] + probabilities[3] + probabilities[4];

        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        var start = player.transform.position;

        Vector3 pos = new Vector3();
        float angle = Random.Range(0, 2f) * Mathf.PI;
        pos.x = start.x + Mathf.Sin(angle) * spawnDistance;
        pos.y = start.y + Mathf.Cos(angle) * spawnDistance;

        yield return new WaitForSeconds(Random.Range(1f, 4f));
        GameObject newEnemy = Instantiate(RollEnemy(), pos, Quaternion.identity);
        StartCoroutine(spawnEnemy());
    }

    private GameObject RollEnemy()
    {
        var rand = Random.Range(0, probabilitiesSum);
        var accumulator = 0f;

        accumulator += probabilities[0];
        if (rand < accumulator) return Prefab0;
        accumulator += probabilities[1];
        if (rand < accumulator) return Prefab1;
        accumulator += probabilities[2];
        if (rand < accumulator) return Prefab2;
        accumulator += probabilities[3];
        if (rand < accumulator) return Prefab3;
        return Prefab4;
    }


}
