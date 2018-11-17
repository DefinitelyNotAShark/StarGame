using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private int maxNumOfEnemiesOnScreen;//probably 1

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float MinTimeBetweenSpawns;

    [SerializeField]
    private float MaxTimeBetweenSpawns;

    public static int enemiesOnScreen;

    private float minX, maxX;
    private float minY, maxY;

    public bool canStartSpawning = false;
    private bool canSpawnAgain = true;

    private void Start()//enemies are coming from the sides so we're gonna be choosing either a max x or a min x. Not going between those
    { 
        minY = -5;//hard coding lol
        maxY = 2;//HACK fix later with some Viewport to world or some shit

        minX = -9;
        maxX = 9;
    }

    private void Update()
    {
        if (canStartSpawning)
        {
            if (enemiesOnScreen < maxNumOfEnemiesOnScreen)//if we don't have enough enemies on the screen
            {
                if (canSpawnAgain)//if we're allowed to spawn again
                {
                    StartCoroutine(StartSpawning());//then we spawn
                    canSpawnAgain = false;
                }
            }
        }
    }

    public IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns + 1));//because max is exclusive
        Instantiate(enemyPrefab, GetRandomTransform(), transform.rotation);       
        maxNumOfEnemiesOnScreen++;
        enemiesOnScreen++;
        canSpawnAgain = true;
    }

    private Vector2 GetRandomTransform()
    {
        float y = Random.Range(minY, maxY + 1);
        float randomX = Random.Range(1, 3);
        float x;

        if (randomX == 1)
            x = minX;

        else if (randomX == 2)
            x = maxX;
        else
            x = maxX;//JIC

        return new Vector2(x, y);
    }
}
