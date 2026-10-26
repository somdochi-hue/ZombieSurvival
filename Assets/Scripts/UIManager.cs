using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;

    public Text waveInfoText;

    public void OEnable()
    {
        SetAmmoText(0, 0);
    }

    public void SetAmmoText(int magAmmo, int remainAmoo)
    {
        ammoText.text = $"{magAmmo} / {remainAmoo}";
    }

    public void SetwaveTaxt(int wave, int count)
    {
        waveInfoText.text = $"Wave: {wave} \nEnemy Left: {count}";
    }
}
