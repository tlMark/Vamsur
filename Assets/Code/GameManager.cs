using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public float health;
    public int playerId;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 200, 250, 300, 400, 550 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public GameResult uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        health = maxHealth;

        playerId = id;
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        
        Resum();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.ShowResultLose();
        Stop();
    }

    public void GameClear()
    {
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.ShowResultWin();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
        {
            return;
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameClear();
        }
    }

    public void GetExp()
    {
        if (!isLive)
        {
            return;
        }

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0f;
    }
    
    public void Resum()
    {
        isLive = true;
        Time.timeScale = 1f;
    }
}
