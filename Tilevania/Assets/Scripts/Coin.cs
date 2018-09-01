using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
  [SerializeField] AudioClip PickUpSfx;
  [SerializeField] public int ScoreValue = 100;

  void OnTriggerEnter2D(Collider2D other)
  {
    AudioSource.PlayClipAtPoint(PickUpSfx, Camera.main.transform.position);
    Destroy(gameObject);
  }  
}
