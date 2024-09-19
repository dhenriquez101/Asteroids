using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30;
    public float spawnRateIncrement = 1f;
    public float xLimit = 0;
    public float maxTimeLife = 4;
    public float speed = 5f;
    private float spawnNext = 0;
    private List<GameObject> activeAsteroids = new List<GameObject>();

    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);
            Vector2 spawnPosition = new Vector2(rand, 9f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            activeAsteroids.Add(meteor);
            Destroy(meteor, maxTimeLife);

            speed += 0.2f;
        }
        MoveAsteroids();
    }

    void MoveAsteroids()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject meteor = activeAsteroids[i];
            if (meteor != null)
            {
                meteor.transform.Translate(Vector3.down * speed * Time.deltaTime);

                if (meteor.transform.position.y < -10f)
                {
                    Destroy(meteor);
                    activeAsteroids.RemoveAt(i);
                }
            }
            else
            {
                activeAsteroids.RemoveAt(i);
            }
        }
    }
}
