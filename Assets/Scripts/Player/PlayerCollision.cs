using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static int STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS;

    [SerializeField]
    private Transform spawnPoint;

    private ReplayController replayController;
    private void Awake()
    {
        replayController = FindObjectOfType<ReplayController>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishBox"))
        {

            STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS += 1;

            replayController.ScriptableObjectReplayer();

            print(gameObject.name);

            this.gameObject.transform.position = spawnPoint.position;
        }
    }

}
