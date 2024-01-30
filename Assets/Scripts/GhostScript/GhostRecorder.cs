using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GhostRecorder : MonoBehaviour
{
    private int recordFrequency = 70;
    public GhostData ghostData;

    private float timer;
    private float timeValue;

    public bool isRecording;

    private string filePath = "GhostData.dat"; 

    private void Awake()
    {
        timeValue = 0;
        timer = 0;
    }

    private void Start()
    {
        isRecording = true;
        timeValue = 0;
        timer = 0;
        //creating them in gamemanager?
        //save this to a gameManager or another script which saves all data? -> big data.
        CreateNewGhost();
    }

    void CreateNewGhost()
    {
        ghostData = new GhostData();
        ghostData.ResetData();
    }

    void FixedUpdate()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if (isRecording && timer >= 1f / recordFrequency)
        {
            ghostData.timeStamp.Add(timeValue);
            ghostData.position.Add(transform.position);
            ghostData.rotation.Add(transform.rotation);
            timer = 0;
        }
    }

    public GhostData GetGhostData()
    {
        //maybe move somewhere else
        //WriteGhostDataToBinary();

        return ghostData;
    }

    //call this in game manager which is called when play hit the blocks.
    //Order would matter.
    public void WriteGhostDataToBinary()
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, ghostData);
        }
        Debug.Log("Ghost data has been written to binary file: " + filePath);
    }
}
