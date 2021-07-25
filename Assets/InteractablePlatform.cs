﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlatform : MonoBehaviour
{
    PlayerController player;
    public bool isPlayerControlling = false;
    public bool horizontalMovment = false;
    public bool verticalMovement = false;
    public bool isPlayerOnPlatform = false;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerControlling == true)
        {
            if (horizontalMovment == true)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x + Input.GetAxisRaw("Horizontal")*speed*Time.deltaTime, -15, 15), transform.position.y);
            }

            if (verticalMovement == true)
            {
                transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, -15, 15));
            }

        }

    }


/*    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isPlayerControlling = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isPlayerControlling = false;
        }
    }
    */ 


    public void ActivatePlatform()
    {
        isPlayerControlling = true;
        if (isPlayerOnPlatform)
        {
            player.transform.parent = gameObject.transform;
        }
    }
    
    public void DeactivatePlatform()
    {
        isPlayerControlling = false;
    }

}
