﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackLauncher : MonoBehaviour
{
    public float maxLength;
    public Jack jackOb;
    public float power;
    public Jack deployedJack { get; set; }
    public float restrainedLength { get; set; }
    PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        PrimaryJackControls();
    }

    private void FixedUpdate()
    {
        if (deployedJack)
        {
            maintainJack();
        }
    }

    private void maintainJack()
    {
        if (deployedJack.cord != null)
        {
            {
                deployedJack.cord.positionCount = 2;
                Vector3[] positions = { deployedJack.cordAttach.position, transform.position };
                deployedJack.cord.SetPositions(positions);
            }
        }
        if (deployedJack.attached)
        {
            RestrictPlayerMovement();
        }
        if ((deployedJack.transform.position - transform.position).magnitude > maxLength) deployedJack.MissedTargets();
    }

    private void RestrictPlayerMovement()
    {
        Vector3 cordVec = deployedJack.attached.attachPoint.position - transform.position;
        if (cordVec.magnitude > restrainedLength)
        {
            float amount = cordVec.magnitude - restrainedLength;
            GetComponent<Rigidbody2D>().velocity += (Vector2)cordVec.normalized * amount;
        }
    }

    private void PrimaryJackControls()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (PlayerState.canSpiderMan)
            {
                if (deployedJack)
                {
                    deployedJack.DetachJack();
                }
                else
                {
                    LaunchJack();
                }
            }
            
        }
    }

    private void LaunchJack()
    {
        Vector2 charPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 launchVector = ((Vector2)Input.mousePosition - charPos).normalized;
        Jack jack = Instantiate(jackOb, transform.position, transform.rotation);
        jack.assignVelocity((launchVector * power) + (Vector2)GetComponentInParent<Rigidbody2D>().velocity);
        jack.launcher = this;
        deployedJack = jack;
    }
}
