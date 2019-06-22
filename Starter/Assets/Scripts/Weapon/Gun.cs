using UnityEngine;

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

    void DealDamage(GameObject go)
    {
        Health health = go.GetComponent<Health>();
        if (health != null)
        {
            health.GotHit(Damage);
        }
    }

    public void AddAmmo(int count)
    {
        Ammo = Ammo + count;
        guiManager.SetWeaponAmmo(GunType, Ammo);
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
        guiManager.SetWeaponAcive(GunType, true);
    }

    void OnDisable()
    {
        guiManager.SetWeaponAcive(GunType, false);
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
}
