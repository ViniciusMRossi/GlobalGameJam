using UnityEngine;

public class ThrowableFurniture : Furniture
{
    private Transform houseParent;
    public float maxRotationStrength;
    
    private void Start()
    {
        houseParent = transform.parent;
    }

    public void Throw(Vector3 force)
    {
        transform.SetParent(houseParent, false);
        _rigidBody.AddForce(force, ForceMode.Impulse);
        _rigidBody.AddTorque(Random.Range(0, maxRotationStrength) * Vector3.forward);
    }

    public void Pickup(Transform parent)
    {
        transform.SetParent(parent, false);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
