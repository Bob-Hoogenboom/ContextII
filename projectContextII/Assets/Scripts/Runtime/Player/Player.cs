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
        public WorldSpaceUI worldSpaceUI;
        public Animator anim;

        [Header("Movement")]
        public Vector3 direction;
        public Vector2 inputAxis;

        public float speed = 5f;
        public Movement moveStruct;

        [Header("Detection")]
        public bool interactHit;
        [SerializeField] private float rayLength = 1f;
        [SerializeField] private float rayHeight = 1f;

        [Header("Interaction")]
        public GameObject interactOBJ;
        private IInteractable lastInteractable = null;
        public bool isInteracting = false;

        [Header("Gravity")]
        public bool isOnGround;
        public Vector3 heightOffset;
        public Vector3 boxSize; //Detection box for the ground checking 
        [HideInInspector] public float velocity;
        [HideInInspector] public float gravity = -9.81f;
        [HideInInspector]public float gravityMultiplier = 3.0f;  //Multiplier for when the player is falling

        [Header("StateMachine")]
        public StateMachine<Player> stateMachine;
        public ScratchPad sharedData => new ScratchPad();

        //states
        public PlayerIdle _idleState { get; private set; } = new PlayerIdle();
        public PlayerMove _moveState { get; private set; } = new PlayerMove();
        public PlayerFalling _fallingState { get; private set; } = new PlayerFalling();
        public PlayerTalk talkingState { get; private set; } = new PlayerTalk();
        public PlayerPush pushingState { get; private set; } = new PlayerPush();
        public PlayerClimb climbingState { get; private set; }  = new PlayerClimb();


        private void Start()
        {
            charCon = gameObject.GetComponent<CharacterController>();
            worldSpaceUI = FindObjectOfType<WorldSpaceUI>();
            anim = gameObject.GetComponentInChildren<Animator>();

            stateMachine = new StateMachine<Player>(this);
            stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            MoveInput();
            CheckInteract();

            stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            stateMachine?.FixedUpdate();
            CheckPlayerGround();
        }

        public void MoveInput()
        {
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            direction = new Vector3(0, 0.0f, 0);
        }

        private void CheckPlayerGround()
        {
            Collider[] floorHit = Physics.OverlapBox(transform.position + heightOffset, boxSize / 2, Quaternion.identity);
            if (floorHit.Length > 0)
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
        }

        private void CheckInteract()
        {
            RaycastHit hit;
            Vector3 pos = transform.position;
            Vector3 rayOrigin = new Vector3(pos.x, pos.y + rayHeight, pos.z);

            if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, rayLength))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                if (interactable == null)
                {
                    interactHit = false;
                    return;
                }

                if (lastInteractable != interactable)
                {
                    lastInteractable?.HidePopUp();
                    lastInteractable = interactable;
                }

                InteractType interactType = interactable.interactType;

                interactable.InteractPopUp();

                interactHit = true;
                if (Input.GetKeyDown(KeyCode.Space) && !isInteracting)
                {
                    switch (interactType)
                    {
                        case InteractType.PUSHABLE:
                            isInteracting = true;
                            interactable.Interact();
                            interactOBJ = hit.collider.gameObject;
                            stateMachine.SetState(pushingState);
                            Debug.Log("Object is pushable!");
                            //OnSwitch PushingState
                            break;

                        case InteractType.TALKABLE:
                            isInteracting = true;
                            interactable.Interact();
                            interactOBJ = hit.collider.gameObject;
                            stateMachine.SetState(talkingState);
                            Debug.Log("Object is talkable!");
                            //OnSwitch TalkingState
                            break;

                        case InteractType.CLIMBABLE:
                            isInteracting = true;
                            interactable.Interact();
                            interactOBJ = hit.collider.gameObject;
                            stateMachine.SetState(climbingState);
                            Debug.Log("Object is climbable!");
                            //OnSwitch ClimbingState
                            break;

                        case InteractType.DEFAULT:
                            interactable.Interact();
                            break;

                        default:
                            Debug.LogWarning("Wow buddy thats not interactable");
                            break;
                    }
                }
            }
            else
            {
                if (lastInteractable != null)
                {
                    lastInteractable.HidePopUp();
                    lastInteractable = null;
                }
            }
        }

        [Serializable]
        public struct Movement
        {
            public float speed;//5
            public float multiplier;//2
            public float acceleration;//100

            [HideInInspector] public bool isSprinting;
            [HideInInspector] public float currentSpeed;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position + heightOffset, boxSize);

            Vector3 pos = transform.position;
            Vector3 rayOrigin = new Vector3(pos.x, pos.y + rayHeight, pos.z);
            Gizmos.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * rayLength);
        }
    }
}