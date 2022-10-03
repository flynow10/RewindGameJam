using UnityEngine;

[System.Serializable]
public class Music
{
    public string name;
    public float volume;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
}