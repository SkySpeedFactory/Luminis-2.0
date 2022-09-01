using Photon.Pun;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    private AvatarController _avatarController;
    [SerializeField] PhotonView pv;
    private bool isInitialized;
    
    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    //public GameObject shellPrefab; -----------> Not Implented 
    //public Transform shellEjectionPort;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    private float nextFire;
    [SerializeField] float bulletLifeTime;
    public bool isShooting;
    
    public void Initialize(AvatarController AvatarController)
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            enabled = false;
        }
        _avatarController = AvatarController;
        isInitialized = true;
    }
    void FixedUpdate()
    {
        if (!isInitialized)
        {
            return;
        }

        if (pv.IsMine)
        {
            CalculateShooting();
        }
    }

    private void CalculateShooting()
    {
        pv.RPC("RPC_CS",RpcTarget.All);
    }
    [PunRPC]
    private void RPC_CS()
    {
        if (isShooting && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation);
            Bullet_9mm bulletScript = bullet.GetComponent<Bullet_9mm>();
            if (bulletScript)
            {
                bulletScript.Initialize(bulletSpawnPoint, bulletSpeed);
            }
            Destroy(bullet, bulletLifeTime);
            //Destroy(muzzleFlash, 0.2f);
        }
    }
}
