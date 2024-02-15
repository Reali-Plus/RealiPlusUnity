using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataConversion : MonoBehaviour
{
    //[SerializeField] List<GameObject> Hand = new List<GameObject>();

    public Vector3 receiveData = new Vector3(); //ici je vais recevoir une coordonnée
    public GameObject indexFinger;

    public struct HandData
    {
        public Vector3 Thumb;
        public Vector3 Index;
        public Vector3 Middle;
        public Vector3 Ring;
        public Vector3 Pinky;
    }

    HandData handData = new HandData();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handData.Index = receiveData;
        indexFinger.transform.position = handData.Index;
        print(handData.Index);
    }
    public void SetFingerPosition(Vector3 newPosition)
    {
        handData.Index = newPosition;
        //indexFinger.transform.position = handData.Index;
    }

}
