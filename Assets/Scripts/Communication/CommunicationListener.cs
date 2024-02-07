using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CommunicationListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadString();
    }

    static void ReadString()
    {
        string path = "Assets/test.txt";

        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
