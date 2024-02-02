using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GhostRecorder : MonoBehaviour
{
    private int recordFrequency = 70;
    public GhostData newGhost;

    private float timer;
    private float timeValue;

    public bool isRecording;

    private string filePath = "GhostData.dat";

    //debug i;
    int i = 0;
    GhostData debugGhost;
    private void Awake()
    {
        timeValue = 0;
        timer = 0;
        


        CreateNewGhost();
    }

    private void Start()
    {
        isRecording = true;
        timeValue = 0;
        timer = 0;

        
    }

    public void CreateNewGhost()
    {
        newGhost = new GhostData(new List<float>(), new List<Vector3>(), new List<Quaternion>());

        newGhost.ResetData();
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if (isRecording && timer >= 1f / recordFrequency)
        {
            newGhost.timeStamp.Add(timeValue);
            newGhost.position.Add(transform.position);
            newGhost.rotation.Add(transform.rotation);

            timer = 0;
        }
    }

    public GhostData GetGhostData()
    {
        return newGhost;
    }

    public void WriteGhostDataToBinary()
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, newGhost);
        }
        Debug.Log("Ghost data has been written to binary file: " + filePath);
    }
}
