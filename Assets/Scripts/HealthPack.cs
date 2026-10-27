using UnityEngine;
using UnityEngine.Events;

public class HealthPack : MonoBehaviour, IItem
{
    public int healthPack = 50;

    public UnityEvent OnUse {get; set;} = new UnityEvent();

        public void Use(GameObject target)
    {
        LivingEntity Playerhealth = target.GetComponentInChildren<LivingEntity>();
        if (Playerhealth != null)
        {
           Playerhealth.AddHealth(healthPack);
        }
        gameObject.SetActive(false);
        OnUse.Invoke();
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Use(other.gameObject);
        }
    }
}
