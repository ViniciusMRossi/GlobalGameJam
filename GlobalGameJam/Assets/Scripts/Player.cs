using System;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
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
    public LayerMask throwable;
    public Transform handTransform;

    private Dictionary<Commands, string> _commandMap;
    private Vector3 _velocity;
    private Rigidbody _rb;
    private Pillow _pillow;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        switch (playerNumber)
        {
            case 1:
                _commandMap = Player1Map;
                break;
            case 2:
                _commandMap = Player2Map;
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
        LookForward(inputDirection);
        _velocity = inputDirection * Time.deltaTime * speed;
        _rb.velocity = _velocity;
    }

    private void UpdateActions()
    {
        HandleActionPress();
    }

    private void HandleActionPress()
    {
        if (!Input.GetButtonDown(_commandMap[Commands.Fire])) return;

        if(!_pillow)
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var results = Physics.OverlapSphere(transform.position, 1, throwable);
            if (!(results?.Length > 0)) return;
            _pillow = results[0].transform.GetComponent<Pillow>();
            _pillow.AttachToPlayer(handTransform);
        }
        else
        {
            _pillow.Throw(transform.forward);
            _pillow = null;
        }
    }

    private Vector3 GetInputDirection()
    {
        var inputDirection = new Vector3(Input.GetAxis(_commandMap[Commands.Horizontal]), 0,
            Input.GetAxis(_commandMap[Commands.Vertical]));
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
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
