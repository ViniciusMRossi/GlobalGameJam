using UnityEngine;

public class Furniture : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Furniture");
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("Missing collider");
        }

        if (GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("Missing collider");
        }
    }
}
