using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
public class Pillow : MonoBehaviour
{
    private Rigidbody _rb;
    private Player heldBy;
    private bool _isFlying;
    private int thrownBy;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AttachToPlayer(Transform handTransform, Player player)
    {
        DisableCollisionToPlayer(true);
        heldBy = player;
        _isFlying = false;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.velocity = Vector3.zero;
        transform.parent = handTransform;
        transform.position = handTransform.position;
    }

    public void Throw(Vector3 direction)
    {
        DisableCollisionToPlayer(false);
        transform.parent = heldBy.transform.parent;
        thrownBy = heldBy.playerNumber;
        heldBy = null;
        _isFlying = true;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.velocity = direction * 10;
    }

    private static void DisableCollisionToPlayer(bool disable)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pillow"), disable);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null || !_isFlying || thrownBy == player.playerNumber) return;
        var gameController = FindObjectOfType<GameController>();
        if(player.playerNumber == 1)
            gameController.OnPlayer1GotHit();
        else if(player.playerNumber == 2)
            gameController.OnPlayer2GotHit();
    }
}