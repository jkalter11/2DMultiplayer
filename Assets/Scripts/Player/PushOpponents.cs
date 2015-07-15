﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;

namespace Assets.Scripts.Player
{
    public class PushOpponents : MonoBehaviour
    {
        [SerializeField] private bool right;
        private PlayerController playerController;
        private Rigidbody2D rigidBody;
        private List<PlayerController> touchingPlayers;
        private float pushSpeed = 2f;

        private void Awake()
        {
            playerController = transform.parent.GetComponentInChildren<PlayerController>();
            rigidBody = transform.parent.GetComponentInChildren<Rigidbody2D>();
            touchingPlayers = new List<PlayerController>();
        }

        private void FixedUpdate()
        {
            foreach (PlayerController player in touchingPlayers)
            {
                if (right)
                {
                    if (Mathf.Abs(player.externalVelocity.x) < pushSpeed)
                    {
                        player.SetExternalVelocityX(pushSpeed);
                    }
                }
                else
                {
                    if (Mathf.Abs(player.externalVelocity.x) < pushSpeed)
                    {
                        player.SetExternalVelocityX(-pushSpeed);
                    }
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerController otherController = other.GetComponent<PlayerController>();
                if (!touchingPlayers.Contains(otherController) && otherController != playerController)
                {
                    touchingPlayers.Add(otherController);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerController otherController = other.GetComponent<PlayerController>();
                if (touchingPlayers.Contains(otherController))
                {
                    touchingPlayers.Remove(otherController);
                }
            }
        }
    }
}