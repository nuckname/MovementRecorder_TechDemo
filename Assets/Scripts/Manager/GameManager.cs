using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject ghost;

    [SerializeField]
    private GameObject player;

    public GhostData ghostData;

    private GhostRecorder ghostRecorder;

    //List<List<GhostData>> allGhostDataPositions = new List<List<GhostData>>();
    List<GhostData> allGhostDataPositions = new List<GhostData>();
    private void Awake()
    {
        ghostRecorder = FindObjectOfType<GhostRecorder>();
    }

    private void Start()
    {
       //this would be ideal however idk how it would work.
        // CreateNewGhost();

        //also need to change the camera rotation.
        Instantiate(player, spawnPoint.position, Quaternion.identity);
    }
    public void SpawningPlayer(GameObject player)
    {
        //spawnPoint.transform.position = player.transform.position;

        player.transform.position = spawnPoint.transform.position;
    }

    public void SpawningClones()
    {
        ghostRecorder = FindObjectOfType<GhostRecorder>();

        //this is fucked.
        //allGhostDataPositions.Add(ghostRecorder.GetGhostData());

        //this is giving errors.
        //ghostRecorder.CreateNewGhost();
               
        Instantiate(ghost, spawnPoint.position, Quaternion.identity);
    }
}
