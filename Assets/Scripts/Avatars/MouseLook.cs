using Photon.Pun;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    
    private Vector2 mouseLook;
    private Vector2 smoothV;

    private readonly float sensitivity = 2.0F;
    private readonly float smoothing = 2.0F;

    private readonly float minRot = -70.0F;
    private readonly float maxRot = +70.0F;

    GameObject character;
    private Vector3 cross;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        character = this.gameObject;
        if (!pv.IsMine)
        {
            enabled = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        if (pv.IsMine)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            LookDirection();
        }
    }

    private void LookDirection()
    {
        if (pv.IsMine)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    return;
                }

                cross = Crosshair.Instance.transform.position;
                cross = new Vector3(hit.point.x, character.transform.position.y, hit.point.z);
                character.transform.LookAt(cross);
            }
        }
    }

    public void SetCharacter(GameObject target)
    {
        character = target;
    }
}
