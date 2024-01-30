using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostData 
{
    public List<float> timeStamp = new List<float>();
    public List<Vector3> position = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();

    /*
    public List<float> timeStamp;
    public List<Vector3> position;
    public List<Quaternion> rotation;
    */

    public void ResetData()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }
}
