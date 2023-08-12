using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1); // MAIN GAME
    }

}