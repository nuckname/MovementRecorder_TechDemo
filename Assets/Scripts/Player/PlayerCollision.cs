using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static int STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS;


    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishBox"))
        {
            gameManager.SpawningPlayer(gameObject);
            gameManager.SpawningClones();
        }
    }
}
