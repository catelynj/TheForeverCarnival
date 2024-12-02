using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MuteAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource source;
    private void Start()
    {
        mixer.SetFloat("Volume", -20f);
    }
    public void OnChangeSlider(float value)
    {
        mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
    }
}
