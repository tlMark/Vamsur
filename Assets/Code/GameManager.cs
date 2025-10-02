using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
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
    public int[] nextExp = { 7, 14, 21, 28, 35, 42, 49, 56, 63, 70 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public GameResult uiResult;
    public GameObject enemyCleaner;
    public Transform uiJoystick;

    void Awake()
    {
        instance = this;
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

    public void GameStart(int id)
    {
        health = maxHealth;

        playerId = id;
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);

        Resum();

        AudioManger.instance.PlayBgm(true);
        AudioManger.instance.PlaySfx(AudioManger.SFX.Select);
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

        AudioManger.instance.PlayBgm(false);
        AudioManger.instance.PlaySfx(AudioManger.SFX.Lose);
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

        AudioManger.instance.PlayBgm(false);
        AudioManger.instance.PlaySfx(AudioManger.SFX.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
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
        uiJoystick.localScale = Vector3.zero;
    }

    public void Resum()
    {
        isLive = true;
        Time.timeScale = 1f;
        uiJoystick.localScale = Vector3.one;
    }
}
