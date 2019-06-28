using UnityEngine;
using SWNetwork;

public class PlayerAim : MonoBehaviour
{
    public Vector2 TurnSpeed = new Vector2(1, 1);
    public float MaxVerticleView = 60f;

    // Orientation state.
    Vector2 currentAngles = new Vector2();

    Camera cam;
    Player player;

    // Aim settings
    RaycastHit shootHit;
    public float range = 100f;
    public LayerMask shootableMask;

    // Target
    public Vector3 aimPoint;
    public GameObject target;

    NetworkID networkID;

    private void Awake()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
        networkID = GetComponent<NetworkID>();
    }

    void EnableFPSCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void DisableFPSCursor()
    {
        Cursor.visible = true; ;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        //EnableFPSCursor();
    }

    void OnEnable()
    {
        //EnableFPSCursor();
    }

    void OnDisable()
    {
        //DisableFPSCursor();
    }

    void Update()
    {
        if (player.Dead || !networkID.IsMine)
        {
            return;
        }

        Vector2 motion = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"));

        motion = Vector2.Scale(motion, TurnSpeed);

        currentAngles += motion;

        float verticleAngle = -Mathf.Clamp(currentAngles.y, -MaxVerticleView, MaxVerticleView);

        cam.transform.localRotation = Quaternion.Euler(verticleAngle, currentAngles.x, 0);

        transform.localRotation = Quaternion.Euler(0, currentAngles.x, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out shootHit, range, shootableMask, QueryTriggerInteraction.Ignore))
        {
            aimPoint = shootHit.point;
            target = shootHit.transform.gameObject;
        }
        else
        {
            aimPoint = ray.origin + ray.direction * range;
            target = null;
        }
    }
}
