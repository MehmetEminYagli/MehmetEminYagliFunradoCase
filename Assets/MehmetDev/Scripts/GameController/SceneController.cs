using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void BtnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BtnExit()
    {
        Application.Quit();
    }
}
