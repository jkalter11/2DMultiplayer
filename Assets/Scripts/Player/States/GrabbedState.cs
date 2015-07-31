﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.States
{
    public class GrabbedState : PlayerState
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        public override string GetName()
        {
            return "DefaultState";
        }
    }
}