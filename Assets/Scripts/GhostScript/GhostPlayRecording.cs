using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayRecording : MonoBehaviour
{
    public GhostData ghostData;
    private float timeValue;

    //linear interpolation variables
    private int index1;
    private int index2;

    bool isReplay;

    /*
    [SerializeField]
    private List<float> newestTimeStamp = new List<float>();

    [SerializeField]
    private List<Vector3> newestPosition = new List<Vector3>();

    [SerializeField]
    private List<Quaternion> newestRotation = new List<Quaternion>();
    */

    [SerializeField]
    private List<float> newestTimeStamp;

    [SerializeField]
    private List<Vector3> newestPosition;

    [SerializeField]
    private List<Quaternion> newestRotation;
    [SerializeField]

    private GhostRecorder ghostRecorder;

    private void Awake()
    {
        GameObject ghostRecorderObject = GameObject.FindWithTag("Player");

        ghostRecorder = ghostRecorderObject.GetComponent<GhostRecorder>();

        if (ghostRecorder != null)
        {
            GhostData _ghostData = ghostRecorder.ghostData;
        }

    }

    private void Start()
    {
        timeValue = 0;

        //just added this.
        //I wont need this.
        if (ghostRecorder.ghostData.position.Count > 0)
        {
            isReplay = true;

            //this is going to tank the fps.
            //when it reaches the end save this to a file?
            //this keeps getting an update position?
            newestPosition = ghostRecorder.ghostData.position;
            newestRotation = ghostRecorder.ghostData.rotation;
            newestTimeStamp = ghostRecorder.ghostData.timeStamp;
        }

    }

    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        //I think while loop was cauing crashes.
        /*
        while(index1 < ghostRecorder.ghostData.timeStamp.Count - 1)
        {
            //GetIndex();
            //SetTransform();
        }
        */

        GetIndex();
        SetTransform();
    }

    //linear interpolation stuff

    //calling the script too many times im guess which causes the game to crash.
    private void GetIndex()
    {
        for (int i = 0; i < newestPosition.Count - 2; i++)
        {
            if (ghostRecorder.ghostData.timeStamp[i] == timeValue)
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
