using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;       //Public variable to store a reference to the player game object
    //public float damping = 0.5f;
    public Vector3 Offset;

    void Start()
    {
        // offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 newPosition = Target.TransformPoint(Offset);
            transform.position = newPosition; //Vector3.Lerp(transform.position, newPosition, damping);
        }
    }
}