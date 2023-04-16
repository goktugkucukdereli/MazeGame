using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    float vertical, horizontal;
    public int speed;

    public Joystick jy;

    private void FixedUpdate()
    {
        if (vertical != 0 || horizontal != 0)
        {
            transform.up = new Vector3(horizontal * speed, vertical * speed, 0);
            transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime, Space.World);
        }
    }
}