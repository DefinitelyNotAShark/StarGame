using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour
{
    //this goes on the text
    [SerializeField]
    private float pauseTimeBetweenText;

    [SerializeField]
    private float beepVolume;

    [SerializeField]
    private AudioClip beep;

    private char[] textConvertedToArray;
    private Text text;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        text = GetComponent<Text>();
    }

    public void AnimateNewText(string textToAnimate)
    {
        textConvertedToArray = textToAnimate.ToCharArray();
        StartCoroutine(DoAnimate());
    }

    private IEnumerator DoAnimate()
    {
        text.text = string.Empty;//reset the text

        for(int i = 0; i < textConvertedToArray.Length; i++)
        {
            text.text = text.text + textConvertedToArray[i];//add a new letter each time
            audioSource.PlayOneShot(beep, beepVolume);
            yield return new WaitForSeconds(pauseTimeBetweenText);
        }
    }
}
