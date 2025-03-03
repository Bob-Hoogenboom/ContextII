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
            ApplyFallingGravity(runner);

            runner.charCon.Move(runner.direction * Time.deltaTime);

            if (runner.isOnGround)
            {
                onSwitch(runner._idleState);
            }
        }

        private void ApplyFallingGravity(Player runner)
        {
            runner.velocity += runner.gravity * runner.gravityMultiplier * Time.deltaTime;
            runner.direction.y = runner.velocity;
        }

        public override void Complete(Player runner)
        {
            base.Complete(runner);

            runner.velocity = 0; //Make sure the character doesn't fall 
            runner.anim.SetBool(_fallingAnim, false);
        }
    }
}