using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IItem
{
    public int coin = 50;

    private GameManager gameManager;
    public UnityEvent OnUse {get; set;} = new UnityEvent();

    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }
    public void Use(GameObject target)
    {
        gameObject.SetActive(false);
        OnUse.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Use(other.gameObject);
            gameManager.AddScore(coin);
        }
    }
}
