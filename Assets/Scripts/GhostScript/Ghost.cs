using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //do not delete this
public class GhostData 
{
    public List<float> timeStamp = new List<float>();
    public List<Vector3> position = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();

    public GhostData(List<float> _timeStamp, List<Vector3> _position, List<Quaternion> _rotation)
    {

        timeStamp = new List<float>(_timeStamp);
        position = new List<Vector3>(_position);
        rotation = new List<Quaternion>(_rotation);
    }

    public void ResetData()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }
}
