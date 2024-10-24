using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int KeysPossessed { get; set; }

    #region Singleton
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    } 

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }
    #endregion
    private void Start()
    {
        GameSceneManager.LoadScene("MemoryScene");
    }
}
