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
        public Vector3 direction;
        public Vector2 inputAxis;

        public float speed = 5f;
        public Movement moveRoutine; //5, 2, 100


        [Header("Gravity")]
        public float gravityMultiplier = 3.0f;
        [HideInInspector] public float gravity = -9.81f;
        [HideInInspector] public float velocity;
        public Vector3 boxSize;
        public float maxDistance;
        public bool isOnGround;

        [Header("StateMachine")]
        public StateMachine<Player> stateMachine;
        public ScratchPad sharedData => new ScratchPad();
        //states
        public PlayerIdle _idleState { get; private set; } = new PlayerIdle();
        public PlayerMove _moveState { get; private set; } = new PlayerMove();
        public PlayerFalling _fallingState { get; private set; } = new PlayerFalling();


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
            CheckPlayerGround();

            stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            stateMachine?.FixedUpdate();
        }

        public void MoveInput()
        {
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            direction = new Vector3(inputAxis.x, 0.0f, inputAxis.y);
        }

        private void CheckPlayerGround()
        {
            if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation , maxDistance))
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position-transform.up* maxDistance, boxSize);
    }
    }
}