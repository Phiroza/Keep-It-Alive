using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float timer;
    private bool resetRotationalOffset;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();           
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            effector.rotationalOffset = 180f;
            resetRotationalOffset = false;
            timer = 0.15f;
        }

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        } else if (resetRotationalOffset == false)
        {
            effector.rotationalOffset = 0;
            resetRotationalOffset = true;
        }

    }
}
