using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    private int namingIndex = 0;

    private void Start()
    {
        ResetValueScripableObjects();
        DeleteScriptableObjects();

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
        CreateNewRecorderGhostScriptableObject();

        //would change this if statment condition.
        if (PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS >= 1 && PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS <= 5)
        {
            /* Have one scriptable object record.
             * then the other sscriptable objets is just passed the data. with a isReplay.
             * 
             * I have Ghost.cs Reset()
             * 
             */



            //just manual make true.
            player5.SetActive(true);
            playerClonesGameObjects[5].SetActive(true);

            int index = PlayerCollision.STATIC_INDEX_COUNTER_FOR_PLAYER_SPAWNS - 1;
            print("index counter: " + index);
            //will spawn game objects in later in with the spawn point but is in update.

            //sets the next clone to record. 
            print(cloneScriptableObjects[index] + " replay is set to true");
            cloneScriptableObjects[index].isReplay = true;

            //saving the clone position.
            //cloneScriptableObjects[index + 1].position = cloneScriptableObjects[0].position;
            //current index
            print(cloneScriptableObjects[index + 1] + " is record = true");
            cloneScriptableObjects[index + 1].isRecord = true;


            //attempting to pass data through. measure fps and graph
            cloneScriptableObjects[0].position = cloneScriptableObjects[index].position;
            cloneScriptableObjects[0].rotation = cloneScriptableObjects[index].rotation;
            cloneScriptableObjects[0].timeStamp = cloneScriptableObjects[index].timeStamp;

            //current idex
            print(playerClonesGameObjects[index] + " is set to active true");
            playerClonesGameObjects[index].SetActive(true);

            //to not get null error.
            if(index >= 1)
            {
                print(cloneScriptableObjects[index - 1] + " i Record = false");
                cloneScriptableObjects[index - 1].isRecord = false;
            }
        }
    }

    private string CreatingScriptableObjectPath()
    {
        string fileName = $"NewGhost {namingIndex}.asset";
        namingIndex++;

        string folderPath = "Assets/";

        string assetPath = folderPath + "/" + fileName;
        return assetPath;
    }

    private void ResetValueScripableObjects()
    {
        for (int i = 0; i < cloneScriptableObjects.Length; i++)
        {
            cloneScriptableObjects[i].isRecord = false;
            cloneScriptableObjects[i].isReplay = false;
        }
    }



    //set this to the player and then pass the data to another scriptable object.
    private void CreateNewRecorderGhostScriptableObject()
    {
        Ghost newGhost = ScriptableObject.CreateInstance<Ghost>();
        //get the amount of newGhots created.
        //save this varaibe so it isnt delete after runtime stops.

        newGhost.isRecord = true;
        newGhost.isReplay = false;
        newGhost.recordFrequency = 70f;

        newGhost.timeStamp = new List<float>();
        newGhost.position = new List<Vector3>();
        newGhost.rotation = new List<Quaternion>();

        string assetPath = CreatingScriptableObjectPath();
        AssetDatabase.CreateAsset(newGhost, assetPath);
    }

    private void DeleteScriptableObjects()
    {
        //perfect.

        /*
        bool loop = true;
        while(loop)
        try
        {
            AssetDatabase.DeleteAsset($"Assets/NewGhost{namingIndex}.asset");
        }
        catch(System.Exception ex)
        {
            loop = false;
            Debug.LogError($"Error deleting ScriptableObject: {ex.Message}");
        }
        */
        //delete each assest based of the new variable created
    }
}
