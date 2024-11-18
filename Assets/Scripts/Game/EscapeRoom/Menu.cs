using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    void OnCollisionEnter()
    {
        GameManager.Instance.StartFirstGame();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.StartFirstGame();
    }

}
