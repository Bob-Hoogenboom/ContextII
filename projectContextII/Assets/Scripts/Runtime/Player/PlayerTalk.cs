using Cinemachine;
using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerTalk : AState<Player>
    {
        private int _talkAnim;
        private CinemachineFreeLook fLCam;
        //private dialogue camera variable*

        public override void Start(Player runner)
        {
            base.Start(runner);
            fLCam = Camera.main.GetComponent<CinemachineFreeLook>(); //switch to a dialogue camera

            _talkAnim = Animator.StringToHash("isTalking");
            runner.anim.SetBool(_talkAnim, true);
        }

        public override void Update(Player runner)
        {
            base.Update(runner);
            if (!runner.isInteracting)
            {
                onSwitch(runner._idleState);
            }
        }

        public override void Complete(Player runner)
        {
            base.Complete(runner);

            runner.interactOBJ = null;
            //switch back to the freelook orbit camera

            runner.anim.SetBool(_talkAnim, false);
        }
    }
}