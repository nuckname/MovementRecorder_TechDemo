using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayRecording : MonoBehaviour
{
    private GhostData ghostData;

    private float timeValue;

    //linear interpolation variables
    private int index1;
    private int index2;

    bool isReplay;

    [SerializeField]
    private int newestGhostIndex;

    [SerializeField]
    private List<float> newestTimeStamp = new List<float>();

    [SerializeField]
    private List<Vector3> newestPosition = new List<Vector3>();

    [SerializeField]
    private List<Quaternion> newestRotation = new List<Quaternion>();

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
        
        GhostData originalGhostData = ghostRecorder.GetGhostData();

        ghostData = new GhostData(originalGhostData.gameObjectGhost, originalGhostData.ghostIndex, originalGhostData.timeStamp, originalGhostData.position, originalGhostData.rotation);


        newestGhostIndex = ghostData.ghostIndex + 1;
        print(ghostData.ghostIndex);

        newestPosition = new List<Vector3>(ghostData.position);
        newestRotation = new List<Quaternion>(ghostData.rotation);
        newestTimeStamp = new List<float>(ghostData.timeStamp);


    }


    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        GetIndex();
        SetTransform();
    }

    //linear interpolation stuff
    private void GetIndex()
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

        index1 = newestTimeStamp.Count - 1;
        index2 = newestTimeStamp.Count - 1;
        
    }

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
