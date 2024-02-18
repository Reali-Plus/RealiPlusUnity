using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataConversion : MonoBehaviour
{
    [SerializeField] private GameObject indexFinger;
    [SerializeField] private float vitesseRotation = 1f;
    [SerializeField] private Vector3 receiveData = new Vector3(); //ici je vais recevoir une coordonnee

    //[SerializeField] private List<GameObject> fingers;
    //[SerializeField] private List<Vector3> receiveData = new List<Vector3>(); 

    public struct HandData
    {
        public Vector3 Thumb;
        public Vector3 Index;
        public Vector3 Middle;
        public Vector3 Ring;
        public Vector3 Pinky;
    }

    HandData handData = new HandData();

    // Update is called once per frame
    void Update()
    {
        handData.Index = receiveData;
        Quaternion rotationVoulue = Quaternion.LookRotation(receiveData.normalized);
        indexFinger.transform.rotation = Quaternion.Slerp(indexFinger.transform.rotation, rotationVoulue, Time.deltaTime * vitesseRotation);
        print(handData.Index);

        //if (receiveData.Count == fingers.Count)
        //{
        //    handData.Thumb = receiveData[0];
        //    handData.Index = receiveData[1];
        //    handData.Middle = receiveData[2];
        //    handData.Ring = receiveData[3];
        //    handData.Pinky = receiveData[4];

        //    for (int i = 0; i < fingers.Count; i++)
        //    {
        //        Quaternion rotationVoulue = Quaternion.LookRotation(receiveData[i].normalized);
        //        fingers[i].transform.rotation = Quaternion.Slerp(fingers[i].transform.rotation, rotationVoulue, Time.deltaTime * vitesseRotation);
        //    }
        //}
        //print("Thumb Position: " + handData.Thumb);
        //print("Index Position: " + handData.Index);
        //print("Middle Position: " + handData.Middle);
        //print("Pinky Position: " + handData.Pinky);
    }
}
