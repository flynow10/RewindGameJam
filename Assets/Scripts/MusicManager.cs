using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] tracks;
    public int song = 0;

    void Start()
    {
        foreach (Music track in tracks)
        {
            track.source = gameObject.AddComponent<AudioSource>();
            track.source.volume = track.volume;
            track.source.clip = track.clip;
            track.source.playOnAwake = false;
        }
    }

    void Update()
    {
        if (!PlayerPrefs.HasKey("Mute") || PlayerPrefs.GetInt("Mute") == 0)
        {
            bool songPlaying = false;
            foreach (Music track in tracks)
            {
                if (track.source.isPlaying)
                    songPlaying = true;
            }


            if (!songPlaying)
            {
                if (song == tracks.Length)
                    song = 0;
                tracks[song].source.Play();
                song++;
            }
        }
    }
    public void Play(string Name)
    {
        Array.Find(tracks, track => track.name == Name).source.Play();
    }
}
