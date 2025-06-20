using System.Collections;
using UnityEngine;

public class Mark : MonoBehaviour, IMarkable
{

    private Outline outline;
    private Coroutine markCooldownCoroutine;
    private Coroutine markDurationCoroutine;
    public Color originalColor;
    public Color endColor;
    private bool onCooldown;
    private readonly float markDuration = 10.0f;
    private readonly float markCooldown = 2.0f;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        onCooldown = false;
    }

    public void MarkEnemy()
    {
        if (markCooldownCoroutine != null)
        {
            return;
        }

        if (markDurationCoroutine != null)
        {
            StopCoroutine(markDurationCoroutine);
        }

        outline.enabled = true;
        outline.OutlineColor = originalColor;
        markDurationCoroutine = StartCoroutine(FadeMark());
        markCooldownCoroutine = StartCoroutine(MarkCooldown());
    }

    IEnumerator MarkCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(markCooldown);
        markCooldownCoroutine = null;
        onCooldown = false;
    }

    IEnumerator FadeMark()
    {
        yield return new WaitForSeconds(markDuration);

        float t = 0f;
        while (t < markDuration)
        {
            t += Time.deltaTime;
            outline.OutlineColor = Color.Lerp(originalColor, endColor, t / markDuration);
            yield return null;
        }

        outline.enabled = false;
        outline.OutlineColor = originalColor;
        markDurationCoroutine = null;
    }

    public bool IsOnCooldown()
    {
        return onCooldown;
    }
}