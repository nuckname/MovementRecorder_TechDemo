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

    int ghostCounter = -1;

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
        ghostCounter += 1;

        //newGhost.ResetData();

        newGhost = new GhostData(this.gameObject, ghostCounter, new List<float>(), new List<Vector3>(), new List<Quaternion>());

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


}
