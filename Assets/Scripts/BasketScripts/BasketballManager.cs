using UnityEngine;

public class BasketballManager : MiniGameManager
{
    [SerializeField]
    private GrabbableBall grabbableBall;

    public override void Initialize() { }
    protected override void StartGame(){ }

    protected override void ResetGame()
    {
        grabbableBall.ResetGrabbableBall();
    }
}
