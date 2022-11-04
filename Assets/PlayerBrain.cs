using UnityEngine;

using UnityEngine.InputSystem;
public class PlayerBrain : MonoBehaviour
{

    private int playerID;
    public int PlayerID { get => playerID; private set => playerID = value;}


    [Header("Input Settings")]
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput PlayerInput { get => playerInput; private set => playerInput = value; }

}
