using FSM;
using System;
using UnityEngine;

// This Player Controller uses the Finite Statemachine from a previous class at HKU
namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IStateRunner
    {
        [Header("References")]
        public CharacterController charCon;
        public Animator anim;

        [Header("Movement")]
        [HideInInspector] public Vector3 direction;
        [HideInInspector] public Vector2 inputAxis;

        public float speed = 5f;
        public Movement moveRoutine; //5, 2, 100


        [Header("Gravity")]
        public float gravityMultiplier = 3.0f;
        [HideInInspector] public float gravity = -9.81f;
        [HideInInspector] public float velocity;


        [Header("StateMachine")]
        public StateMachine<Player> stateMachine;
        public ScratchPad sharedData => new ScratchPad();
        //states
        public PlayerIdle _idleState { get; private set; } = new PlayerIdle();
        public PlayerMove _moveState { get; private set; } = new PlayerMove();


        private void Start()
        {
            charCon = gameObject.GetComponent<CharacterController>();
            anim = gameObject.GetComponentInChildren<Animator>();

            stateMachine = new StateMachine<Player>(this);
            stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            MoveInput();

            stateMachine?.Update();
            ApplyGravity();
            ApplyMovement();
        }

        private void FixedUpdate()
        {
            stateMachine?.FixedUpdate();
        }

        private void ApplyGravity()
        {
            if (charCon.isGrounded && velocity < 0.0f)
            {
                velocity = -1.0f;
            }
            else
            {
                velocity += gravity * gravityMultiplier * Time.deltaTime;
            }

            direction.y = velocity;
        }

        private void ApplyMovement()
        {
            var targetSpeed = moveRoutine.isSprinting ? moveRoutine.speed * moveRoutine.multiplier : moveRoutine.speed;
            moveRoutine.currentSpeed = Mathf.MoveTowards(moveRoutine.currentSpeed, targetSpeed, moveRoutine.acceleration * Time.deltaTime);

            charCon.Move(direction * moveRoutine.currentSpeed * Time.deltaTime);
        }

        public void MoveInput()
        {
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            direction = new Vector3(inputAxis.x, 0.0f, inputAxis.y);
        }

        public void Sprint()
        {
            moveRoutine.isSprinting = Input.GetKeyDown(KeyCode.LeftShift);
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

    }
}