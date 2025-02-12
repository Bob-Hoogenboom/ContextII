using System;
using UnityEngine;

/// <summary>
/// This script was taken from https://github.com/chonkgames/Simple-Character-Controller-in-Unity/blob/main/Assets/Scripts/PlayerController.cs
/// It only serves as a base/ quick functionality test and will be expended oppon
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector2 _inputAxis;

    //[SerializeField] private float speed = 5f;
    [SerializeField] private Movement movement;//5, 2, 100


    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 500f;
    private Camera _mainCamera;


    [Header("Gravity")]
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveInput();

        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        if (_inputAxis.sqrMagnitude == 0) return;

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_inputAxis.x, 0.0f, _inputAxis.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);

        _characterController.Move(_direction * movement.currentSpeed * Time.deltaTime);
    }

    public void MoveInput()
    {
        _inputAxis.x = Input.GetAxisRaw("Horizontal");
        _inputAxis.y = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(_inputAxis.x, 0.0f, _inputAxis.y);
    }


    public void Sprint()
    {
        movement.isSprinting = Input.GetKeyDown(KeyCode.LeftShift);
    }

    private bool IsGrounded() => _characterController.isGrounded;
}

[Serializable]
public struct Movement
{
    public float speed;
    public float multiplier;
    public float acceleration;

    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float currentSpeed;
}