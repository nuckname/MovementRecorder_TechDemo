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

    private GhostRecorder ghostRecorder;
    [SerializeField]
    private CleaningUpList cleaningUpList;

    [SerializeField]
    private List<GhostData> PreviousGhostRecordings = new List<GhostData>();

    [SerializeField]
    private GhostPlayRecording ghostPlayRecording;

    private GhostData currentGhostData;
    private GhostData shortenGhostData;

    private void Awake()
    {
        ghostPlayRecording = FindObjectOfType<GhostPlayRecording>();
    }
    private void Start()
    {
        //Note: also need to change the camera rotation.
        Instantiate(player, spawnPoint.position, Quaternion.identity);
    }
    public void SpawningPlayer(GameObject player)
    {
        player.transform.position = spawnPoint.transform.position;
    }

    private GhostData ShortenGhostData(GhostData ghostData)
    {
        return cleaningUpList.RemovePositionsBeforeSpawn(ghostData);
    }

    public void SpawningClones()
    {

        ghostRecorder = FindObjectOfType<GhostRecorder>();

        //currentGhostData = ghostRecorder.GetGhostData();
        
        Instantiate(ghost, spawnPoint.position, Quaternion.identity);
        
        //ghostRecorder.CreateNewGhost();

        //need to somehow remove this however it makes all the List<GhostData> null in the gameObject.
        StartCoroutine(DelayedCreation());
    }
    
    // Fixes bug. Waits for a short delay before creating a new ghost recorder so it doesnt replace new ghost data.
    //fix later idk.
    private IEnumerator DelayedCreation()
    {
        yield return new WaitForSeconds(0.01f);

        //bad for performances.
        //currentGhostData = ghostRecorder.GetGhostData();

        ghostRecorder.CreateNewGhost();
        
    }
}
