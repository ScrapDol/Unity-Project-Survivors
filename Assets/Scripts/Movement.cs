using System;
using UnityEngine;

public enum ControlType
{
   PC,
   mobile
}

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
   public ControlType CurrentControlType
   {
      get { return _controlType; }
   }
   
   public Vector2 MoveInput
   {
      get { return _moveInput; }
   }

   [SerializeField] private GameObject _gUI;
   [SerializeField] private Joystick _joystick;
   [SerializeField] private float _speed;
   [SerializeField] private ControlType _controlType;
   [SerializeField] private Animator _animator;
   

   private Rigidbody2D _rigidbody;
   private Vector2 _moveInput;
   private Vector2 _moveVelocity;
   private bool _facingRight = true;
   private static readonly int IsRunning = Animator.StringToHash("isRunning");

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animator = GetComponent<Animator>();
      _rigidbody.freezeRotation = true;
      
      if (_controlType == ControlType.PC)
      {
         _gUI.SetActive(false);
      }
   }

   private void Update()
   {
      if (_controlType == ControlType.PC)
      {
         _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      }

      if (_controlType == ControlType.mobile)
      {
         _moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
      }

      if (_moveInput.x != 0 || _moveInput.y != 0)
      {
         _animator.SetBool(IsRunning, true);
      }
      else
      {
         _animator.SetBool(IsRunning, false);
      }

      if (_facingRight && _moveInput.x < 0)
      {
         Flip();
      }
      else if (!_facingRight && _moveInput.x > 0)
      {
         Flip();
      }
      
      _moveVelocity = _moveInput.normalized * _speed;
   }

   private void FixedUpdate()
   {
      _rigidbody.MovePosition(_rigidbody.position + _moveVelocity * Time.fixedDeltaTime);
   }

   private void Flip()
   {
      _facingRight = !_facingRight;
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
   }
}
