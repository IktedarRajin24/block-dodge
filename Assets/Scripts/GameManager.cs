using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public GameObject bomb;
    public GameObject[] fruits;
    public Transform spawnPoint;
    public TextMeshProUGUI tapText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI health;
    public GameObject Title;

    public float spawnRate = 2.0f;
    public bool gameStarted = false;
    private float spawnCooldown = 0.5f; 

    private float maxXPos = 2.0f, minXPos = -2.0f;
    private Dictionary<float, float> recentPositions = new Dictionary<float, float>();

    private void Start()
    {
        healthText.enabled = false;
        health.enabled = false;
        scoreText.enabled = false;
        score.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            healthText.enabled = true;
            health.enabled = true;
            scoreText.enabled = true;
            score.enabled = true;
            tapText.enabled = false;
            Title.SetActive(false);
            StartSpawning();
            gameStarted = true;
        }
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnItem", 0.5f, spawnRate);
        InvokeRepeating("SpawnBomb", 1.5f, spawnRate * 2f);
    }

    private void SpawnItem()
    {
        Vector3 spawnPos = spawnPoint.position;

        
        float spawnX;
        do
        {
            spawnX = Random.Range(minXPos, maxXPos);
        } while (IsPositionRecentlyUsed(spawnX));

        spawnPos.x = spawnX;
        recentPositions[spawnX] = Time.time; 

        if (Random.value < 0.5f)
        {
            Instantiate(block, spawnPos, Quaternion.identity);
        }
        else
        {
            GameObject fruit = fruits[Random.Range(0, fruits.Length)];
            Instantiate(fruit, spawnPos, Quaternion.identity);
        }
    }

    private void SpawnBomb()
    {
        Vector3 spawnPos = spawnPoint.position;

        float spawnX;
        do
        {
            spawnX = Random.Range(minXPos, maxXPos);
        } while (IsPositionRecentlyUsed(spawnX));

        spawnPos.x = spawnX;
        recentPositions[spawnX] = Time.time; 

        Instantiate(bomb, spawnPos, Quaternion.identity);
    }

    
    private bool IsPositionRecentlyUsed(float position)
    {
        if (recentPositions.ContainsKey(position))
        {
            float lastUsedTime = recentPositions[position];
            if (Time.time - lastUsedTime < spawnCooldown)
            {
                return true; 
            }
        }
        return false; 
    }
}
