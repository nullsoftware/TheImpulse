using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 Move;
        public Vector2 Look;

        public bool Jump;
        public bool Sprint;
        public bool Aim;
        public bool Shoot;

        [Header("Movement Settings")]
        public bool AnalogMovement;

        [Header("Mouse Cursor Settings")]
        public bool CursorLocked = true;
        public bool CursorInputForLook = true;

        #region On Methods

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (CursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnAim(InputValue value)
        {
            AimInput(value.isPressed);
        }

        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        #endregion

        #region Input Methods

        public void MoveInput(Vector2 value)
        {
            Move = value;
        }

        public void LookInput(Vector2 value)
        {
            Look = value;
        }

        public void JumpInput(bool value)
        {
            Jump = value;
        }

        public void SprintInput(bool value)
        {
            Sprint = value;
        }

        public void AimInput(bool value)
        {
            Aim = value;
        }

        public void ShootInput(bool value)
        {
            Shoot = value;
        }

        #endregion

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(CursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}