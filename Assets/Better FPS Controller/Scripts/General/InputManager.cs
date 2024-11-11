// Code by: Sidharth S (aka SidTheLoser)

using System;
using General;
using UnityEngine;

namespace Better_FPS_Controller.Scripts.General
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
        private KeyCode meleeAttackKeyCode = KeyCode.E;

        [SerializeField] private KeyCode rangedAttackKeyCode = KeyCode.Mouse0;

        [Header("Misc")] [SerializeField] private KeyCode gamePauseKeyCode = KeyCode.Escape;

        [HideInInspector] public static Vector3 MovementVector;
        [HideInInspector] public static bool JumpButtonPressed;
        [HideInInspector] public static bool MeleeAttackPressed;
        [HideInInspector] public static bool RangedAttackPressed;
        [HideInInspector] public static bool SprintButtonPressed;

        private void Awake()
        {
            if (gamePauseKeyCode == KeyCode.Escape && Application.isEditor) gamePauseKeyCode = KeyCode.P;
        }

        private void Update()
        {
            MovementVector = Vector2.zero;
            if (Input.GetKey(forwardKeyCode)) MovementVector.z += 1;
            if (Input.GetKey(backwardKeyCode)) MovementVector.z -= 1;
            if (Input.GetKey(rightwardKeyCode)) MovementVector.x += 1;
            if (Input.GetKey(leftwardKeyCode)) MovementVector.x -= 1;

            JumpButtonPressed = Input.GetKeyDown(jumpKeyCode);
            MeleeAttackPressed = Input.GetKeyDown(meleeAttackKeyCode);
            RangedAttackPressed = Input.GetKeyDown(rangedAttackKeyCode);
            SprintButtonPressed = Input.GetKey(sprintKeyCode);

            if (Input.GetKeyDown(gamePauseKeyCode))
            {
                GlobalVariables.GamePaused = !GlobalVariables.GamePaused;
                ToggleMouseCapture(!GlobalVariables.GamePaused);
            }
        }

        public static void ToggleMouseCapture(bool value)
        {
            Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.None;
            Cursor.visible = !value;
        }
    }
}