using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    public PlayerAim playerAim;
    public Transform chest;
    //public Vector3 offset;

    private void LateUpdate()
    {
        chest.LookAt(playerAim.aimPoint);
    }
}
