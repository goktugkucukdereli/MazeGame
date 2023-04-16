using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MTEK.Audio;

public class Maze_Menu : MonoBehaviour
{
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;
    public GameObject level5Button;
    public GameObject level6Button;
    public GameObject level7Button;
    public GameObject level8Button;
    public GameObject level9Button;
    public GameObject level10Button;
    public GameObject exitButton;

    public void OpenLevel(string loadScene) 
    {
        SceneManager.LoadScene(loadScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
