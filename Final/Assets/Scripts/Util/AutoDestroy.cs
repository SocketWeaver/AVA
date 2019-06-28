using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public bool DestroyOnInvisible = false;
    public bool DestroyOnCollision = false;
    public bool DestroyOnParticleEffectEnds = false;
    public bool DestroyOnDelayTimesUp = false;
    public float Delay = 3f;

    void Start()
    {
        if (DestroyOnParticleEffectEnds)
        {
            var exp = GetComponent<ParticleSystem>();
            if (exp != null)
            {
                exp.Play();
                DoDestroy(exp.main.duration);
            }
        }

        if (DestroyOnDelayTimesUp)
        {
            DoDestroy(Delay);
        }
    }

    void OnBecameInvisible()
    {
        if (DestroyOnInvisible)
        {
            DoDestroy(0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (DestroyOnCollision)
        {
            DoDestroy(0);
        }
    }

    void DoDestroy(float delay)
    {
        Destroy(gameObject, delay);
    }
}
