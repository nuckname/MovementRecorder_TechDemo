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
    private List<GhostData> PreviousGhostRecordings = new List<GhostData>();
    private void Start()
    {
        //Note: also need to change the camera rotation.
        Instantiate(player, spawnPoint.position, Quaternion.identity);
    }
    public void SpawningPlayer(GameObject player)
    {
        player.transform.position = spawnPoint.transform.position;
    }

    public void SpawningClones()
    {
        ReplayAllOldGhostMovement();

        ghostRecorder = FindObjectOfType<GhostRecorder>();
        Instantiate(ghost, spawnPoint.position, Quaternion.identity);
        
        StartCoroutine(DelayedCreation());
    }
    
    // Fixes bug. Waits for a short delay before creating a new ghost recorder so it doesnt replace new ghost data.
    private IEnumerator DelayedCreation()
    {
        yield return new WaitForSeconds(0.01f);

        //bad for performances.
        PreviousGhostRecordings.Add(ghostRecorder.GetGhostData());

        //giving an error
        //ghostRecorder.WriteGhostDataToBinary();

        ghostRecorder.CreateNewGhost();
    }


    /// <summary>
    ///FROM GAME MANAGER CALL GHOST PLAYER SCRIPT AND PASS IN DATA TO REPLAY GHOST.
    ///LIIKE GAME OBJECT AND POS AND STUFF.
    /// </summary>

    //this should be in another script. however performance :/
    private void ReplayAllOldGhostMovement()
    {
        foreach (GhostData ghostData in PreviousGhostRecordings)
        {
            Debug.Log("Ghost Index:" + ghostData.ghostIndex);

            for (int i = 0; i < ghostData.timeStamp.Count; i++)
            {
                Debug.Log("Time Stamp index:" + ghostData.ghostIndex + ": " + ghostData.timeStamp[i]);
                //ghostData.gameObjectGhost.transform.position = ghostData.timeStamp[i];

                //I should call GhostPlayRecordings.cs
            }

            for (int i = 0; i < ghostData.position.Count; i++)
            {
                Debug.Log("Position index:" + ghostData.ghostIndex + ": " + ghostData.position[i]);
            }

            for (int i = 0; i < ghostData.rotation.Count; i++)
            {
                Debug.Log("Rotation index:" + ghostData.ghostIndex + ": " + ghostData.rotation[i]);
            }
        }
       
    }
}
