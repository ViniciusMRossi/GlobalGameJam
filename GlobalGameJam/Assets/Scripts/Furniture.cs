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
}
