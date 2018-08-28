using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
  [SerializeField] float MoveSpeed = 1f;

  private Rigidbody2D _rigidBody;
  private BoxCollider2D _edgeDetectorCollider;

  // Use this for initialization
  void Start () 
  {
    _rigidBody = GetComponent<Rigidbody2D>();
    _rigidBody.velocity = Vector2.right * MoveSpeed;
  }

  // Update is called once per frame
  void Update () 
  {
  }
}
