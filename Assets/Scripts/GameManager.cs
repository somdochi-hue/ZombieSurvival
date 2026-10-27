using JetBrains.Annotations;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uIManager;
    public ZombieSpawner zombieSpawner;
    public ItemSpawner itemSpawner;

    private int score = 0;

    public bool IsGameover {get; private set;}

    private void Start()
    {
        uIManager.Setscore(score);
    }

    public void AddScore(int add)
    {
        if(IsGameover)
        {
            return;
        }
        score += add;
        uIManager.Setscore(score);
    }

    public void EndGame()
    {
        IsGameover = true;
        uIManager.SetActiveGameOverUI(true);

        itemSpawner.enabled = false;
        zombieSpawner.OnEndGame();
        itemSpawner.enabled = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
