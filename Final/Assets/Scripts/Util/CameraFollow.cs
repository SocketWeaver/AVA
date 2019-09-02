using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public enum CameraMode{
        FirstPerson,
        ThirdPerson
    }
    public Transform Target;  
    public CameraMode CamMode = CameraMode.ThirdPerson;

    public Vector3 FirstPersonOffset;
    public Vector3 ThirdPersonOffset;

    void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 offset = Vector3.zero;

            switch (CamMode)
            {
                case CameraMode.FirstPerson:
                    offset = FirstPersonOffset;
                    break;
                case CameraMode.ThirdPerson:
                    offset = ThirdPersonOffset;
                    break;
            }

            Vector3 newPosition = Target.TransformPoint(offset);
            transform.position = newPosition;
        }
    }
}
