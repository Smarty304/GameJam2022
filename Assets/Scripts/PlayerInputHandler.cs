using UnityEngine;

using UnityEngine.InputSystem;

namespace UnityCustomScripts
{
    namespace InputSystem
    {
        public class PlayerInputHandler : MonoBehaviour
        {
            [SerializeField] private PlayerInputReceiver playerInputReceiver;

            private void Awake()
            {
                if (playerInputReceiver == null)
                {
                    Debug.LogWarning("No Current Player Input Receiver.");
                }
            }

            public void SEND_MoveInput(InputAction.CallbackContext context)
            {
                if (playerInputReceiver == null)
                {
                    Debug.LogWarning("No Current Player Input Receiver.");
                    return;
                }
                playerInputReceiver.RECEIVE_MoveInput(context);
            }

            public void SEND_SprintInput(InputAction.CallbackContext context)
            {
                playerInputReceiver.RECEIVE_SprintInput(context);
            }

            public void SEND_JumpInput(InputAction.CallbackContext context)
            {
                playerInputReceiver.RECEIVE_JumpInput(context);
            }

            public void SEND_InteractInput(InputAction.CallbackContext context)
            {
                playerInputReceiver.RECEIVE_InteractInput(context);
            }
        }
    }
}


