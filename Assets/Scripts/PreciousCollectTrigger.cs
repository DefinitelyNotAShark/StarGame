using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciousCollectTrigger : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//if the player collected a precious
        {
            collision.GetComponent<PlayerPreciousCount>().preciousCount++;//add 1 to our count

            if (!collected)//just in case we run into it again while it's invisible;
                StartCoroutine(CollectPreciousCooldown());

            collected = true;
        }
    }

    IEnumerator CollectPreciousCooldown()
    {
        Debug.Log("You got something precious!");
        GetComponent<SpriteRenderer>().enabled = false;//turn it invisible                                                
        //AUDIO play precious collection sound ("VICTORY")
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
