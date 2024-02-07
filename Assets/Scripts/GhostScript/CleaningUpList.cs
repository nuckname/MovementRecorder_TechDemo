using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningUpList : MonoBehaviour
{
    /*
    [SerializeField] 
    private Transform spawnPoint;
    */

    //for some reason spawnPoint.transform isnt working. it always equals Vector3(0,0,0) temp fix.
    private Vector3 hardCodedSpawnPoint = new Vector3(4.76f, 3.66f, -3.64f);

    public GhostData RemovePositionsBeforeSpawn(GhostData ghostData)
    {
        for (int i = 0; i <= ghostData.position.Count; i++)
        {
            if (ghostData.position[i] != hardCodedSpawnPoint)
            {
                ghostData.position.RemoveAt(i);
                ghostData.rotation.RemoveAt(i);
                ghostData.timeStamp.RemoveAt(i);
            }
            else
            {
                break;
            }
        }
        if(ghostData.position.Count < 3)
        {
            Debug.LogError("CleaningUpList Basically deleted the whole list ( ͡° ͜ʖ ͡°)");
        }
        

        return ghostData;
    }



}
