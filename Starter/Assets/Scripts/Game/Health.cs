using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHp = 100;
    int currentHP = 0;

    void Start()
    {
        currentHP = MaxHp;
    }

    public void GotHit(int damage)
    {
        if (currentHP == 0)
        {
            return;
        }

        currentHP = Mathf.Max(currentHP - damage, 0);

        HPUpdated(currentHP);
    }

    public virtual void HPUpdated(int hp) {

    }

    public virtual void HPReady(int hp)
    {

    }
}
