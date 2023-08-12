using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _vel = 3.5f;
    private float _defaultvel = 3.5f;
    [SerializeField]
    private GameObject _LazerPrefab;
    [SerializeField]
    private GameObject _TriplePrefab;
    [SerializeField]
    private GameObject _SpeedUpPrefab;
    [SerializeField]
    private GameObject _ShieldPrefab;
    [SerializeField]
    private UiManager _UiManager;
    [SerializeField]
    private SceneManager _Scene;

    [SerializeField]
    private GameObject[] _hurt;

    [SerializeField]
    private AudioClip _lazerSound;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _taxadeTiro = 3.5f;
    [SerializeField]
    private int _vidas = 3;
    private Spawn_manager _spawnmanager;
    [SerializeField]
    private bool isTripleShot = false;
    [SerializeField]
    private bool isSpeedUp = false;
    [SerializeField]
    private bool isShield = false;
    [SerializeField]
    private int Score;
    private float _proxTiro = -1f;
    private bool _isDead = false;
    private int _randomhurt;
    void Start()
    {   
        _randomhurt = Random.Range(0, 1);
        transform.position = new Vector3(0, 0, 0);

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on player is null");
        }
        else
        {
            _audioSource.clip = _lazerSound;
            
        }
        _spawnmanager = GameObject.Find("Spawn_manager").GetComponent<Spawn_manager>();
        if (_spawnmanager == null)
        {
            Debug.LogError("Spawn manager é null");
        }
        _UiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (_UiManager == null)
        {
            Debug.Log("Ui manager é null"); 
        }
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CalcMov();
        if (Input.GetKey(KeyCode.Z) && Time.time > _proxTiro)
        {
            Tiro();
        }
        if( Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
          
        }
        if(_isDead == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("SpaceShooter");
                Debug.Log("Reset");
            }
        }
        if (isSpeedUp == true)
        {
            Vel();
        }
    }
        
    void CalcMov()
    {   
        float movHorizontal = CrossPlatformInputManager.GetAxis("Horizontal"); // float movHorizontal = Input.GetAxis("Horizontal");

        float movVertical = CrossPlatformInputManager.GetAxis("Vertical"); //float movVertical = Input.GetAxis("Vertical");

        Vector3 direção = new Vector3(movHorizontal, movVertical, 0);
        transform.Translate(direção * _vel * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -1.3f, 3.3f), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.80f, 4.80f), transform.position.y , 0);
  
    }
    void Tiro()
    {
        
        _proxTiro = Time.time + _taxadeTiro;
        if (isTripleShot == true)
        {
            StartCoroutine(TripleShotRoutine());
            Instantiate(_TriplePrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        else
        {
            Instantiate(_LazerPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);  
        }
        _audioSource.Play();
    }
       
    void Vel()
    {
        _vel = 8.5f;
        StartCoroutine(SpeedUpRoutine());
    }

    public void Dano()
    {
        if (isShield == true)
        {
            isShield = false;
            _ShieldPrefab.SetActive(false);
            return;
        }
        _vidas -= 1;


        if (_vidas == 2)
        {
            _hurt[_randomhurt].SetActive(true);
        }

        else if (_vidas == 1)
        {
            if (_randomhurt == 0)
                _hurt[1].SetActive(true);
            else if (_randomhurt == 1)
            {
                _hurt[0].SetActive(true);
            }
        }
         _UiManager.UpdateLives(_vidas);
         if (_vidas < 1)
         {
             _spawnmanager.onplayerdeath();
             Destroy(this.gameObject);

             _UiManager.GameOver();



         }
        
    }

    public void TriplePowerup()
    {
        isTripleShot = true;
    }
    public void SpeedPowerup()
    {
        isSpeedUp = true;
    }
    public void ShieldPowerup()
    {
        isShield = true;
        _ShieldPrefab.SetActive(true);
    }

    public void ScoreCount(int points)
    {
        Score += points;
        _UiManager.UpdateScore(Score);

    }


     IEnumerator TripleShotRoutine()
     {
         yield return new WaitForSeconds(5);
         isTripleShot = false;
     }
     IEnumerator SpeedUpRoutine()
     {
         yield return new WaitForSeconds(5);
         isSpeedUp = false;
         _vel = _defaultvel;
     }

}   
