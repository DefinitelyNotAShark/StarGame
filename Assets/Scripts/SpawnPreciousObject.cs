using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPreciousObject : MonoBehaviour
{
    [SerializeField]
    private int maxNumOfPreciousOnScreen;

    [SerializeField]
    private GameObject preciousPrefab;

    [SerializeField]
    private Sprite[] preciousSprites;

    [SerializeField]
    private float MinTimeBetweenSpawns;

    [SerializeField]
    private float MaxTimeBetweenSpawns;

    [HideInInspector]
    public static int numOfPreciousOnScreen = 0;

    public bool canStartSpawning = false;

    private GameObject preciousInstance;


    private float minY, maxY;
    private float minX, maxX;
    private bool canSpawnAgain = true;
    private int spriteIndex;

    // Use this for initialization
    void Start()
    {
        minY = -4;//hard coding lol
        maxY = 1.7f;//HACK fix later with some Viewport to world or some shit

        minX = -7.5f;
        maxX = 7.5f;
    }
	
	void Update ()
    {
        if (canStartSpawning)
        {
            if (numOfPreciousOnScreen < maxNumOfPreciousOnScreen)
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
        preciousInstance = Instantiate(preciousPrefab, GetRandomTransform(), transform.rotation);

        preciousInstance.GetComponent<SpriteRenderer>().sprite = preciousSprites[spriteIndex];//set it to have a different sprite

        numOfPreciousOnScreen++;

        if (spriteIndex < preciousSprites.Length - 1)//if we have more sprites to go through, then we can spawn more precious objects...
        {
            canSpawnAgain = true;
            spriteIndex++;
        }
    }

    private Vector2 GetRandomTransform()
    {
        float y = Random.Range(minY, maxY + 1);
        float x = Random.Range(minX, maxX + 1);

        return new Vector2(x, y);
    }
}
