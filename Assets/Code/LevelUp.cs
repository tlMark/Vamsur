using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        rectTransform.localScale = Vector3.one;
    }

    public void Hide()
    {
        rectTransform.localScale = Vector3.zero;
    }
}
