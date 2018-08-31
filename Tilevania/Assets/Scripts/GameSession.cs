﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour 
{
  [SerializeField] int PlayerLives = 3;

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
  }

  private void ResetGameSession()
  {
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
}
