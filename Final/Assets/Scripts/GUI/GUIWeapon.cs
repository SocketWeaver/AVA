using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIWeapon : MonoBehaviour
{
    public Text Text;
    public Image Border;
    public Constants.Guns GunType = Constants.Guns.Default;

    public void SetAmmoCount(int value)
    {
        if (Text != null)
        {
            Text.text = value.ToString();
        }
    }

    public void SetWeaponActive(bool value)
    {
        if (Border != null)
        {
            Border.enabled = value;
        }
    }
}
