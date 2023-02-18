using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class WaveSpawner : MonoBehaviour
{
   
    public List<EnemyGroup> groups = new List<EnemyGroup>();
    public int waveDuration;
    public int waveInterval;
    public int waveValueMultiplier = 30;
    public float spawnDistance = 18;


    private int currWave; 
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
 
 
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    private GameObject player;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        GenerateWave();
    }
 
    void FixedUpdate() {
        if(waveTimer <= 0) {
            GenerateWave();
            return;
        }

        if(spawnTimer <= 0 && enemiesToSpawn.Count > 0) spawnEnemy();
 
        spawnTimer -= Time.fixedDeltaTime;
        waveTimer -= Time.fixedDeltaTime;
    }
 
    private void GenerateWave() {
        currWave++;
        GenerateEnemies();
 
        spawnInterval = (float)waveDuration / enemiesToSpawn.Count;
        waveTimer = waveInterval;
    }
 
    private void GenerateEnemies() {
        var waveValue = currWave * waveValueMultiplier;
        var generatedEnemies = new List<GameObject>();
        int groupId = Random.Range(0, groups.Count);
        var enemies = groups[groupId].enemies;

        while(waveValue > 0 && generatedEnemies.Count < 50) {
            int enemyId = Random.Range(0, enemies.Count);
            generatedEnemies.Add(enemies[enemyId]);
            waveValue -= enemies[enemyId].GetComponent<Enemy>().power;
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
  
    private void spawnEnemy() {
        var start = player.transform.position;
        var pos = new Vector3();
        var angle = Random.Range(0, 2f) * Mathf.PI;
        pos.x = start.x + Mathf.Sin(angle) * spawnDistance;
        pos.y = start.y + Mathf.Cos(angle) * spawnDistance;

        GameObject enemy = Instantiate(enemiesToSpawn[0], pos, Quaternion.identity);
        enemiesToSpawn.RemoveAt(0);
        spawnedEnemies.Add(enemy);
        spawnTimer = spawnInterval;
    }
}

[System.Serializable]
public class EnemyGroup
{
    public List<GameObject> enemies;
}