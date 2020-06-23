using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Image liveImageDisplay;
    public Sprite[] lives;
    public float score;
    public Text scoretext;
    public Text pressSpace;
    public Image title;
    public bool gameOver;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _spawn;
    public void Start()
    {
        _spawn.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown("space"))
            {
                clearScore();
                title.gameObject.SetActive(false);
                pressSpace.gameObject.SetActive(false);
                Instantiate(_player, new Vector3(0, 0, 0), Quaternion.identity);
                _spawn.gameObject.SetActive(true);
                updateScore(0);
                gameOver = false;
            } 
        }
    }

    public void updateLife(int currentLives)
    {
       liveImageDisplay.sprite = lives[currentLives];
    }

    public void updateScore(int scoreIncrement)
    {
        score += scoreIncrement;
        scoretext.text = "Score: " + score;  
    }

    public void clearScore()
    {
        score = 0;
    }

    
}
