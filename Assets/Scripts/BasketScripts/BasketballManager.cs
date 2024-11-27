using UnityEngine;

public class BasketballManager : MiniGameManager
{
    [SerializeField]
    private GrabbableBall grabbableBall;

    protected override void StartGame()
    {
        
    }

    protected override void ResetGame()
    {
        grabbableBall.ResetGrabbableBall();
    }
}
