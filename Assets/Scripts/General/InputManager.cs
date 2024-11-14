// Code by: Sidharth S (aka SidTheLoser)

using UnityEngine;
using UnityEngine.Serialization;

namespace General
{
    public class InputManager : MonoBehaviour
    {
        [Header("Player Movement Keybindings")] [SerializeField]
        private KeyCode forwardKeyCode = KeyCode.W;

        [SerializeField] private KeyCode backwardKeyCode = KeyCode.S;
        [SerializeField] private KeyCode leftwardKeyCode = KeyCode.A;
        [SerializeField] private KeyCode rightwardKeyCode = KeyCode.D;
        [SerializeField] private KeyCode jumpKeyCode = KeyCode.Space;
        [SerializeField] private KeyCode sprintKeyCode = KeyCode.LeftControl;

        [Header("Player Attack Keybindings")] [SerializeField]
        private KeyCode useKeyCode = KeyCode.E;

        [SerializeField] private KeyCode rangedAttackKeyCode = KeyCode.Mouse0;

        [Header("Misc")] [SerializeField] private KeyCode gamePauseKeyCode = KeyCode.Escape;
        [SerializeField] private GameObject pauseMenu;

        [HideInInspector] public static Vector3 MovementVector;
        [HideInInspector] public static bool JumpButtonPressed;
        [HideInInspector] public static bool UseButtonPressed;
        [HideInInspector] public static bool RangedAttackPressed;
        [HideInInspector] public static bool RangedAttackPressedDown;
        [HideInInspector] public static bool SprintButtonPressed;

        private void Awake()
        {
            if (gamePauseKeyCode == KeyCode.Escape && 
                (Application.isEditor || Application.platform == RuntimePlatform.WebGLPlayer)) 
                gamePauseKeyCode = KeyCode.P;
        }

        private void Update()
        {
            MovementVector = Vector2.zero;
            if (Input.GetKey(forwardKeyCode)) MovementVector.z += 1;
            if (Input.GetKey(backwardKeyCode)) MovementVector.z -= 1;
            if (Input.GetKey(rightwardKeyCode)) MovementVector.x += 1;
            if (Input.GetKey(leftwardKeyCode)) MovementVector.x -= 1;

            JumpButtonPressed = Input.GetKeyDown(jumpKeyCode);
            UseButtonPressed = Input.GetKeyDown(useKeyCode);
            RangedAttackPressed = Input.GetKeyDown(rangedAttackKeyCode);
            RangedAttackPressedDown = Input.GetKey(rangedAttackKeyCode);
            SprintButtonPressed = Input.GetKey(sprintKeyCode);

            if (Input.GetKeyDown(gamePauseKeyCode))
            {
                GlobalVariables.GamePaused = !GlobalVariables.GamePaused;
                ToggleMouseCapture(!GlobalVariables.GamePaused);
            }
            
            if (pauseMenu is not null)
                pauseMenu.SetActive(GlobalVariables.GamePaused);
        }

        public static void ToggleMouseCapture(bool value)
        {
            Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.None;
            Cursor.visible = !value;
        }
    }
}