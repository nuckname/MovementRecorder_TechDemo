using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReplayManager : MonoBehaviour
{

    [Header("Spawning")]
    [SerializeField]
    private Transform spawnPoint;

    [Header("Prefab")]
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject ghostPlayer;

    [SerializeField]
    private GameObject ghostBullet;

    private GhostRecorder ghostRecorder;

    //just to debug.
    public GameObject[] ghostObjects;
    
    public GameObject[] foundObjects;

    private void Awake()
    {
        ghostRecorder = FindObjectOfType<GhostRecorder>();

        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("RecordThisGameObject");
    }

    public void ReplayAllGhostMovement()
    {
        ReplaySystem("Ghost");
    }

    private void ReplaySystem(string tag)
    {
        //this may cause scaling issues.
        ghostObjects = GameObject.FindGameObjectsWithTag(tag);
        //ghostData.ghostGameObject
        foreach (GameObject ghostObject in ghostObjects)
        {
            GhostPlayRecording ghostPlayRecording = ghostObject.GetComponent<GhostPlayRecording>();

            ghostPlayRecording.isReplayGhostMovement = true;
            ghostPlayRecording.ReplayMovement();
        }
    }

    public void SpawningClones()
    {
        ghostRecorder = FindObjectOfType<GhostRecorder>();

        //ChangeGhostColor(ghost);
        Instantiate(ghostPlayer, spawnPoint.position, Quaternion.identity);

        //amount for bullet counter. 
        //spawnPoint.positoin = muzzle pos?
        Instantiate(ghostBullet, new Vector3(4, 2, 1), Quaternion.identity);

        //would spawn it at pos[0]

        StartCoroutine(DelayedCreation());
    }

    // Fixes bug. Waits for a short delay before creating a new ghost recorder so it doesnt replace new ghost data.
    //fix later idk.
    private IEnumerator DelayedCreation()
    {
        yield return new WaitForSeconds(0.01f);

        ghostRecorder.CreateNewGhost();

    }

}
