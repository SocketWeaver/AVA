using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class PlayerWeapon : MonoBehaviour
{
    public Constants.Guns DefaultGunType = Constants.Guns.Laser;

    public List<Gun> Guns;

    Player player;
    Gun curretnGun;

    NetworkID networkID;
    SyncPropertyAgent syncPropertyAgent;

    const string CURRENT_WEAPON_INDEX = "CurrentWeaponIndex";
    const string SHOOTING = "Shooting";
    bool lastShootingState = false;

    private void Start()
    {
        player = GetComponent<Player>();
        ActivateWeapon(DefaultGunType);
        networkID = GetComponent<NetworkID>();
        syncPropertyAgent = GetComponent<SyncPropertyAgent>();
    }

    void Update()
    {
        if (player.Dead)
        {
            return;
        }

        if (syncPropertyAgent.GetPropertyWithName(SHOOTING).GetBoolValue())
        {
            curretnGun.Fire();
        }

        if (networkID.IsMine)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                syncPropertyAgent.Modify(CURRENT_WEAPON_INDEX, (int)Constants.Guns.Laser);
                //ActivateWeapon(Constants.Guns.Laser);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                syncPropertyAgent.Modify(CURRENT_WEAPON_INDEX, (int)Constants.Guns.MachineGun);
                //ActivateWeapon(Constants.Guns.MachineGun);
            }

            bool shooting = Input.GetButton("Fire1");

            if(shooting != lastShootingState)
            {
                syncPropertyAgent.Modify(SHOOTING, shooting);
                lastShootingState = shooting;
            }
        }
    }

    public void OnCurrentWeaponIndexChanged()
    {
        Constants.Guns gunType = (Constants.Guns)syncPropertyAgent.GetPropertyWithName(CURRENT_WEAPON_INDEX).GetIntValue();
        if(gunType == 0)
        {
            syncPropertyAgent.Modify(CURRENT_WEAPON_INDEX, (int)DefaultGunType);
        }
        ActivateWeapon(gunType);
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
