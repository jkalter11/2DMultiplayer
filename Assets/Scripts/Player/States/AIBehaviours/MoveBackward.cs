﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.States.AIBehaviours
{
    public class MoveBackward : AIBehaviour
    {
        public override void Process(List<Transform> opponentPositions)
        {
            if (playerController.facingRight)
            {
                PlayerInputController.MoveX(-1);
            }
            else
            {
                PlayerInputController.MoveX(1);
            }
            if (!TimedDisable)
            {
                Disable();
            }
        }
    }
}