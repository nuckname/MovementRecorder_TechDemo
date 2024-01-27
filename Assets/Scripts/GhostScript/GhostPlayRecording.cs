using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayRecording : MonoBehaviour
{
    public Ghost ghost;
    private float timeValue;

    //linear interpolation variables
    private int index1;
    private int index2;

    private void Awake()
    {
        timeValue = 0;

        //just added this.
        if (ghost.position.Count > 0)
        {
            
            ghost.isReplay = true;

            //saves the data to another scriptable objects so we can replay multi clones at once.
            //how to access this?
            CreateScriptableObject();
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);    
    }

    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        if (ghost.isReplay)
        {
            GetIndex();
            SetTransform();
        }
    }

    void CreateScriptableObject()
    {
        // Create a new instance of your ScriptableObject class
        Ghost newDataContainer = ScriptableObject.CreateInstance<Ghost>();

        newDataContainer.isReplay = true;
        newDataContainer.isRecord = false;   
    }

    //linear interpolation stuff
    private void GetIndex()
    {
        for (int i = 0; i < ghost.timeStamp.Count - 2; i++)
        {
            if (ghost.timeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghost.timeStamp[i] < timeValue & timeValue < ghost.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }
        index1 = ghost.timeStamp.Count - 1;
        index2 = ghost.timeStamp.Count - 1;
    }

    //linear interpolation lerp math shit.
    private void SetTransform()
    {
        if (index1 == index2)
        {
            this.transform.position = ghost.position[index1];
            this.transform.rotation = ghost.rotation[index1];
        }
        else
        {
            float interpolationFactor = (timeValue - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);

            this.transform.position = Vector3.Lerp(ghost.position[index1], ghost.position[index2], interpolationFactor);
            //this.transform.eulerAngles = Vector3.Lerp(ghost.rotation[index1], ghost.rotation[index2], interpolationFactor);
            this.transform.rotation = Quaternion.Slerp(ghost.rotation[index1], ghost.rotation[index2], interpolationFactor);
        }
    }
}
