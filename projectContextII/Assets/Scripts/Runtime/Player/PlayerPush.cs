using FSM;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerPush : AState<Player>
    {
        private GameObject pushableObject;
        private Vector3 moveDirection;
        private float pushSpeed = 3f; // Movement speed while pushing
        private float pullSpeed = 1f; // Movement speed while pulling
        private bool isPulling = false;

        private int _pushIdleAnim;
        private int _pushWalkingAnim;


        public override void Start(Player runner)
        {
            base.Start(runner);
            pushableObject = runner.interactOBJ;

            _pushIdleAnim = Animator.StringToHash("pushReady");
            _pushWalkingAnim = Animator.StringToHash("isPushing");

            Vector3 boxPos = runner.interactOBJ.transform.position;
            Vector3 runnerPos = runner.transform.position;
            Vector3 forward = runner.transform.forward;

            float dotX = Vector3.Dot(forward, Vector3.right);   // +X
            float dotNegX = Vector3.Dot(forward, Vector3.left); // -X
            float dotZ = Vector3.Dot(forward, Vector3.forward); // +Z
            float dotNegZ = Vector3.Dot(forward, Vector3.back); // -Z

            // Find the maximum dot product value to determine the strongest direction
            float maxDot = Mathf.Max(dotX, dotNegX, dotZ, dotNegZ);

            if (maxDot == dotX)
            {
                Debug.Log("Player is looking mostly towards +X (right)");
                runner.transform.position = new Vector3(runnerPos.x, runnerPos.y, boxPos.z); // Snap Z, keep X
            }
            else if (maxDot == dotNegX)
            {
                Debug.Log("Player is looking mostly towards -X (left)");
                runner.transform.position = new Vector3(runnerPos.x, runnerPos.y, boxPos.z); // Snap Z, keep X
            }
            else if (maxDot == dotZ)
            {
                Debug.Log("Player is looking mostly towards +Z (forward)");
                runner.transform.position = new Vector3(boxPos.x, runnerPos.y, runnerPos.z); // Snap X, keep Z
            }
            else if (maxDot == dotNegZ)
            {
                Debug.Log("Player is looking mostly towards -Z (backward)");
                runner.transform.position = new Vector3(boxPos.x, runnerPos.y, runnerPos.z); // Snap X, keep Z
            }

            runner.anim.SetBool(_pushIdleAnim, true);
        }

        public override void Update(Player runner)
        {


            float vertical = runner.inputAxis.y;    // W/S or Up/Down

            // Ensure movement input is valid
            if (vertical != 0)
            {
                runner.anim.SetBool(_pushWalkingAnim, true);

                // Convert input into world-space movement direction based on the player's forward
                Vector3 inputDirection = runner.transform.forward * vertical; // Use only Z movement

                // Determine dominant movement axis
                if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.z))
                {
                    moveDirection = new Vector3(Mathf.Sign(inputDirection.x), 0, 0); // Lock to X-axis
                }
                else
                {
                    moveDirection = new Vector3(0, 0, Mathf.Sign(inputDirection.z)); // Lock to Z-axis
                }

                Debug.Log(inputDirection);

                // Determine speed (push vs pull)
                isPulling = vertical < 0;
                float moveSpeed = isPulling ? pullSpeed : pushSpeed;


                runner.charCon.Move(moveDirection * moveSpeed * Time.deltaTime);

                // Move the object in sync with the player
                Vector3 targetPosition;
                if (isPulling)
                {
                    targetPosition = runner.transform.position + moveDirection; // Keep object in front/back
                }
                else
                {
                    targetPosition = runner.transform.position + moveDirection * 2.5f; // Keep object in front/back
                }

                //# lerp slightly pushes the box left or right
                pushableObject.transform.position = Vector3.Lerp(pushableObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                runner.anim.SetBool(_pushWalkingAnim, false);
            }

            if (!runner.isOnGround)
            {
                onSwitch(runner._fallingState);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                onSwitch(runner._idleState);
            }
        }

        public override void Complete(Player runner)
        {
            base.Complete(runner);
            runner.interactOBJ = null;
            runner.isInteracting = false;

            runner.anim.SetBool(_pushIdleAnim, false);
            runner.anim.SetBool(_pushWalkingAnim, false);
        }
    } 
}