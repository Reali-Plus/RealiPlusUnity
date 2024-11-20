using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameManager : MonoBehaviour
{
    protected bool gameActive = false;

    public virtual void Starting()
    {
        gameActive = true;
        Debug.Log("Game started.");
        StartGame();
    }

    public virtual void Restart()
    {
        if (!gameActive)
        {
            Debug.LogWarning("Can't restart. Game is not active.");
            return;
        }

        gameActive = false;
        Debug.Log("Game restarted.");
        ResetGame();
        gameActive = true;
    }

    protected abstract void StartGame();
    protected abstract void ResetGame();

}
