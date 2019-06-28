using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float RotateSpeed = 20f;

    private void Update()
    {
        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }

    public virtual void Consume(Collider other)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Consume(other);
    }
}
