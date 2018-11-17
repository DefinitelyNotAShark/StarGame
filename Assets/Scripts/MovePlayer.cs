using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private string horizontalName;

    [SerializeField]
    private string verticalName;

    [SerializeField]
    private float speed;

    private float horizontalValue;
    private float verticalValue;

    void Update ()
    {
        GetInput();
	}

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        horizontalValue = Input.GetAxis(horizontalName);
        verticalValue = Input.GetAxis(verticalName);

        if (!GameManager.hasMoved)
            MoveCheck();
    }

    private void Move()
    {
        transform.Translate(new Vector2(horizontalValue, verticalValue) * speed * Time.deltaTime);
    }

    private void MoveCheck()//checks to see whether to keep prompting the player with controls icon
    {
        if (horizontalValue > .1f || horizontalValue > .1f)
            GameManager.hasMoved = true;
    }
}
