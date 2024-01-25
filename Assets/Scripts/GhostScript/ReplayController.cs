using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerClonesGameObjects;

    [SerializeField]
    private Ghost[] cloneScriptableObjects;

    [SerializeField]
    private Ghost cloneScript, cloneScript1, cloneScript2, cloneScript3, cloneScript4, cloneScript5;

    [SerializeField]
    private GameObject player, player1, player2, player3, player4, player5;

    private void Start()
    {
        playerClonesGameObjects = new GameObject[6]; 

        playerClonesGameObjects[0] = player;
        playerClonesGameObjects[1] = player1;
        playerClonesGameObjects[2] = player2;
        playerClonesGameObjects[3] = player3;
        playerClonesGameObjects[4] = player4;
        playerClonesGameObjects[5] = player5;

        cloneScriptableObjects = new Ghost[6]; 

        cloneScriptableObjects[0] = cloneScript;
        cloneScriptableObjects[1] = cloneScript1;
        cloneScriptableObjects[2] = cloneScript2;
        cloneScriptableObjects[3] = cloneScript3;
        cloneScriptableObjects[4] = cloneScript4;
        cloneScriptableObjects[5] = cloneScript5;
    }

    //called when player collision with finish box
    public void ScriptableObjectReplayer()
    {
        if (PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS >= 1 && PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS <= 5)
        {
            int index = PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS;
            print(index);
            //will spawn game objects in later in with the spawn point but is in update.

            //sets the next clone to record. 
            cloneScriptableObjects[index].isReplay = true;
            


            //current index
            cloneScriptableObjects[index + 1].isRecord = true;
            //current idex
            playerClonesGameObjects[index].SetActive(true);
                
            //to not get null error.
            if(index > 1)
            {
                cloneScriptableObjects[index - 1].isRecord = false;
            }
        }
    }
}
