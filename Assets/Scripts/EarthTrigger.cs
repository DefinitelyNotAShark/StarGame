using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTrigger : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audio.Play();
            collision.GetComponent<PlayerPreciousCount>().hasFoundEarth = true;
        }
    }
}
