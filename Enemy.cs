using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemyvel = 4;
    private GameObject _EnemyPrefab;
    private GameObject _Player;
    private Animator _Anim;
    private Collider _enemyCollider;
    [SerializeField]
    private AudioSource _audioSource;
    private BoxCollider2D _colider;


    // Start is called before the first frame update
    void Start()
    {
        _colider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on enemy is null");
        }
        _Anim = GetComponent<Animator>();
        if (_Anim == null)
        {
            Debug.Log("Animation is null");
        }
        _enemyCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _enemyvel * Time.deltaTime);
        if (transform.position.y < -1.4f)
        {
            float randomx = Random.Range(-6.5f, 6f);
            transform.position = new Vector3(randomx, 4, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Dano();
            }
            _Anim.SetTrigger("OnEnemyDeath");
            _enemyvel = 0;
            _audioSource.Play();
            _colider.enabled = false;
            Destroy(this.gameObject, 2.8f);
           

        }
        if (other.tag == "Lazer")
        {
            
            Destroy(other.gameObject);
            //add 10 to score
            _Player = GameObject.FindGameObjectWithTag("Player");
            Player player = _Player.transform.GetComponent<Player>();
            if (player != null)
            {
                player.ScoreCount(10);
            }

            _Anim.SetTrigger("OnEnemyDeath");
            _enemyvel = 0;
            _audioSource.Play();
            _colider.enabled = false;
            Destroy(this.gameObject, 2.8f);
           
        }

    }  
}
