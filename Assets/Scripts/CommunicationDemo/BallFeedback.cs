using UnityEngine;

public class BallFeedback : MonoBehaviour
{
    [SerializeField]
    private string communicationTag = "SleeveCommunication";

    private SocketCommunication socketCommunication;

    private void Start()
    {
        socketCommunication = GameObject.FindGameObjectWithTag(communicationTag).GetComponent<SocketCommunication>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HapticsData hapticsData = new HapticsData();
            socketCommunication.SendData(hapticsData);
        }
    }
}