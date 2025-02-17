using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerMove : AState<Player>
    {
        private int _moveAnim;
        //RotationVars
        private float rotationSpeed = 500f;
        private Camera _mainCamera;

        public override void Start(Player runner)
        {
            base.Start(runner);
            _mainCamera = Camera.main;

            _moveAnim = Animator.StringToHash("isWalking");
            runner.anim.SetBool(_moveAnim, true);
        }

        public override void Update(Player runner)
        {
            ApplyRotation(runner);
            ApplyMovement(runner);
            Sprint(runner);

            if (runner.inputAxis.magnitude <= 0.1f)
            {
                onSwitch(runner._idleState);
            }
            if (!runner.isOnGround)
            {
                onSwitch(runner._fallingState);
            }
        }

        public override void Complete(Player runner)
        {
            Debug.Log("Switching out of Move");
            base.Complete(runner);

            runner.anim.SetBool(_moveAnim, false);
        }


        //StateFunctions:
        private void ApplyMovement(Player runner)
        {
            var targetSpeed = runner.moveStruct.isSprinting ? runner.moveStruct.speed * runner.moveStruct.multiplier : runner.moveStruct.speed;
            runner.moveStruct.currentSpeed = Mathf.MoveTowards(runner.moveStruct.currentSpeed, targetSpeed, runner.moveStruct.acceleration * Time.deltaTime);

            runner.charCon.Move(runner.direction * runner.moveStruct.currentSpeed * Time.deltaTime);
        }

        private void ApplyRotation(Player runner)
        {
            if (runner.inputAxis.sqrMagnitude == 0) return;

            runner.direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(runner.inputAxis.x, 0.0f, runner.inputAxis.y);
            var targetRotation = Quaternion.LookRotation(runner.direction, Vector3.up);

            runner.transform.rotation = Quaternion.RotateTowards(runner.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void Sprint(Player runner)
        {
            runner.moveStruct.isSprinting = Input.GetKey(KeyCode.LeftShift);
        }
    }


}