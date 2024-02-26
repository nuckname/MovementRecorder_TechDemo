using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayRecording : MonoBehaviour
{
    //READ IN FROM FILE.
    [SerializeField]
    private GhostData ghostData;
    

    private float timeValue;

    //linear interpolation variables
    private int index1;
    private int index2;
    private bool isReplay;

    private List<GameObject> ghostRecorderObjects = new List<GameObject>();
    
    private int currentIndex = 0;

    public bool isReplayGhostMovement = false;

    [SerializeField]
    private GhostRecorder ghostRecorder;
    
    private void Awake()
    {
        //I think data is being over written.

        //I dont want an array becuase it store it on each gameobjects. So each ghost holds all the arrays data. 

        //ideally this. but doesnt work as there are multiple types of GhostRecorder.
        //(gives error)



        //GameObject ghostRecorderObject = GameObject.FindWithTag("Player");
        //ghostRecorder = ghostRecorderObject.GetComponent<GhostRecorder>();


        //ghostRecorder = FindObjectOfType<GhostRecorder>();

        /*
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("RecordThisGameObject");
        foreach (GameObject obj in foundObjects)
        {
            ghostRecorderObjects.Add(obj);
        }
        */

        //GetNextGhostData();
    }

    private void Start()
    {
        isReplay = true;

        FileManager fileManager = FindObjectOfType<FileManager>();
        fileManager.LoadSingleGhostData();



        //GhostData originalGhostData = ghostRecorder.GetGhostData();
    }

    public void ReplayMovement()
    {
        timeValue = 0f;
        GetIndex();
        SetTransform();
    }

    void Update()
    {
        if (isReplayGhostMovement)
        {
            timeValue += Time.unscaledDeltaTime;
            
            //if ghost timestamp runs out.
            if (timeValue >= ghostData.timeStamp[ghostData.timeStamp.Count - 1])
            {
                timeValue = ghostData.timeStamp[ghostData.timeStamp.Count - 1];
                SetTransform();
                isReplayGhostMovement = false;

            }
            else
            {
                GetIndex();
                SetTransform();
            }
        }
    }
    /*
    private void GetNextGhostData()
    {
        currentIndex++;
        print(currentIndex);
        ghostData = ghostRecorderObjects[currentIndex].GetComponent<GhostRecorder>().GetGhostData();
        ghostData = new GhostData(70, this.gameObject, ghostData.timeStamp, ghostData.position, ghostData.rotation);

        //currentIndex++;
        currentIndex = (currentIndex + 1);
    }
    */
    private void GetIndex()
    {
        for (int i = 0; i < ghostData.timeStamp.Count - 2; i++)
        {
            if (ghostData.timeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghostData.timeStamp[i] < timeValue & timeValue < ghostData.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = ghostData.timeStamp.Count - 1;
        index2 = ghostData.timeStamp.Count - 1;
    }

    private void SetTransform()
    {
        if (index1 == index2)
        {
            this.transform.position = ghostData.position[index1];
            this.transform.rotation = ghostData.rotation[index1];
        }
        else
        {
            float interpolationFactor = (timeValue - ghostData.timeStamp[index1]) / (ghostData.timeStamp[index2] - ghostData.timeStamp[index1]);

            this.transform.position = Vector3.Lerp(ghostData.position[index1], ghostData.position[index2], interpolationFactor);
            this.transform.rotation = Quaternion.Slerp(ghostData.rotation[index1], ghostData.rotation[index2], interpolationFactor);
        }
    }
}
