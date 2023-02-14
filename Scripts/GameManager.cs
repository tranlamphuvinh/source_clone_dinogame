using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float initialGameSpeed = 7f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI noteText;
    public Button retryButton;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    private float score;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else
        {
            DestroyImmediate(Instance);
        }
    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;    
        }
    }
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();   
        spawnManager = FindObjectOfType<SpawnManager>();

        Time.timeScale = 0;

        playerController.gameObject.SetActive(false);
        spawnManager.gameObject.SetActive(false);

        noteText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }
    public void NewGame()
    {
        
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();   

        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        playerController.gameObject.SetActive(true);    
        spawnManager.gameObject.SetActive(true);

        noteText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }
    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        playerController.gameObject.SetActive(false);
        spawnManager.gameObject.SetActive(false);

        noteText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 1f;
            NewGame();  
        }
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }
    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        if(score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
