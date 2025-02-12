using FSM;
using UnityEngine;

namespace Player
{
    public class PlayerIdle : AState<Player>
    {
        public override void Start(Player runner)
        {
            base.Start(runner);

        }

        public override void Update(Player runner)
        {


            if (runner.inputAxis.magnitude >= 0.1f)
            {
                onSwitch(runner._moveState);
            }
        }

        public override void Complete(Player runner)
        {
            Debug.Log("Switching out of Idle");
            base.Complete(runner);
        }
    }
}