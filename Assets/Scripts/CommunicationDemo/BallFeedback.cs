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
            HapticsData haptics = new HapticsData();
            haptics.AddFeedback(new FingerFeedback(0, true, true));
            socketCommunication.SendData(haptics);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HapticsData haptics = new HapticsData();
            haptics.AddFeedback(new FingerFeedback(0, false, false));
            socketCommunication.SendData(haptics);
        }
    }
}