using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class playRandomAudio : MonoBehaviour
{

    public AudioClip[] clips;

    public bool randomizePitch;
    public float pitchRange;

    protected AudioSource m_Source;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        AudioClip[] source = clips;

        int choice = Random.Range(1, source.Length);

        if (randomizePitch)
            m_Source.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        m_Source.PlayOneShot(source[choice]);
    }
}
