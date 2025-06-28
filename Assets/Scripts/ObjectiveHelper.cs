using System.Collections;
using UnityEngine;

public class ObjectiveHelper : MonoBehaviour
{

    public void StartTimer(GameObject target, float time)
    {
        StartCoroutine(TutorialTimer(target, time));
    }

    public void End(GameObject target)
    {
        Outline outline = target.GetComponent<Outline>();
        outline.enabled = false;
    }

    IEnumerator TutorialTimer(GameObject currentObjective, float time)
    {
        yield return new WaitForSeconds(time);
        Outline outline = currentObjective.GetComponent<Outline>();
        outline.enabled = true;
        outline.OutlineColor = Color.yellow;
    }
}
