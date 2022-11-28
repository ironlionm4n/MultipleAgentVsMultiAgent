using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level_3");
    }
}
