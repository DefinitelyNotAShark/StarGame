using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciousCollectTrigger : MonoBehaviour
{
    private SpawnPreciousObject spawner;
    private bool collected = false;
    private AudioSource audio;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("preciousSpawner").GetComponent<SpawnPreciousObject>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "preciousFinder")//if the player collected a precious
        {
            if (collision.gameObject.GetComponentInChildren<ShinePlayer>().isShining == true)//can only collect if we're shining
            {
                Debug.Log("trying to collect precious");
                collision.GetComponentInParent<PlayerPreciousCount>().preciousCount++;//add 1 to our count

                if (!collected)//just in case we run into it again while it's invisible;
                    StartCoroutine(CollectPreciousCooldown());

                collected = true;
            }
            Debug.Log("Shining was false what the fuck");
        }
    }

    IEnumerator CollectPreciousCooldown()
    {
        GetComponent<SpriteRenderer>().enabled = false;//turn it invisible                                                
        audio.Play();//play collect noise
        spawner.DecreaseAmountOnScreen();//this helps us change the amount on screen var without making it public
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
