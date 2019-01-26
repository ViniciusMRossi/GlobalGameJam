using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private enum Commands
    {
        Horizontal,
        Vertical,
        Fire,
        Pause
    }

    private static readonly Dictionary<Commands, string> Player1Map = new Dictionary<Commands, string>
    {
        {Commands.Horizontal, "Horizontal1"},
        {Commands.Vertical, "Vertical1"},
        {Commands.Fire, "Action1"}
    };


    private static readonly Dictionary<Commands, string> Player2Map = new Dictionary<Commands, string>
    {
        {Commands.Horizontal, "Horizontal2"},
        {Commands.Vertical, "Vertical2"},
        {Commands.Fire, "Action2"}
    };

    public float speed;
    public int playerNumber;

    private Dictionary<Commands, string> commandMap;
    private Vector2 velocity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        switch (playerNumber)
        {
            case 1:
                commandMap = Player1Map;
                break;
            case 2:
                commandMap = Player2Map;
                break;
            default:
                throw new ArgumentException("Bad player number");
        }
    }

    private void Update()
    {
        UpdatePlaying();
        UpdateActions();
    }

    private void UpdatePlaying()
    {
        var inputDirection = GetInputDirection();
        velocity = inputDirection * Time.deltaTime * speed;
        rb.velocity = velocity;
    }

    private void UpdateActions()
    {
        HandleActionPress();
    }

    private void HandleActionPress()
    {
        if (!Input.GetButtonDown(commandMap[Commands.Fire])) return;

        //TODO PERFORM FIRE
    }

    private Vector3 GetInputDirection()
    {
        var inputDirection = new Vector3(Input.GetAxis(commandMap[Commands.Horizontal]),
            Input.GetAxis(commandMap[Commands.Vertical]));
        Debug.Log(inputDirection);
        return inputDirection;
    }

    private void LookForward(Vector3 inputDirection)
    {
        if (inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
        }
    }

    public void OnHit(float impactValue)
    {
    }

    public void StartPlaying()
    {
    }
}
