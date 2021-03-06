﻿using UnityEngine;

namespace Assets.Scripts.Player.StateBehaviours
{
    public class InterpolateToFacingRelativeVelocity : StateMachineBehaviour
    {
        [SerializeField] private int frameApplied = 0;
        [SerializeField] private float rate = 0;
        [SerializeField] private bool interpolateX = false;
        [SerializeField] private bool interpolateY = false;
        [SerializeField] private Vector2 velocity = new Vector2();
        private int countDown = 0;
        private bool applied = false;
        private float directionModifier = 1f;
        private PlayerController playerController;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerController = animator.GetComponentInChildren<PlayerController>();
            countDown = frameApplied;
            applied = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (countDown != 0 && !applied)
            {
                countDown--;
            }
            else if (!applied)
            {
                directionModifier = 1f;

//                if (DamageScaled)
//                {
//                    damageMultiplier = playerController.shield;
//                }

                if (!playerController.facingRight)
                {
                    directionModifier = -1f;
                }

                if (interpolateX)
                {
                    if (playerController.GetVelocityX() > velocity.x)
                    {
                        playerController.IncrementVelocityX(-rate);
                        if (playerController.GetVelocityX() < velocity.x)
                        {
                            playerController.SetVelocityX(velocity.x);
                            applied = true;
                        }
                    }
                    else if (playerController.GetVelocityX() < velocity.x)
                    {
                        playerController.IncrementVelocityX(rate);
                        if (playerController.GetVelocityX() > velocity.x)
                        {
                            playerController.SetVelocityX(velocity.x);
                            applied = true;
                        }
                    }
                }
                if (interpolateY)
                {
                    if (playerController.GetVelocityY() > velocity.y)
                    {
                        playerController.IncrementVelocityY(-rate);
                        if (playerController.GetVelocityX() < velocity.x)
                        {
                            playerController.SetVelocityX(velocity.x);
                            applied = true;
                        }
                    }
                    else if (playerController.GetVelocityY() < velocity.y)
                    {
                        playerController.IncrementVelocityY(rate);
                        if (playerController.GetVelocityX() > velocity.x)
                        {
                            playerController.SetVelocityX(velocity.x);
                            applied = true;
                        }
                    }
                }
            }
        }       
    }
}