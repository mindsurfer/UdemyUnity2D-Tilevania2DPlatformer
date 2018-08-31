using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour 
{
  private Rigidbody2D _parentRigidBody;

  // Use this for initialization
  void Start () 
  {
    _parentRigidBody = GetComponentInParent<Rigidbody2D>();
  }
  
  // Update is called once per frame
  void Update () 
  {
    
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (collider.name == "Foreground")
    {
      FlipEnemy();
    }
  }

  private void FlipEnemy()
  {
    transform.parent.localScale = new Vector2(-(Mathf.Sign(transform.parent.localScale.x)), transform.parent.localScale.y);
    _parentRigidBody.velocity = new Vector2(-(_parentRigidBody.velocity.x), 0f);
  }
}
