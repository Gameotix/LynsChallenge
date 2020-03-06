using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float minZ, maxZ;
    public GameObject[] enemies;
    public int numberOfEnemies;
    public float spawnTime;

    private int currentEnemies;

    private void Update()
    {
        if (currentEnemies >= numberOfEnemies)
        {
            int enemies = FindObjectsOfType<Enemy>().Length;
            if (enemies <= 0)
            {
                FindObjectOfType<CameraFollow>().maxXAndY.x = 200;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void SpawnEnemy()
    {
        bool positionX = Random.Range(0, 2) == 0 ? true:false;
        Vector3 spawnPosition;
        spawnPosition.z = Random.Range(minZ, maxZ);

        if (positionX)
        {
            spawnPosition = new Vector3(this.transform.position.x + 10, 0, spawnPosition.z);
        }
        else
        {
            spawnPosition = new Vector3(this.transform.position.x - 10, 0, spawnPosition.z);
        }
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity);
        currentEnemies++;
        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<CameraFollow>().maxXAndY.x = this.transform.position.x;
            SpawnEnemy();
        }
    }
}
