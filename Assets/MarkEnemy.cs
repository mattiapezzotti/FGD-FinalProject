using System.Collections;
using UnityEngine;

public class Mark : MonoBehaviour, IMarkable
{

    private Outline outline;
    private Coroutine markCooldownCoroutine;
    private Coroutine markDurationCoroutine;
    public Color originalColor;
    public Color endColor;
    private Animator animator;
    private readonly float markDuration = 10.0f;
    private readonly float markCooldown = 2.0f;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void MarkEnemy()
    {
        if (markCooldownCoroutine != null)
        {
            return;
        }

        Debug.Log("Enemy Marked!");

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
        yield return new WaitForSeconds(markCooldown);
        markCooldownCoroutine = null;
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
}