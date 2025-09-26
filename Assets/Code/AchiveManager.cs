using System;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] LockCharacters;
    public GameObject[] UnlockCharacters;

    enum AchiveType
    {
        Unlock1, Unlock2, Unlock3
    }

    AchiveType[] achives;

    void Awake()
    {
        achives = (AchiveType[])Enum.GetValues(typeof(AchiveType));
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        PlayerPrefs.SetInt("Unlock1", 1);
        PlayerPrefs.SetInt("Unlock2", 1);
        PlayerPrefs.SetInt("Unlock3", 1);
    }
}
