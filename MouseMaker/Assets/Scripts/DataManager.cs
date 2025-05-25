using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;
    public GameResultManager gameResultManager;

    public int health;
    public int apple;
    public int block;
    public float time;
    public bool isGoal;
    public float bestscore;

    public static DataManager Instance;

    private void Awake()
    {
        // ΩÃ±€≈Ê
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveDataBeforeSceneChange()
    {
        health = player.health;
        apple = gameManager.appleCount;
        block = gameManager.blockCount;
        isGoal = gameManager.isGoal;
        time = gameManager.limitTime;
    }

    public void SaveScoreBeforeSceneChange()
    {
        bestscore = gameResultManager.bestscore;
    }
}
