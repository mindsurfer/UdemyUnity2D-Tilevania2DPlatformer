using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour 
{
  private int _startingSceneIndex;

  void Awake()
  {
    if (FindObjectsOfType<ScenePersist>().Length > 1)
      Destroy(gameObject);
    else
      DontDestroyOnLoad(gameObject);
  }
  // Use this for initialization
  void Start () 
  {
    _startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
  }

  // Update is called once per frame
  void Update () 
  {
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    if (currentSceneIndex != _startingSceneIndex)
      Destroy(gameObject);
  }
}
