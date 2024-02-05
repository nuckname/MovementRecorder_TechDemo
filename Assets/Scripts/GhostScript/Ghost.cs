using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //means i can add it to a list. I think
public class GhostData 
{
    public GameObject gameObjectGhost;
    public int ghostIndex;
    public List<float> timeStamp = new List<float>();
    public List<Vector3> position = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();

    public GhostData(GameObject _gameObjectGhost, int _ghostIndex, List<float> _timeStamp, List<Vector3> _position, List<Quaternion> _rotation)
    {
        gameObjectGhost = _gameObjectGhost;
        //Probs dont need the index number.
        ghostIndex = _ghostIndex;
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
