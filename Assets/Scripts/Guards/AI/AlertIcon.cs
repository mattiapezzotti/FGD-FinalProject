using UnityEngine;

public class AlertIcon : MonoBehaviour
{
    public float showTime = 2f;

    void OnEnable()
    {
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), showTime);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
