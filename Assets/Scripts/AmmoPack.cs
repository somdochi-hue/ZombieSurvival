using UnityEngine;
using UnityEngine.Events;

public class AmmoPack : MonoBehaviour, IItem
{
    [Header("총알")]
    public int ammoPack = 30;
    public UnityEvent OnUse {get; set;} = new UnityEvent();

    public void Use(GameObject target)
    {
        Gun PlayerGun = target.GetComponentInChildren<Gun>();
        if (PlayerGun != null)
        {
           PlayerGun.AddammoRemain(ammoPack);
        }
        gameObject.SetActive(false);
        OnUse.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Use(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}