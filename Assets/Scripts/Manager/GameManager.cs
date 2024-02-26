using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    private Transform spawnPoint;

    [Header("Prefab")]
    [SerializeField]
    private GameObject player;

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
   
    public void ChangeGhostColor(GameObject ghost)
    {
        Renderer ghostRenderer = ghost.GetComponent<Renderer>();

        ghostRenderer.material = currentGhostToKill;

    }
}
