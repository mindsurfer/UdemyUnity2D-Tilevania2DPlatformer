using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour 
{
  [SerializeField] float LevelLoadDelay = 2f;
  [SerializeField] float LevelExitSloMoFactor = 0.2f;

  void Start()
  {
  }

  void OnTriggerEnter2D()
  {
    StartCoroutine(LoadNextLevel());
  }

  IEnumerator LoadNextLevel()
  {
    Time.timeScale = LevelExitSloMoFactor;
    yield return new WaitForSecondsRealtime(LevelLoadDelay);
    Time.timeScale = 1f;

    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
}
