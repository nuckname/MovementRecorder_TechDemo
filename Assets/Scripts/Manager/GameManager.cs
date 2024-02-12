using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GhostData ghostData;

    [Header("Spawning")]
    [SerializeField]
    private Transform spawnPoint;

    [Header("Prefab")]
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject ghost;
    
    [Header("Get Scripts")]
    [SerializeField]
    private GhostPlayRecording ghostPlayRecording;

    private GhostRecorder ghostRecorder;

    [Header("Ghost Color")]
    [SerializeField]
    private Material currentGhostToKill;

    [SerializeField]
    private Material doNotKillGhost;


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

    public void SpawningClones()
    {

        ghostRecorder = FindObjectOfType<GhostRecorder>();

        ChangeGhostColor(ghost);
        Instantiate(ghost, spawnPoint.position, Quaternion.identity);
        
        StartCoroutine(DelayedCreation());
    }
    
    // Fixes bug. Waits for a short delay before creating a new ghost recorder so it doesnt replace new ghost data.
    //fix later idk.
    private IEnumerator DelayedCreation()
    {
        yield return new WaitForSeconds(0.01f);

        ghostRecorder.CreateNewGhost();
        
    }
    public void ChangeGhostColor(GameObject ghost)
    {
        Renderer ghostRenderer = ghost.GetComponent<Renderer>();

        ghostRenderer.material = currentGhostToKill;

    }
}
