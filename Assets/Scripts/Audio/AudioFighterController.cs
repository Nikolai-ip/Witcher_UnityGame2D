using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFighterController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _trySwordHitAudioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _countreAttackAudioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _blockAttackAudioClips = new List<AudioClip>();

    [SerializeField] private List<AudioClip> _swordHitInBodyAudioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _escapeAudioClips = new List<AudioClip>();
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayTrySwordHitAudioClip()
    {
        _audioSource.clip = _trySwordHitAudioClips[Random.Range(0, _trySwordHitAudioClips.Count)];
        _audioSource.Play();
    }
    public void PlayCountreAttackAudioClip()
    {
        _audioSource.clip = _countreAttackAudioClips[Random.Range(0, _countreAttackAudioClips.Count)];
        _audioSource.Play();
    }
    public void PlayBlockAttackAudioClip()
    {
        _audioSource.clip = _blockAttackAudioClips[Random.Range(0, _blockAttackAudioClips.Count)];
        _audioSource.Play();
    }
    public void PlaySwordHitInBodyAudioClip()
    {
        _audioSource.clip = _swordHitInBodyAudioClips[Random.Range(0, _swordHitInBodyAudioClips.Count)];
        _audioSource.Play();
    }
    public void PlayEscapeAudioClip()
    {
        _audioSource.clip = _escapeAudioClips[Random.Range(0, _escapeAudioClips.Count)];
        _audioSource.Play();
    }
}
