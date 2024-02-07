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
    public List<float> newestTimeStamp = new List<float>();

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


        //I dont think I need this anymore. As it has been moved to GameManager.
        
        newestPosition = new List<Vector3>(ghostData.position);
        newestRotation = new List<Quaternion>(ghostData.rotation);
        newestTimeStamp = new List<float>(ghostData.timeStamp);
        
    }
    public void ReplayMovement()
    {
        //this isnt working.
        timeValue = 0f;
        GetIndex();
        SetTransform();
    }

    /// <summary>
    /// IDEA: have a bool condintinue where if the player leaves the white zone then it will start playing and then at the end of the list it will wait. it will
    /// beucase the ghost has the data in the GameObject so we can use that.
    /// </summary>
    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

         
        GetIndex();
        SetTransform();
    }

    //linear interpolation stuff
    private void GetIndex()
    {
        for (int i = 0; i < newestTimeStamp.Count - 2; i++)
        {
            if (newestTimeStamp[i] == timeValue)
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


    //Debug testing in GameManger
    //going to try pass info from GameManager into this and see what happens. So i can get GameObject and stuff.
    public void TESTGetIndex(GhostData ghostData)
    {
        for (int i = 0; i < ghostData.timeStamp.Count - 1; i++)
        {
            if (Mathf.Approximately(ghostData.timeStamp[i], timeValue))
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghostData.timeStamp[i] < timeValue && timeValue < ghostData.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = newestTimeStamp.Count - 1;
        index2 = newestTimeStamp.Count - 1;

    }

    public void TESTSetTransform(GhostData ghostData)
    {
        if (index1 == index2)
        {
            transform.position = ghostData.position[index1];
            transform.rotation = ghostData.rotation[index1];
        }
        else
        {
            float interpolationFactor = (timeValue - newestTimeStamp[index1]) / (ghostData.timeStamp[index2] - ghostData.timeStamp[index1]);

            transform.position = Vector3.Lerp(ghostData.position[index1], ghostData.position[index2], interpolationFactor);
            transform.rotation = Quaternion.Slerp(ghostData.rotation[index1], ghostData.rotation[index2], interpolationFactor);
        }
    }
}
