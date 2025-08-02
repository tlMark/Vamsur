using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { EXP, Level, Kills, Time, Health }

    public InfoType type;

    Text text;
    Slider slider;

    void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.EXP:
                float curexp = GameManager.instance.exp;
                float maxexp = GameManager.instance.nextExp[GameManager.instance.level];
                slider.value = curexp / maxexp;
                break;
            case InfoType.Level:

                break;
            case InfoType.Kills:

                break;
            case InfoType.Time:

                break;
            case InfoType.Health:

                break;
        }
    }
}
