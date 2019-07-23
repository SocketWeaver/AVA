using UnityEngine;
using SWNetwork;

public class Health : MonoBehaviour
{
    public int MaxHp = 100;

    const string HEALTH = "Health";
    SyncPropertyAgent syncPropertyAgent;

    void Start()
    {
        syncPropertyAgent = GetComponent<SyncPropertyAgent>();
    }

    public void OnHealthReady()
    {
        int version = syncPropertyAgent.GetPropertyWithName(HEALTH).version;

        if(version == 0)
        {
            syncPropertyAgent.Modify(HEALTH, MaxHp);
        }
    }

    public void OnHealthChanged()
    {
        int currentHP = syncPropertyAgent.GetPropertyWithName(HEALTH).GetIntValue();
        HPUpdated(currentHP);
    }

    public void GotHit(int damage)
    {
        int currentHP = syncPropertyAgent.GetPropertyWithName(HEALTH).GetIntValue();

        if (currentHP == 0)
        {
            return;
        }

        currentHP = Mathf.Max(currentHP - damage, 0);

        syncPropertyAgent.Modify(HEALTH, currentHP);
    }

    public virtual void HPUpdated(int hp) {

    }
}
