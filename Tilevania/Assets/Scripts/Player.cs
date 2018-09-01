using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [SerializeField] float RunSpeed = 5f;
  [SerializeField] float JumpSpeed = 5f;
  [SerializeField] float ClimbSpeed = 5f;

  bool IsRunning = false;
  bool IsClimbing = false;
  bool IsAlive = true;

  private Rigidbody2D _rigidBody;
  private Animator _animator;
  private CapsuleCollider2D _playerCollider;
  private BoxCollider2D _feetCollider;
  private Vector2 _playerDirection;
  private GameSession _gameSession;

  private float _startingGravity;
  private bool _allowedToClimb = true;

  // Use this for initialization
  void Start()
  {
    _rigidBody = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _playerCollider = GetComponent<CapsuleCollider2D>();
    _feetCollider = GetComponent<BoxCollider2D>();
    _gameSession = FindObjectOfType<GameSession>();

    _startingGravity = _rigidBody.gravityScale;
  }

  // Update is called once per frame
  void Update()
  {
    if (IsAlive)
    {
      Run();
      FlipSprite();
      Jump();
      ClimbLadder();
      CheckHazard();
    }
  }

  void FixedUpdate()
  {
    //Run(_playerDirection);
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (collider.name == "Climbing")
      _allowedToClimb = true;
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.name == "Climbing")
      _allowedToClimb = true;

    // Player collided with enemy
    if (collider.GetComponent<EnemyMovement>())
    {
      Die();
    }

    var coin = collider.GetComponent<Coin>();
    if (coin)
    {
      PickupCoin(coin);
    }
  }

  private void Run()
  {
    var horizValue = CrossPlatformInputManager.GetAxis("Horizontal");
    _rigidBody.velocity = new Vector2(horizValue * RunSpeed, _rigidBody.velocity.y);
    IsRunning = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;
    _animator.SetBool("IsRunning", IsRunning);
  }

  private void FlipSprite()
  {
    if (IsRunning)
    {
      transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1f);
    }
  }

  private void Jump()
  {
    if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing")))
      return;

    if (CrossPlatformInputManager.GetButtonDown("Jump"))
    {
      var jumpVelocity = new Vector2(0f, JumpSpeed);
      _rigidBody.velocity += jumpVelocity;
      _allowedToClimb = false;
    }
  }

  private void ClimbLadder()
  {
    if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    {
      SetNotClimbingState();
      return;
    }

    var vertKeyPressed = CrossPlatformInputManager.GetButton("Vertical");
    if (vertKeyPressed)
      _allowedToClimb = true;

    if (!_allowedToClimb)
    {
      SetNotClimbingState();
      return;
    }

    IsClimbing = Mathf.Abs(_rigidBody.velocity.y) > Mathf.Epsilon;

    var vertValue = CrossPlatformInputManager.GetAxis("Vertical");
    var climbingVelocity = new Vector2(_rigidBody.velocity.x, vertValue * ClimbSpeed);
    _animator.SetBool("IsClimbing", IsClimbing);
    _rigidBody.gravityScale = 0f;
    _rigidBody.velocity = climbingVelocity;
  }

  private void SetNotClimbingState()
  {
    IsClimbing = false;
    _animator.SetBool("IsClimbing", IsClimbing);
    _rigidBody.gravityScale = _startingGravity;
  }

  private void Die()
  {
    _animator.SetTrigger("Death");
    IsAlive = false;
    _gameSession.ProcessPlayerDeath();
  }

  private void CheckHazard()
  {
    if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
    {
      Die();
    }
  }

  private void PickupCoin(Coin coin)
  {
    _gameSession.AddToScore(coin.ScoreValue);
  }
}
