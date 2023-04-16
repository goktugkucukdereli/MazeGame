using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Maze_MouseCtrl : MonoBehaviour
{
    public GameObject youWin;
    public float speed;
    private Rigidbody2D rb;
    private GameObject mouse;
    public GameObject cheese;
    public GameObject truee, falsee, falsePath;
    public Text timer;
    private float time2;
    public float minXval, maxXval;
    public float minYval, maxYval;
    private float vertical, horizontal;
    public Joystick jy;

    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Swing");
    }

    public void Cheeses()
    {
        Destroy(cheese);
        youWin.SetActive(true);
        gameObject.SetActive(false);
    }

    public bool walking = false;
    void Update()
    {
        line();

        time2 += Time.deltaTime;
        timer.text = " " + time2.ToString("F0");

        vertical = jy.Vertical;
        horizontal = jy.Horizontal;

        if (vertical != 0 && horizontal != 0)
        {
            anim.ResetTrigger("Walk");
            transform.up = new Vector3(horizontal * speed, vertical * speed, 0); // farenin yönünü yukarıya döndürüyor
            transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime, Space.World);
            anim.ResetTrigger("Swing");
            anim.SetTrigger("Walk");
            if(walking==false)
            
            walking = true;
            //Maze_AudioManager.maze_AManager.WalkVoice2();
        }

        else if(vertical == 0 && horizontal == 0)
        {
            walking = true;
            anim.SetTrigger("Swing");
            anim.ResetTrigger("Walk");
        }
    }

    public void RightStepSound()
    {
        Maze_AudioManager.maze_AManager.WalkVoice1();
    }
    public void LeftStepSound()
    {
        Maze_AudioManager.maze_AManager.WalkVoice2();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cheese")
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            anim.ResetTrigger("Swing");
            anim.SetTrigger("Eat");
            Debug.Log("Cheese");

            if (time2 <= 30)
            {
                Debug.Log("3 Yıldız Kazandınız.");
            }

            else if (time2 > 30 && time2 < 45)
            {
                Debug.Log("2 Yıldız Kazandınız.");
            }

            else if (time2 >= 45)
            {
                Debug.Log("1 Yıldız Kazandınız.");
            }

            Invoke("Cheeses", 2);
        }

        if (collision.gameObject == truee)
        {
            Maze_AudioManager.maze_AManager.EatVoice();
            anim.ResetTrigger("Swing");
            anim.SetTrigger("Eat");
        }

        if (collision.gameObject == falsee)
        {
            Maze_AudioManager.maze_AManager.ContactVoice();
            anim.ResetTrigger("Swing");
            anim.SetTrigger("Contact");
        }

        if (collision.gameObject == falsePath)
        {
            anim.ResetTrigger("Swing");
            anim.SetTrigger("Look");
        }
    }

    private void line()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minXval, maxXval);
        pos.y = Mathf.Clamp(pos.y, minYval, maxYval);
        transform.position = pos;
    }
}
