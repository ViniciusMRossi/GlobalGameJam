using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
public class Pillow : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isThrown;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AttachToPlayer(Transform handTransform)
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.velocity = Vector3.zero;
        transform.parent = handTransform;
        transform.position = handTransform.position;
    }

    public void Throw(Vector3 direction)
    {
        _rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        _rb.velocity = direction * 10;
    }
}