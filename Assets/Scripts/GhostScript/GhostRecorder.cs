using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    public GhostData newGhost;

    private float timer;
    private float timeValue;

    public bool isRecording;

    public GhostRecorder ghostRecorder;

    string filePath = "Assets/GhostData.json";
    private void Awake()
    {
        CreateNewGhost();
    }


    public void CreateNewGhost()
    {
        newGhost.ResetData();
        print("created new ghost");

        newGhost = new GhostData(70, this.gameObject, new List<float>(), new List<Vector3>(), new List<Quaternion>());
        
        timer = 0;
        timeValue = 0;
    }


    void Update()
    {
        if(isRecording)
        {
            timer += Time.unscaledDeltaTime;
            timeValue += Time.unscaledDeltaTime;

            if (timer >= 1f / newGhost.frequencyRecording)
            { 

                newGhost.timeStamp.Add(timeValue);
                
                newGhost.position.Add(transform.position);
                newGhost.rotation.Add(transform.rotation);

                timer = 0;
            }
        }
    }

    public void LoadDataToFile()
    {
        FileManager fileManager = FindObjectOfType<FileManager>();
        fileManager.WriteGhostData(newGhost);
    }

    public GhostData GetGhostData()
    {
        return newGhost;
    }


}
