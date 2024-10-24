using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nbrLevel = 2;
    public int KeysPossessed { get; set; } = 0;

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

    public void AddKey()
    {
        KeysPossessed++;
        Debug.Log("Keys Possessed: " + KeysPossessed);
        //TODO: Added visual content like UI for nbr of key possessed

        if (KeysPossessed >= nbrLevel)
        {
            LoadDoorScene();
        }
    }

    private void LoadDoorScene()
    {
        GameSceneManager.LoadScene("DoorScene"); 
    }
}
