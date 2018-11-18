using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//goes on the player
public class CollectShinyThings : MonoBehaviour
{
    [HideInInspector]
    public int itemsCollected;

    [SerializeField]
    private int maxShiny;

    private AudioSource audio;

    [SerializeField]
    private AudioClip collectSound;

    [SerializeField]
    private ShinyUI shinyUI;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")//if I run into a shiny thing
        {
            if (!GetComponentInChildren<ShinePlayer>().isShining)
            {
                if (itemsCollected < maxShiny)//can't collect more than the max a
                {
                    audio.PlayOneShot(collectSound);
                    Destroy(collision.gameObject);
                    shinyUI.AddUI();//change the ui
                    itemsCollected++;
                    SpawnShiny.numOfShinyOnScreen--;
                }
            }
        }
    }

    public void DestroyAllShinyThings()
    {
        int tempItemCount = itemsCollected;
        for (int i = 0; i < tempItemCount; i++)
        {
            shinyUI.DeleteUI();
            itemsCollected--;
        }
    }
}
