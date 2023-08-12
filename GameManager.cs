using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    private bool _buttonpress = false;

    private void Update()
    {
        Reestart();
    }

    public void Reestart()
    {
        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            
            SceneManager.LoadScene(1); //Current Game scene
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    


}
