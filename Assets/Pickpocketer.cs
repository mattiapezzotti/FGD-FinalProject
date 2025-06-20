using UnityEngine;

public interface IPickpocketer
{
    public void Pickpocket();
}

public class Pickpocketer : MonoBehaviour
{
    public Transform source;
    public float range;
    private Ray r;
    private RaycastHit hit;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        r = new (source.position, source.forward);

        if (Input.GetKeyDown(KeyCode.F))
        {

            if (Physics.Raycast(r, out hit, range))
            {
                if (hit.collider.gameObject.TryGetComponent(out IPickpocketer interacted))
                {
                    animator.SetTrigger("Pickpocket");
                    interacted.Pickpocket();
                }
            }
        }
    }
}
