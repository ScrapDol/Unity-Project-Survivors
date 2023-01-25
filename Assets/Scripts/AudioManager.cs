using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
}
