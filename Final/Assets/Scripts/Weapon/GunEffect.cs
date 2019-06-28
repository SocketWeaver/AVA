using UnityEngine;
using System.Collections;

public class GunEffect : MonoBehaviour
{
    public ParticleSystem GunParticles;
    public LineRenderer GunLine;
    public Light GunLight;
    public Light FaceLight;
    public GameObject BulletHitParticleEffect;

    public void StopEffects()
    {
        // Lights
        FaceLight.enabled = false;
        GunLight.enabled = false;

        // Bullet line
        GunLine.enabled = false;
    }

    public void PlayEffects(Vector3 targetPosition)
    {
        // Lights
        GunLight.enabled = true;
        FaceLight.enabled = true;

        // Bullet line
        GunLine.enabled = true;
        GunLine.SetPosition(0, GunLine.transform.position);
        GunLine.SetPosition(1, targetPosition);

        // Particle effect
        GunParticles.Stop();
        GunParticles.Play();

        if (BulletHitParticleEffect != null)
        {
            Instantiate(BulletHitParticleEffect, targetPosition, Quaternion.identity);
        }
    }

}
