﻿using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.forward = _camera.transform.forward;
    }
}
