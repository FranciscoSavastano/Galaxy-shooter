using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _powerupvel = 3;
    [SerializeField]
    private int _PowerUpId;
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        

  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerupvel * Time.deltaTime);

        if (transform.position.y <= -2.90f)
        {
            Destroy(this.gameObject);
        }
    

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, transform.position);
                switch (_PowerUpId)
                {
                    case 0:
                        player.TriplePowerup();
                        
                        break;
                    case 1:
                        player.SpeedPowerup();
                      
                        break;
                    case 2:
                        player.ShieldPowerup();
        
                        break;
                    default:
                        Debug.Log("Valor Padrão");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
