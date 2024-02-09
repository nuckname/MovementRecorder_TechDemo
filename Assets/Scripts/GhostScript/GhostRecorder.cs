using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    private int recordFrequency = 70;
    public GhostData newGhost;

    private float timer;
    private float timeValue;

    public bool isRecording;

    private void Awake()
    {
        timeValue = 0;
        timer = 0;

        CreateNewGhost();
    }

    private void Start()
    {
        isRecording = false;
        timeValue = 0;
        timer = 0;
    }

    public void CreateNewGhost()
    {
        newGhost.ResetData();

        newGhost = new GhostData(new List<float>(), new List<Vector3>(), new List<Quaternion>());

        timer = 0;
        timeValue = 0;
    }

    void Update()
    {
        if(isRecording)
        {
            timer += Time.unscaledDeltaTime;
            timeValue += Time.unscaledDeltaTime;

            if (timer >= 1f / recordFrequency)
            {
                newGhost.timeStamp.Add(timeValue);
                newGhost.position.Add(transform.position);
                newGhost.rotation.Add(transform.rotation);

                timer = 0;
            }
        }
    }


    public GhostData GetGhostData()
    {
        return newGhost;
    }


}
