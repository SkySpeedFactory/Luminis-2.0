using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject exitPanel;
    private LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            exitPanel.SetActive(!exitPanel.activeSelf);
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
    }
}
