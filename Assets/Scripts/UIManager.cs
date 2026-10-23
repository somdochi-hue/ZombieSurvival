using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;

    public void OEnable()
    {
        SetAmmoText(0, 0);
    }

    public void SetAmmoText(int magAmmo, int remainAmoo)
    {
        ammoText.text = $"{magAmmo} / {remainAmoo}";
    }
}
