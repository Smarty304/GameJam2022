using UnityEngine;

using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    [Header("Single Player - Input"), Space]
    public PlayerBrain playerBrain;
    public PlayerInput playerInput;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
    }
}
