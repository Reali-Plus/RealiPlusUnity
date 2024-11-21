using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameSceneManager();
            }
            return instance;
        }
    }
    private static GameSceneManager instance;

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
