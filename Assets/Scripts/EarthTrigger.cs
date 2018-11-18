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
        if (collision.gameObject.tag == "preciousFinder")//if the player collected a precious
        {
            if (collision.gameObject.GetComponentInChildren<ShinePlayer>().isShining == true)//can only collect if we're shining
            {
                audio.Play();
                collision.GetComponentInParent<PlayerPreciousCount>().hasFoundEarth = true;
            }
        }
    }
}
