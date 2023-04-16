using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTEK.Audio;

public class Maze_AudioManager : MonoBehaviour
{
    public static Maze_AudioManager maze_AManager;
    public AudioClip[] maze_Voices;

    void Awake()
    {
        if (maze_AManager == null)
        {
            maze_AManager = this;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
        }
        else if (maze_AManager != null)
            Destroy(gameObject);
    }

    public void Start()
    {
        MtekAudio.SetBackgroundMusic?.Invoke(maze_Voices[0]);
    }

    public void ContactVoice()
    {
        MtekAudio.PopOne?.Invoke(maze_Voices[1]);
    }

    public void WalkVoice1()
    {
        MtekAudio.PopOne?.Invoke(maze_Voices[2]);
    }

    public void WalkVoice2()
    {
        MtekAudio.PopOne?.Invoke(maze_Voices[3]);
    }

    public void EatVoice()
    {
        MtekAudio.PopOne?.Invoke(maze_Voices[4]);
    }
}
