using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private GameObject _GameOverText;
    [SerializeField]
    private GameObject _restarttext;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;

    private bool dead = false;

    private GameManager _GM;

    // Start is called before the first frame update
    void Start()
    {

        _restarttext.gameObject.SetActive(false);
        _GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_GM == null)
        {
            Debug.Log("Game Manger is Null");
        }
        //_livesSprites[CurrentPlayerLives = 3];
        _ScoreText.text = "Pontos : 0";
    }
    // Update is called once per frame

    public void UpdateScore(int PlayerScore)
    {
        _ScoreText.text = "Pontos : " + PlayerScore.ToString();
    }
    public void UpdateLives(int CurrentLives)
    {
        _LivesImg.sprite = _livesSprites[CurrentLives];
    }
    public void GameOver()
    {
        _restarttext.gameObject.SetActive(true);
        dead = true;
        StartCoroutine(GameOverRoutine(_GameOverText, dead));
        _GM.GameOver();
    }
    public static IEnumerator GameOverRoutine(GameObject GoverText, bool dead)
    {
        while(dead == true)
        {
            GoverText.SetActive(true);
            yield return new WaitForSeconds(0.35f);
            GoverText.SetActive(false);
            yield return new WaitForSeconds(0.35f);
        }

    }
}
