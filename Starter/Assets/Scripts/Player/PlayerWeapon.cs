using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Constants.Guns DeafultGunType = Constants.Guns.Laser;

    public List<Gun> Guns;

    Player player;
    Gun curretnGun;

    private void Start()
    {
        player = GetComponent<Player>();
        ActivateWeapon(DeafultGunType);
    }

    void Update()
    {
        if (player.Dead)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ActivateWeapon(Constants.Guns.Laser);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ActivateWeapon(Constants.Guns.MachineGun);
        }

        if (Input.GetButton("Fire1"))
        {
            curretnGun.Fire();
        }
    }

    public void ActivateWeapon(Constants.Guns gunType)
    {
        foreach (Gun gun in Guns)
        {
            if (gun.GunType == gunType)
            {
                gun.gameObject.SetActive(true);
                curretnGun = gun;
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }
}
