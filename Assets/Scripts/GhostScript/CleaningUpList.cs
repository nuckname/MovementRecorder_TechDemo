using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningUpList : MonoBehaviour
{
    // this is so that each GhostData variable will start at the spawn points poition.

    private Vector3 GhostStartingPosition;
    
    [SerializeField]
    private Vector3 SpawnPoint;
    private void Start()
    {
        GhostStartingPosition = SpawnPoint;
    }

}
