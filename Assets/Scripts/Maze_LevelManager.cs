using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze_LevelManager : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey(gameObject.name) && PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            transform.GetChild(7).GetComponent<Image>().enabled = true;
        }
        else
        {
            transform.GetChild(7).GetComponent<Image>().enabled = false;
        }
    }
}
