﻿using System;
using Assets.Scripts.Player;
using Assets.Scripts.Player.States;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using XInputDotNetPure;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof (PlayerController))]
    public class PlayerControllerInput : MonoBehaviour
    {
        [SerializeField] public int playerNumber = 0;
        [SerializeField] public bool tapJump = true;
        [SerializeField] public bool vibration = true;
        internal int XIndex;
        private PlayerController character;
        private bool xActive = false;
        private bool yActive = false;

        private const float thresholdX = 0.1f;
        private const float thresholdY = 1f;

        private float x = 0f;
        private float y = 0f;
        private bool upPressed = false;
        private bool downPressed = false;
        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool jump = false;
        private bool run = false;
        private bool action1 = false;
        private bool action2 = false;

        private readonly string[] player = {"K", "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10", "J11"};

        private void Awake()
        {
            character = GetComponent<PlayerController>();
        }

        private void Start()
        {
            print(playerNumber);
            print(XIndex);
            print("");
        }

        private void Update()
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal" + player[playerNumber]);
            y = CrossPlatformInputManager.GetAxis("Vertical" + player[playerNumber]);

            // Check if axes have just been pressed
            if (Input.GetAxisRaw("Horizontal" + player[playerNumber]) != 0)
            {
                if (!xActive)
                {
                    if (Input.GetAxisRaw("Horizontal" + player[playerNumber]) < 0)
                    {
                        leftPressed = true;
                    }
                    else
                    {
                        rightPressed = true;
                    }
                    xActive = true;
                }
            }
            else
            {
                xActive = false;
            }

            if (Input.GetAxisRaw("Vertical" + player[playerNumber]) != 0)
            {
                if (!yActive)
                {
                    if (Input.GetAxisRaw("Vertical" + player[playerNumber]) < 0)
                    {
                        downPressed = true;
                    }
                    else
                    {
                        upPressed = true;
                    }
                    yActive = true;
                }
            }
            else
            {
                yActive = false;
            }
            
            // Remove noise
//            if (Mathf.Abs(x) < thresholdX)
//                x = 0;
//            if (Mathf.Abs(y) < thresholdY)
//                y = 0;
            
            // Check button presses
            if (!jump)
            {
                jump = CrossPlatformInputManager.GetButtonDown("Jump" + player[playerNumber]);
            }

            run = CrossPlatformInputManager.GetButton("Run" + player[playerNumber]);

            if (!action1)
            {
                action1 = CrossPlatformInputManager.GetButtonDown("Primary" + player[playerNumber]);
            }

            if (!action2)
            {
                action2 = CrossPlatformInputManager.GetButtonDown("Secondary" + player[playerNumber]);
            }

        }


        private void FixedUpdate()
        {
            // Execute character moves
            if (character.GetState() != null)
            {
                character.GetState().Move(x, y);
                if (upPressed)
                {
                    character.GetState().Up();
                    upPressed = false;
                }
                if (downPressed)
                {
                    character.GetState().Down();
                    downPressed = false;
                }
                if (leftPressed)
                {
                    character.GetState().Left();
                    leftPressed = false;
                }
                if (rightPressed)
                {
                    character.GetState().Right();
                    rightPressed = false;
                }
                if (jump)
                {
                    character.GetState().Jump();
                    jump = false;
                }
                character.run = run;
                if (action1)
                {
                    character.GetState().Primary(x, y);
                    action1 = false;
                }
                if (action2)
                {
                    character.GetState().Secondary(x, y);
                    action2 = false;
                }
            }
        }

        public bool ButtonActive(string name)
        {
            return Input.GetButton(name + player[playerNumber]);
        }

        public bool AxisActive(string name)
        {
            if (Input.GetAxisRaw(name + player[playerNumber]) != 0)
            {
                return true;
            }
            return false;
        }

        public bool AxisPositive(string name)
        {
            if (Input.GetAxisRaw(name + player[playerNumber]) > 0)
            {
                return true;
            }
            return false;
        }

        public bool AxisNegative(string name)
        {
            if (Input.GetAxisRaw(name + player[playerNumber]) < 0)
            {
                return true;
            }
            return false;
        }

        public void VibrateController(float leftMotor, float rightMotor)
        {
//            print("Trying to vibrate " + (PlayerIndex)(playerNumber));
            if (XIndex != -1 && vibration)
            {
                GamePad.SetVibration((PlayerIndex) XIndex, leftMotor, rightMotor);
            }
        }

        public void StopVibration()
        {
            if (XIndex != -1 && vibration)
            {
                GamePad.SetVibration((PlayerIndex) XIndex, 0f, 0f);
            }
        }
    }
}