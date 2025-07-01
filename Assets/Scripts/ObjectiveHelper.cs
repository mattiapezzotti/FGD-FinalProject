using System.Collections;
using UnityEngine;

public class ObjectiveHelper : MonoBehaviour
{
    private IEnumerator coroutine;


    public void StartTimer(GameObject target, float time)
    {
        coroutine = TutorialTimer(target, time);
        StartCoroutine(coroutine);
    }

    public void EndHelper(GameObject target)
    {
        StopCoroutine(coroutine);
        Outline outline = target.GetComponent<Outline>();
        outline.enabled = false;
    }

    private IEnumerator TutorialTimer(GameObject currentObjective, float time)
    {
        yield return new WaitForSeconds(time);
        Outline outline = currentObjective.GetComponent<Outline>();
        outline.enabled = true;
        outline.OutlineColor = Color.yellow;
    }
}
