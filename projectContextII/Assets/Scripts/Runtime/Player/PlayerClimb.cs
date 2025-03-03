using FSM;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerClimb : AState<Player>
    {
        private ClimbTrigger _trigger;
        private RaycastHit hit;
        private float _raylength;


        private float _climbSpeed = 3f;
        private float _timeToLandingSpot = 1f;
        private bool _isMovingToGoal = false;



        public override void Start(Player runner)
        {
            base.Start(runner);
            
            runner.transform.LookAt(runner.interactOBJ.transform.position, Vector3.up);

            _raylength = (runner.transform.position - runner.interactOBJ.transform.position).magnitude;

            _trigger = runner.interactOBJ.GetComponent<ClimbTrigger>();
        }

        public override void Update(Player runner)
        {
            base.Update(runner);
                
            Climbing(runner);


            if (Physics.Raycast(runner.transform.position, runner.transform.TransformDirection(Vector3.forward), out hit, _raylength))
            {
                if (hit.collider == _trigger.ladderTop)
                {
                    if (_isMovingToGoal) return;
                    _isMovingToGoal = true;
                    Debug.Log("Top reached");
                    runner.StartCoroutine( MoveToTarget(_trigger.climbLanding.position, runner));
                   
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                onSwitch(runner._idleState);
            }
        }

        public override void Complete(Player runner)
        {
            base.Complete(runner);

            Debug.Log("Exit Climb State");

            runner.interactOBJ = null;
            runner.isInteracting = false;
            _isMovingToGoal = false;
        }

        private void Climbing(Player runner)
        {
            Vector3 targetDirection = new Vector3(0, runner.inputAxis.y * _climbSpeed, 0f);
            //runner.moveStruct.currentSpeed = Mathf.MoveTowards(runner.moveStruct.currentSpeed, runner.moveStruct.speed, runner.moveStruct.acceleration * Time.deltaTime);
            if (_isMovingToGoal) return;
            runner.charCon.Move(targetDirection * Time.deltaTime);
        }

        IEnumerator MoveToTarget(Vector3 target, Player runner)
        {
            float elapsedTime = 0;
            Vector3 currentPos = runner.transform.position;

            while (elapsedTime < _timeToLandingSpot)
            {
                runner.transform.position = Vector3.Lerp(currentPos, target, (elapsedTime / _timeToLandingSpot));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            runner.transform.position = target;
            onSwitch(runner._idleState);
            yield return null;


        }

    }
}