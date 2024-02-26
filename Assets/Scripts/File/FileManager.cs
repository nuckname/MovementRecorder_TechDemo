using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class FileManager : MonoBehaviour
{
    private string filePath = "GhostData.txt";

    public static int GhostFileIdex = 0;
    public void WriteGhostData(GhostData newGhost)
    {
        print("WriteGhostData");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Ghost Start");

            for (int i = 0; i < newGhost.timeStamp.Count; i++)
            {
                writer.WriteLine(newGhost.timeStamp[i]);
                writer.WriteLine(newGhost.position[i]);
                writer.WriteLine(newGhost.rotation[i]);
            }
            GhostFileIdex += 1;
            writer.WriteLine("Ghost End");
        }
    }

    //called on game starting?
    //GhostPlayRecording
    public void LoadSingleGhostData()
    {
        print("LoadGhostData");

        //GhostData ghostData = new GhostData();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != "End Ghost")
            {
                /*
                // Assuming each line represents a timestamp, position, and rotation
                float timeStamp = float.Parse(line);
                Vector3 position = Vector3.Parse(reader.ReadLine()); // Assuming Vector3 is a custom class or struct

                Vector3 position = New Vector3(reader.ReadLine()); 

                Quaternion rotation = Quaternion.Parse(reader.ReadLine()); // Assuming Quaternion is a custom class or struct
                Quaternion rotation = System.Convert.(reader.ReadLine()); // Assuming Quaternion is a custom class or struct

                ghostData.timeStamp.Add(timeStamp);
                ghostData.position.Add(position);
                ghostData.rotation.Add(rotation);

                // Skip the empty line
                reader.ReadLine();
                */
            }



        }

    }
