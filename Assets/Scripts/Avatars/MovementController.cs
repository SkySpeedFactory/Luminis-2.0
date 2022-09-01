using Photon.Pun;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    private CharacterController controller;
    [SerializeField] ParticleSystem dust;

    private Vector3 movement;
    private Vector3 mousePos;
    [SerializeField] float speed = 5;
    private bool isRunning = false;

    [SerializeField] Camera cam;
    [SerializeField] Transform camTarget;
    [SerializeField] Texture2D tex;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        pv = GetComponent<PhotonView>();
        
        if (pv.IsMine)
        {
            SpawnCamera();
        }
    }
    void Start()
    {
        if (pv.IsMine)
        {
            SetCursor();
        }
        
    }
    void FixedUpdate()
    {
        
        if (pv.IsMine)//if we own photonview(this avatar) execute 
        {
            Movement(); 
        }
    }

    void Movement()
    {
        if (pv.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Vector3 direction = new Vector3(movement.x, 0, movement.z).normalized;
        
            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                controller.Move(Vector3.zero);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                controller.Move(direction * speed* 1.1f * Time.fixedDeltaTime);
            }
            else
            {
                isRunning = false;
            }
        }

        if (isRunning && !dust.isPlaying)
        {
            dust.Play();
            Debug.Log("dust playing");
            if (pv.IsMine)
            {
                pv.RPC("RPC_SendIsRunningOn", RpcTarget.All);
            }
        }
        else
        {
            dust.Stop();
            if (pv.IsMine)
            {
                pv.RPC("RPC_SendIsRunningOff", RpcTarget.All);
            }
        }
    }

    public void SpawnCamera()
    {
        PhotonNetwork.Instantiate(cam.name, this.transform.position + new Vector3(0,7,0) , Quaternion.Euler(90,0,0));
        FollowPlayer.fp.CameraSetup(camTarget);
    }
    void SetCursor()
    {
        //cursor for the time being in movement, will be implemented in shooting script
        CursorMode mode = CursorMode.ForceSoftware;
        Vector2 hotSpot = new Vector2(tex.width / 2,tex.height / 2);// hotspot default is at top left corner
        //tex.SetPixel(tex.width,tex.height, Color.red); doesnt work
        Cursor.SetCursor(tex, hotSpot, mode);
    }
    [PunRPC]
    void RPC_SendIsRunningOn()
    {
        isRunning = true;
    }
    [PunRPC]
    void RPC_SendIsRunningOff()
    {
        isRunning = false;
    }
}
