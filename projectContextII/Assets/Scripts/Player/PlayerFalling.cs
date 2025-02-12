using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerFalling : AState<Player>
    {
        private int _fallingAnim;
        public override void Start(Player runner)
        {
            base.Start(runner);

            _fallingAnim = Animator.StringToHash("isFalling");
            runner.anim.SetBool(_fallingAnim, true);
        }

        public override void Update(Player runner)
        {

            if (runner.charCon.isGrounded)
            {
                onSwitch(runner._idleState);
            }
        }

        private void ApplyGravity(Player runner)
        {
            runner.velocity += runner.gravity * runner.gravityMultiplier * Time.deltaTime;
            runner.direction.y = runner.velocity;

            runner.charCon.Move(runner.direction * Time.deltaTime);
        }

        public override void Complete(Player runner)
        {
            Debug.Log("Switching out of Falling");
            base.Complete(runner);

            runner.anim.SetBool(_fallingAnim, false);
        }
    }
}