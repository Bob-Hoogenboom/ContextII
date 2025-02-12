using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerMove : AState<Player>
    {
        //RotationVars
        private float rotationSpeed = 500f;
        private Camera _mainCamera;

        public override void Start(Player runner)
        {
            base.Start(runner);
            _mainCamera = Camera.main;
        }

        public override void Update(Player runner)
        {
            ApplyRotation(runner);

            if (runner.inputAxis.magnitude <= 0.1f)
            {
                onSwitch(runner._idleState);
            }
        }

        public override void Complete(Player runner)
        {
            Debug.Log("Switching out of Move");
            base.Complete(runner);
        }


        //StateFunctions:
        private void ApplyRotation(Player runner)
        {
            if (runner.inputAxis.sqrMagnitude == 0) return;

            runner.direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(runner.inputAxis.x, 0.0f, runner.inputAxis.y);
            var targetRotation = Quaternion.LookRotation(runner.direction, Vector3.up);

            runner.transform.rotation = Quaternion.RotateTowards(runner.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


}