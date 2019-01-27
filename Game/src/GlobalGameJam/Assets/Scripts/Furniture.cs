using UnityEngine;

public class Furniture : MonoBehaviour
{
    protected Rigidbody _rigidBody;
    protected Collider _collider;
    
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Furniture");
        if ((_collider = GetComponent<Collider>()) == null)
        {
            Debug.LogError("Missing collider");
        }
        else
        {
            if (_collider.isTrigger)
            {
                Debug.LogWarning("Should not be a trigger, changing this for this run");
                _collider.isTrigger = false;
            }
        }

        if ((_rigidBody = GetComponent<Rigidbody>()) == null)
        {
            Debug.LogError("Missing collider");
        }
        else
        {
            
        }
    }
    
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
