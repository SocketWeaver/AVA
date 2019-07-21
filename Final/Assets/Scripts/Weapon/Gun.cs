using UnityEngine;
using SWNetwork;

public class Gun : MonoBehaviour
{
    public float TimeBetweenBullets = 0.2f;
    public float effectDisplayTime = 0.2f;

    public float Range = 100f;
    public LayerMask ShootableMask;
    public int Damage;
    public int Ammo;
    public Constants.Guns GunType = Constants.Guns.Default;

    GUIManager guiManager;
    PlayerAim playerAim;
    GunEffect gunEffect;
    float timer;
    RaycastHit shootHit;

    public void AddAmmo(int count)
    {
        NetworkID networkID = GetComponentInParent<NetworkID>();
        if (networkID != null && networkID.IsMine)
        {
            Ammo += count;
            guiManager.SetWeaponAmmo(GunType, Ammo);
        }
    }

    private void Awake()
    {
        guiManager = FindObjectOfType<GUIManager>();
        playerAim = GetComponentInParent<PlayerAim>();
        gunEffect = GetComponent<GunEffect>();

        AddAmmo(0);
    }

    private void OnEnable()
    {
        NetworkID networkID = GetComponentInParent<NetworkID>();
        if(networkID!= null && networkID.IsMine)
        {
            guiManager.SetWeaponAcive(GunType, true);
        }
    }

    void OnDisable()
    {
        NetworkID networkID = GetComponentInParent<NetworkID>();
        if (networkID != null && networkID.IsMine)
        {
            guiManager.SetWeaponAcive(GunType, false);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= effectDisplayTime)
        {
            gunEffect.StopEffects();
        }
    }

    public void Fire()
    {
        if (timer >= TimeBetweenBullets && Ammo > 0)
        {
            DoFire();
        }
    }

    public void DoFire()
    {
        AddAmmo(-1);

        timer = 0f;

        gunEffect.PlayEffects(playerAim.aimPoint);

        if(playerAim.target != null)
        {
            DealDamage(playerAim.target);
        }
    }

    private void DealDamage(GameObject go)
    {
        Health health = go.GetComponent<Health>();
        if (health != null)
        {
            health.GotHit(Damage);
        }
    }
}
