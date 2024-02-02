using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayRecording : MonoBehaviour
{
    private GhostData ghostData;

    private float timeValue;

    //linear interpolation variables
    //they are set to one so they dont go into a negatibe numebr on the frst loop.
    private int index1 = 3;
    private int index2 = 3;

    bool isReplay;

    
    [SerializeField]
    private List<float> newestTimeStamp = new List<float>();

    [SerializeField]
    private List<Vector3> newestPosition = new List<Vector3>();

    [SerializeField]
    private List<Quaternion> newestRotation = new List<Quaternion>();
    

    /*
    [SerializeField]
    private List<float> newestTimeStamp;

    [SerializeField]
    private List<Vector3> newestPosition;

    [SerializeField]
    private List<Quaternion> newestRotation;
    */

    [SerializeField]
    private GhostRecorder ghostRecorder;


    private void Awake()
    {
        GameObject ghostRecorderObject = GameObject.FindWithTag("Player");

        ghostRecorder = ghostRecorderObject.GetComponent<GhostRecorder>();
        
    }

    private void Start()
    {
        timeValue = 0;
        isReplay = true;

        /*
        newestPosition = ghostRecorder.newGhost.position;
        newestRotation = ghostRecorder.newGhost.rotation;
        newestTimeStamp = ghostRecorder.newGhost.timeStamp;
        */

        //may cause performance issue?

        ghostData = ghostRecorder.GetGhostData();

        newestPosition = ghostData.position;
        newestRotation = ghostData.rotation;
        newestTimeStamp = ghostData.timeStamp;

    }


    void Update()
    {
        timeValue += Time.unscaledDeltaTime;
        if(newestTimeStamp != null)
        {
            GetIndex();
            SetTransform();
        }
        else
        {
            print("newestTimeStamp is null");
        }

    }

    //linear interpolation stuff
    
    private void GetIndex()
    {
        // Check if the lists are not null and if they have elements
        if (newestTimeStamp != null && newestTimeStamp.Count > 0)
        {
            for (int i = 0; i < newestTimeStamp.Count - 1; i++)
            {
                if (Mathf.Approximately(newestTimeStamp[i], timeValue))
                {
                    index1 = i;
                    index2 = i;
                    return;
                }
                else if (newestTimeStamp[i] < timeValue && timeValue < newestTimeStamp[i + 1])
                {
                    index1 = i;
                    index2 = i + 1;
                    return;
                }
            }

            // If no suitable indices are found, set both indices to the last element
            index1 = newestTimeStamp.Count - 1;
            index2 = newestTimeStamp.Count - 1;
        }
        else
        {
            // Handle the case where the lists are null or empty
            Debug.LogError("newestTimeStamp list is null or empty.");
            // You can also throw an exception or handle this case based on your application's requirements
        }
    }
    

    /*
    private void GetIndex()
    {
        for (int i = 0; i < newestPosition.Count - 1; i++)
        {
            //if (ghostRecorder.newGhost.timeStamp[i] == timeValue)
            if (newestTimeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (newestTimeStamp[i] < timeValue & timeValue < newestTimeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = newestTimeStamp.Count - 1;
        index2 = newestTimeStamp.Count - 1;
        
    }
    */


    //linear interpolation lerp math shit.
    private void SetTransform()
    {
        if (index1 == index2)
        {
            transform.position = newestPosition[index1];
            transform.rotation = newestRotation[index1];
        }
        else
        {
            float interpolationFactor = (timeValue - newestTimeStamp[index1]) / (newestTimeStamp[index2] - newestTimeStamp[index1]);

            transform.position = Vector3.Lerp(newestPosition[index1], newestPosition[index2], interpolationFactor);
            transform.rotation = Quaternion.Slerp(newestRotation[index1], newestRotation[index2], interpolationFactor);
        }
    }
}
