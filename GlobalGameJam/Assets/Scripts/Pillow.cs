using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
public class Pillow : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isFlying;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AttachToPlayer(Transform handTransform)
    {
        DisableCollisionToPlayer(true);
        _isFlying = false;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.velocity = Vector3.zero;
        transform.parent = handTransform;
        transform.position = handTransform.position;
    }

    public void Throw(Vector3 direction)
    {
        DisableCollisionToPlayer(false);
        _isFlying = true;
        _rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        _rb.velocity = direction * 10;
    }

    private static void DisableCollisionToPlayer(bool disable)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pillow"), disable);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Player player;
        if ((player = other.gameObject.GetComponent<Player>()) == null || !_isFlying) return;
        var gameController = FindObjectOfType<GameController>();
        if(player.playerNumber == 1)
            gameController.OnPlayer1GotHit();
        else
            gameController.OnPlayer2GotHit();
    }
}