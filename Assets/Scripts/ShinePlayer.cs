using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinePlayer : MonoBehaviour
{
    [SerializeField]
    private string shineAxisName;

    [SerializeField]
    private float shineDuration;

    [SerializeField]
    private Sprite light;

    [SerializeField]
    private Sprite noLight;

    [SerializeField]
    private AudioClip missSound;

    [SerializeField]
    private AudioClip shineSound;

    [SerializeField]
    private BoxCollider2D parentCollider;

    [SerializeField]
    private ShinyUI shinyUI;

    public bool isShining { get; private set; }

    private bool canShine = true;
    
    private SpriteRenderer renderer;
    private BoxCollider2D collider2D;
    private AudioSource audio;
    private CollectShinyThings shinyCollectionScript;


    private void Start()
    {
        shinyCollectionScript = GetComponentInParent<CollectShinyThings>();
        collider2D = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = noLight;
    }
    void Update ()
    {
        GetShineInput();
	}

    private void GetShineInput()
    {
        if (GameManager.canShine)//if we have the ability to shine
        {
            if (Input.GetButtonDown(shineAxisName))//and we press the shine button
            {
                if (canShine && shinyCollectionScript.itemsCollected > 0)//we can't shine a light if we have no stars//GRAPHIC no symbol
                {
                    renderer.sprite = light;
                    shinyCollectionScript.itemsCollected--;//use up one of our stars when we shine
                    shinyUI.DeleteUI();//show that we used it up
                    collider2D.enabled = true;//toggle colliders
                    parentCollider.enabled = false;
                    isShining = true;
                    StartCoroutine(ShineCooldown());
                }
            }
        }
    }

    IEnumerator ShineCooldown()
    {
        canShine = false;
        audio.PlayOneShot(shineSound);

        if (!GameManager.hasShone)//gets us to stop playing the prompt 
            GameManager.hasShone = true;

        yield return new WaitForSeconds(shineDuration);//hold the light until the duration is up
        renderer.sprite = noLight;
        collider2D.enabled = false;
        parentCollider.enabled = true;

        isShining = false;
        canShine = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            Destroy(collision.gameObject);
            audio.PlayOneShot(missSound);
            SpawnShiny.numOfShinyOnScreen--;           
        }

        if(collision.tag == "enemy")
        {
            collision.GetComponent<EnemyFollowPlayer>().CallDeathCoroutine();
        }
    }
}
