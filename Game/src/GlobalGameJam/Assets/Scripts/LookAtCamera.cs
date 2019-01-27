using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool isPlayer2;
    private Camera _camera;
    private int multiplier;

    private void Start()
    {
        multiplier = isPlayer2 ? -1 : 1;
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.forward = _camera.transform.forward * multiplier;
    }
}
