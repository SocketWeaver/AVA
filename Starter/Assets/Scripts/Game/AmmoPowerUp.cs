using UnityEngine;

public class AmmoPowerUp : PowerUp
{
    public int WeaponIndex;
    public int Ammo;

    public override void Consume(Collider other)
    {
        PlayerWeapon playerWeapon = other.gameObject.GetComponent<PlayerWeapon>();

        if (playerWeapon != null)
        {
            Gun gun = playerWeapon.Guns[WeaponIndex];

            gun.AddAmmo(Ammo);

            Destroy(gameObject);
        }
    }
}
