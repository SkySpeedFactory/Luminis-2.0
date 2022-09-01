using Photon.Pun;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    
    public static FollowPlayer fp;
    [SerializeField] Transform target;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            enabled = false;
            this.gameObject.SetActive(false);
        }
        fp = this;
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, 3);
            }
        }
    }
    public void CameraSetup(Transform target)
    {
        this.target = target;
    }

}
