using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float playerPunishmentTime;

    [SerializeField]
    private AudioClip stunPlayer;

    [SerializeField]
    private AudioClip killEnemy;

    private AudioSource audio;
    private GameObject player;
    private Vector2 vectorToMove;

	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        FollowThePlayer();
	}

    void FollowThePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(EnemyGotPlayerPunishment());
        }
    }

    IEnumerator EnemyGotPlayerPunishment()
    {
        audio.PlayOneShot(stunPlayer);
        GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<MovePlayer>().canMove = false;
        player.GetComponent<CollectShinyThings>().DestroyAllShinyThings();
        yield return new WaitForSeconds(playerPunishmentTime);
        player.GetComponent<MovePlayer>().canMove = true;
        SpawnEnemy.enemiesOnScreen--;
        Destroy(this.gameObject);
    }

    IEnumerator PlayerGotEnemy()
    {
        audio.PlayOneShot(killEnemy);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        SpawnEnemy.enemiesOnScreen--;
        Destroy(this.gameObject);
    }

    public void CallDeathCoroutine()
    {
        StartCoroutine(PlayerGotEnemy());
    }

}
