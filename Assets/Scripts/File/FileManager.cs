using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; 
public class FileManager : MonoBehaviour
{
    public string filePath = "ghostData.dat"; // Define the file path
    
    public void WriteGhostDataToBinary(GhostData Ghost)
    {
        // Serialize ghost data to binary file
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, Ghost);
        }
        Debug.Log("Ghost data has been written to binary file: " + filePath);
    }
}
