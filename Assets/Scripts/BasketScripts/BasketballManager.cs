using UnityEngine;

public class BasketballManager : MiniGameManager
{
    [SerializeField]
    private GrabbableBall grabbableBall;

    [SerializeField] private GameObject playerHead;

    [SerializeField] private Vector3 playerHeadAngle = Vector3.zero;
    [SerializeField] private Vector3 playerHeadPosition = Vector3.zero;

    private Vector3 lastHeadAngle = Vector3.zero;
    private Vector3 lastHeadPosition = Vector3.zero;

    public override void Initialize() { }

    protected override void StartGame()
    {
        lastHeadAngle.x = playerHead.transform.localEulerAngles.x;
        lastHeadAngle.y = playerHead.transform.localEulerAngles.y;
        lastHeadAngle.z = playerHead.transform.localEulerAngles.z;
        playerHead.transform.localEulerAngles = playerHeadAngle;

        lastHeadPosition = playerHead.transform.localPosition;
        playerHead.transform.localPosition = playerHeadPosition;
    }

    protected override void ResetGame()
    {
        playerHead.transform.localEulerAngles = lastHeadAngle;
        playerHead.transform.localPosition = lastHeadPosition;
        grabbableBall.ResetGrabbableBall();
    }
}
