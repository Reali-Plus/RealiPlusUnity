using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    protected bool gameActive = false;

    public virtual void Starting()
    {
        gameActive = true;
        StartGame();
    }

    public virtual void Restart()
    {
        if (!gameActive)
        {
            return;
        }

        gameActive = false;
        ResetGame();
        gameActive = true;
    }

    protected abstract void StartGame();
    protected abstract void ResetGame();

}
