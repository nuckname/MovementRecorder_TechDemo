using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private GhostRecorder ghostRecorder;
    private GhostPlayRecording ghostPlayRecording;
    private GhostReplayManager ghostReplayManager;

    bool ignoreFirstLoopExit = true;
    bool ignoreFirstLoopEnter = true;
    private void Awake()
    {
        //nice
        ghostRecorder = FindObjectOfType<GhostRecorder>();
        ghostPlayRecording = FindObjectOfType<GhostPlayRecording>();
        ghostReplayManager = FindObjectOfType<GhostReplayManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishBox"))
        {
            gameManager.SpawningPlayer(gameObject);
            ghostReplayManager.SpawningClones();

            //how do i pass in the movement data from here?
            //is this getting the right ghost Recorder
            ghostRecorder.LoadDataToFile();
            print("end of movement");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ghostRecorder.isRecording = true;

        if (!ignoreFirstLoopExit)
        {
            ghostReplayManager.ReplayAllGhostMovement();
        }
        else
        {
            ignoreFirstLoopExit = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        ghostRecorder.isRecording = false;
        
        if(!ignoreFirstLoopEnter)
        {
            ghostPlayRecording = FindObjectOfType<GhostPlayRecording>();
            ghostPlayRecording.isReplayGhostMovement = false;
        }
        else
        {
            ignoreFirstLoopEnter = false;
        }

    }
}
