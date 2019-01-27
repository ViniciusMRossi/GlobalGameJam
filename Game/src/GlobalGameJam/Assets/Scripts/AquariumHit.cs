using UnityEngine;

public class AquariumHit : MonoBehaviour
{
    private AudioSource _audioSource;
    public ParticleSystem[] _waterLeaks;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var pillow = other.gameObject.GetComponent<Pillow>();
        if (pillow != null && pillow.heldBy == null)
        {
            _audioSource.Play();
            foreach (var waterLeak in _waterLeaks)
            {
                var leakEmission = waterLeak.emission;
                leakEmission.enabled = true;
            }
        }
    }
}
