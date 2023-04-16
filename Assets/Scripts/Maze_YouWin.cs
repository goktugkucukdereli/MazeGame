using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maze_YouWin : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject mainmenuButton;
    public GameObject nextlevelButton;

    public void OpenLevel(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void OpenLevel2(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }
}
