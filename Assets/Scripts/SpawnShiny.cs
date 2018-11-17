using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShiny : MonoBehaviour
{
    [SerializeField]
    private int maxNumOfShinyOnScreen;

    [SerializeField]
    private GameObject shinyPrefab;

    [SerializeField]
    private float MinTimeBetweenSpawns;

    [SerializeField]
    private float MaxTimeBetweenSpawns;

    [HideInInspector]
    public static int numOfShinyOnScreen = 0;

    private float minX, maxX;
    private float minY, maxY;
    public bool canStartSpawning = false;
    private bool canSpawnAgain = true;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();


        minY = -4;//hard coding lol
        maxY = 1.7f;//HACK fix later with some Viewport to world or some shit

        minX = -8;
        maxX = 8;
    }

    // Update is called once per frame
    void Update ()
    {
        if (canStartSpawning)
        {
            if (numOfShinyOnScreen < maxNumOfShinyOnScreen)
            {
                if (canSpawnAgain)
                {
                    StartCoroutine(StartSpawning());
                    canSpawnAgain = false;
                }
            }
        }
	}

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns));//because max is exclusive
        Instantiate(shinyPrefab, GetRandomTransform(), transform.rotation);
        audio.Play();//play jewel appear sound
        numOfShinyOnScreen++;
        canSpawnAgain = true;
    }

    private Vector2 GetRandomTransform()
    {
        float y = Random.Range(minY, maxY + 1);
        float x = Random.Range(minX, maxX + 1);

        return new Vector2(x, y);
    }
}
