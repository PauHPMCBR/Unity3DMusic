using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicImporter : MonoBehaviour
{
    public string path;
    public AudioImporter importer;
    public AudioSource audioSource;

    void Awake()
    {
        path = "C://Users//Pau//Documents//Programacio//Unity3DMusic//Assets//Sounds//out.wav";
        importer.Import(path);
    }

    void Start()
    {
        audioSource.clip = importer.audioClip;
        audioSource.Play();
    }
}
