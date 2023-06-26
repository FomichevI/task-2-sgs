using UnityEngine;

public class CarAudio : MonoBehaviour
{
    [SerializeField] private float _motorForceScale = 0.1f;
    [SerializeField] private AudioClip _motorClip;
    [SerializeField] private AudioSource _slideAudoiSource;
    private AudioSource _motorAudoiSource;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _motorAudoiSource = GetComponent<AudioSource>();
        PlayMove();
    }

    private void FixedUpdate()
    {        
        _motorAudoiSource.pitch = 1 + _rigidbody.velocity.magnitude * _motorForceScale;
        _motorAudoiSource.volume = 0.5f + _rigidbody.velocity.magnitude * _motorForceScale;
    }

    private void PlayMove()
    {
        _motorAudoiSource.clip = _motorClip;
        _motorAudoiSource.Play();
    }
    public void PlaySliding()
    {
        if(!_slideAudoiSource.isPlaying)
        _slideAudoiSource.Play();
    }
}
