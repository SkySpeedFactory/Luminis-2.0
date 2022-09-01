using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AvatarController : MonoBehaviour
{
    public PhotonView pv;
    
    [Header("PlayerStats")]
    private float health;
    [SerializeField] float maxHealth = 2f;
    
    [Header("FlashLight")]
    
    [Header("Weapon")]
    [SerializeField] WeaponsController currentWeapon;
    
    [Header("Shooting")]
    public bool isShooting;

    private void Awake()
    {
        if (!pv.IsMine)
        {
            enabled = false;
        }
        if (currentWeapon)
        {
            currentWeapon.Initialize(this);
        }
        health = maxHealth;
    }

    private void Start()
    {
        if (Camera.main == null)
        {
            SceneManager.LoadScene("Lobby");
            PhotonNetwork.LeaveRoom();
        }
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            CalculateShooting();
            CalculateDeath();
        }
    }
    #region Shooting
    private void CalculateShooting()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
        if (!currentWeapon)
        {
            return;
        }
        currentWeapon.isShooting = isShooting;
    }
    #endregion

    public void CalculateDamage(float damage)
    {
        pv.RPC("RPC_CDMG",RpcTarget.All, damage);
    }

    [PunRPC]
    public void RPC_CDMG(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
    public void CalculateDeath()
    {
        if (health <= 0)
        {
            PhotonView.Destroy(gameObject,1f);
            SceneManager.LoadScene("Lobby");
            PhotonNetwork.LeaveRoom();
        }
    }
}
