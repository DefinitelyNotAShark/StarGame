using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float startBufferTime;

    [SerializeField]
    private float playerInvisibleTime;

    [SerializeField]
    private float timeBeforeMovePrompt;

    [SerializeField]
    private float timeAfterLongPrompt;

    [SerializeField]
    private float timeAfterWinPrompt;

    [SerializeField]
    private Text text;

    [SerializeField]
    private Image promptImage;

    [SerializeField]
    private Sprite movePromptSprite;

    [SerializeField]
    private Sprite shinePromptSprite;

    [SerializeField]
    private string[] storyText;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float blinkSpeed;

    [SerializeField]
    private int blinkNumber;//number of times the movement prompt blinks

    [SerializeField]
    private AudioClip playerAppearSound;

    private AudioSource audio;

    public static bool hasMoved = false;
    public static bool hasShone = false;
    public static bool canShine = false;

    private AnimateText animateTextScript;
    private GameObject preciousUI;

	void Start ()
    {
        audio = GetComponent<AudioSource>();
        player.GetComponent<SpriteRenderer>().enabled = false;
        promptImage.sprite = movePromptSprite;
        promptImage.enabled = false;
        animateTextScript = text.GetComponent<AnimateText>();
        preciousUI = GameObject.FindGameObjectWithTag("preciousUI");
        preciousUI.SetActive(false);
        StartCoroutine(startBuffer());
	}

    /// <summary>
    /// waits a second, animates text in and turns the player on before starting the move icon blinking coroutine
    /// </summary>
    IEnumerator startBuffer()
    {
        yield return new WaitForSeconds(startBufferTime);
        animateTextScript.AnimateNewText(storyText[0]);
        yield return new WaitForSeconds(playerInvisibleTime);

        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<MovePlayer>().canMove = true;
        audio.PlayOneShot(playerAppearSound);

        yield return new WaitForSeconds(timeBeforeMovePrompt);
        StartCoroutine(BlinkMovePrompt());
    }

    IEnumerator BlinkMovePrompt()//prompt the player to move until they do 
    {
        while(hasMoved == false)
        {
            promptImage.enabled = true;
            promptImage.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(blinkSpeed);
            promptImage.enabled = false;
            yield return new WaitForSeconds(blinkSpeed);
        }
        promptImage.enabled = false;
        StartCoroutine(CollectionPrompt());
    }

    IEnumerator CollectionPrompt()
    {
        animateTextScript.AnimateNewText(storyText[1] + "\n" + storyText[2]);
        yield return new WaitForSeconds(timeAfterLongPrompt);//don't have to wait very long
        //start a spawn script of items to collect
        while (player.GetComponent<CollectShinyThings>().itemsCollected < 3)
        {
            GetComponentInChildren<SpawnShiny>().canStartSpawning = true;
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(ShinePrompt());
    }

    IEnumerator ShinePrompt()
    {
        promptImage.sprite = shinePromptSprite;
        animateTextScript.AnimateNewText(storyText[3] + "\n" + storyText[4]);//animate lines 2 and 3
        canShine = true;//give player shine ability
        yield return new WaitForSeconds(timeAfterLongPrompt);
        promptImage.enabled = true;
        while(hasShone == false)
        {
            promptImage.enabled = true;
            promptImage.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(blinkSpeed);
            promptImage.enabled = false;
            yield return new WaitForSeconds(blinkSpeed);
        }
        promptImage.enabled = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(ShiningScaresMyFriendsPrompt());
    }

    IEnumerator ShiningScaresMyFriendsPrompt()
    {
        animateTextScript.AnimateNewText(storyText[5] + "\n" + storyText[6]);
        yield return new WaitForSeconds(timeAfterLongPrompt * 2.3f);//longer
        StartCoroutine(PreciousThingsPrompt());
    }

    IEnumerator PreciousThingsPrompt()
    {
        animateTextScript.AnimateNewText(storyText[7] + "\n" + storyText[8]);
        yield return new WaitForSeconds(timeAfterLongPrompt);
        GetComponentInChildren<SpawnPreciousObject>().canStartSpawning = true;//start spawning my precioussss
        preciousUI.SetActive(true);
        while (player.GetComponent<PlayerPreciousCount>().preciousCount < 1)//.count < 1
        {
            yield return new WaitForSeconds(blinkSpeed);//don't get caught doing this every frame
        }//keep it simple until they collect a precious
        StartCoroutine(EnemiesPrompt());
    }

    IEnumerator EnemiesPrompt()
    {
        animateTextScript.AnimateNewText(storyText[9] + "\n" + storyText[10]);
        yield return new WaitForSeconds(timeAfterLongPrompt * 2);
        GetComponentInChildren<SpawnEnemy>().canStartSpawning = true;
        while (player.GetComponent<PlayerPreciousCount>().preciousCount < player.GetComponent<PlayerPreciousCount>().maxPreciousCount)
        {
            yield return new WaitForSeconds(blinkSpeed);
        }
        StartCoroutine(WinPrompt());
    }

    IEnumerator WinPrompt()
    {        
        animateTextScript.AnimateNewText(storyText[11] + "\n" + storyText[12]);
        yield return new WaitForSeconds(timeAfterWinPrompt);
        //show something here
        SceneManager.LoadScene("Win Scene");
    }
}
