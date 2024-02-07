using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static int STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS;


    private GameManager gameManager;
    private GhostRecorder ghostRecorder;
    private GhostPlayRecording ghostPlayRecording;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        ghostRecorder = FindObjectOfType<GhostRecorder>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishBox"))
        {
            gameManager.SpawningPlayer(gameObject);
            gameManager.SpawningClones();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ghostPlayRecording = FindObjectOfType<GhostPlayRecording>();
        

        ghostRecorder.isRecording = true;
        ghostPlayRecording.ReplayMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        ghostRecorder.isRecording = false;
    }
}
