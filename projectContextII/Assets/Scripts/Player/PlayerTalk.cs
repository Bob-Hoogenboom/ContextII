using Cinemachine;
using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerTalk : AState<Player>
    {
        private CinemachineFreeLook fLCam;
        //private dialogue camera variable*

        public override void Start(Player runner)
        {
            base.Start(runner);
            fLCam = Camera.main.GetComponent<CinemachineFreeLook>(); //switch to a dialogue camera
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
            //switch back to the freelook orbit camera
        }
    }
}