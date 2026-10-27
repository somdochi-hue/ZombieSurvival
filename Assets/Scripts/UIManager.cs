using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;

    public Text scoreText;

    public Text waveInfoText;

    public GameObject gameOverUI;

    public void OEnable()
    {
        SetActiveGameOverUI(false);
    }

    public void SetAmmoText(int magAmmo, int remainAmoo)
    {
        ammoText.text = $"{magAmmo} / {remainAmoo}";
    }

    public void SetwaveTaxt(int wave, int count)
    {
        waveInfoText.text = $"Wave: {wave} \nEnemy Left: {count}";
    }

        public void Setscore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }
}
