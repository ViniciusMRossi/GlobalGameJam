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
        
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(force, ForceMode.Impulse);
        _rigidBody.AddTorque(Random.Range(0, maxRotationStrength) * Vector3.forward);
    }

    public void Pickup(Transform parent)
    {
        transform.SetParent(parent, false);
        _rigidBody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player;
        if ((player = other.gameObject.GetComponent<Player>()) != null)
        {
            player.OnHit(_rigidBody.mass * _rigidBody.velocity.magnitude);
        }
    }
}
