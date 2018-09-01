using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour 
{
  [SerializeField] int PlayerLives = 3;
  [SerializeField] int Score = 0;
  [SerializeField] Text LivesText;
  [SerializeField] Text ScoreText;

  void Awake()
  {
    int numGameSessions = GameObject.FindObjectsOfType<GameSession>().Length;
    if (numGameSessions > 1)
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }

  // Use this for initialization
  void Start () 
  {
    UpdateGameSessionInfo();
  }

  public void AddToScore(int score)
  {
    Score += score;
    UpdateGameSessionInfo();
  }
  
  public void ProcessPlayerDeath()
  {
    if (PlayerLives > 1)
      TakeLife();
    else
      ResetGameSession();
  }

  private void TakeLife()
  {
    PlayerLives--;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    UpdateGameSessionInfo();
  }

  private void ResetGameSession()
  {
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }

  private void UpdateGameSessionInfo()
  {
    LivesText.text = PlayerLives.ToString();
    ScoreText.text = Score.ToString();
  }
}
