using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static int STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS;


    private GameManager gameManager;
    private GhostRecorder ghostRecorder;
    private GhostPlayRecording ghostPlayRecording;

    bool ignoreFirstLoopExit = true;
    bool ignoreFirstLoopEnter = true;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        ghostRecorder = FindObjectOfType<GhostRecorder>();
        
        ghostPlayRecording = FindObjectOfType<GhostPlayRecording>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishBox"))
        {
            gameManager.SpawningPlayer(gameObject);
            gameManager.SpawningClones();
        }
    }

    //some of these varaibles are out of scope on this script.
    private void OnTriggerExit(Collider other)
    {
        ghostRecorder.isRecording = true;

        //not here but storing GameObjects maybe using Ghost.cs
        //a lot of Get / Find stuff will be bad for scaling.
        if (!ignoreFirstLoopExit)
        {
            //this script isnt getting anything as they arent set as active. so it only returns like 1 varaible.
            GameObject[] ghostObjects = GameObject.FindGameObjectsWithTag("Ghost");

            foreach (GameObject ghostObject in ghostObjects)
            {
                //ghostObject.SetActive(true);
                print(ghostObject);

                GhostPlayRecording ghostRecording = ghostObject.GetComponent<GhostPlayRecording>();

                ghostRecording.isReplayGhostMovement = true;
                ghostRecording.ReplayMovement();
            }
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
