using UnityEngine;

public class Trigger_Helper : MonoBehaviour
{
    public GameObject target;
    public float timer;
    private bool active;
    public string keyID;
    private ObjectiveHelper objectiveHelper;

    void Start()
    {
        active = true;
        objectiveHelper = transform.parent.gameObject.GetComponent<ObjectiveHelper>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        objectiveHelper.StartTimer(target, timer);
    }

    void Update()
    {
        if (Inventory.inventory.HasItem(keyID) && active)
        {
            active = false;
            objectiveHelper.EndHelper(target);
        }
    }
}
