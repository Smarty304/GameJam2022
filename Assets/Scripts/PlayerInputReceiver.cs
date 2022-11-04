
using System;
using UnityEngine;

using UnityEngine.InputSystem;

namespace UnityCustomScripts
{
    namespace InputSystem
    {
        public class PlayerInputReceiver : MonoBehaviour
        {
            private PlayerBrain playerBrain;
            [SerializeField] private PlayerController playerController; 

            private void Awake(){
                playerBrain = GetComponentInParent<PlayerBrain>();
            }

            public void RECEIVE_MoveInput(InputAction.CallbackContext context)
            {
                playerController.move = context.ReadValue<Vector2>();
            }
            public void RECEIVE_SprintInput(InputAction.CallbackContext context)
            {
                playerController.sprint = context.action.IsPressed();
            }
            public void RECEIVE_ShootInput(InputAction.CallbackContext context)
            {
                if (context.started) playerController.Shoot();
                //carMovement.brake = context.action.IsPressed();
            }
            public void RECEIVE_JumpInput(InputAction.CallbackContext context)
            {
                if(context.started){
                    playerController.jumpDown = false;
                    playerController.jumpUp = context.action.IsPressed();
                }
                if(context.canceled){
                    playerController.jumpUp = false;
                    playerController.jumpDown = context.action.WasReleasedThisFrame();
                }
            }
        }
    }
}


