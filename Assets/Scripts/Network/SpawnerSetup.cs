using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;


public class SpawnerSetup : MonoBehaviour
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    [SerializeField] static GameObject LocalPlayerInstance;

    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] Transform[] spawnPoints;

    //public List<Transform> possibleSpawns = new List<Transform>();
    //public Transform[] spawnPointsBlue;
    //public Transform[] spawnPointsRed;
    
    private void Start()
    {
        //for (int i = 0; i < spawnPoints.Length; i++)
        //{
        //    possibleSpawns.Add(spawnPoints[i]);
        //}
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnpoint = spawnPoints[randomNumber]; 
        Debug.Log(playerPrefabs.Length);
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);
        
        if (playerPrefabs == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            GameObject tempAvatar = PhotonNetwork.Instantiate(playerToSpawn.name, spawnpoint.transform.position, Quaternion.identity);
            LocalPlayerInstance = tempAvatar; 
        }
    }
}
