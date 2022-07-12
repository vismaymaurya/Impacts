using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool walk;
		public bool pickup;
		public bool alpha01;
		public bool alpha02;
		public float scroll;
		public bool drop;
		public bool fire1;
		public bool fire2;
		public bool mouse1;
		public bool consume;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
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

		public void OnWalk(InputValue value)
		{
			WalkInput(value.isPressed);
		}

		public void OnPickup(InputValue value)
		{
			PickupInput(value.isPressed);
		}

		public void OnAlpha01(InputValue value)
		{
			Alpha01Input(value.isPressed);
		}

		public void OnAlpha02(InputValue value)
		{
			Alpha02Input(value.isPressed);
		}

		public void OnScroll(InputValue value)
		{
			ScrollInput(value.Get<float>());
		}

		public void OnDrop(InputValue value)
		{
			DropInput(value.isPressed);
		}

		public void OnFire1(InputValue value)
		{
			Fire1Input(value.isPressed);
		}

		public void OnFire2(InputValue value)
		{
			Fire2Input(value.isPressed);
		}

		public void OnMouse1(InputValue value)
		{
			Mouse1Input(value.isPressed);
		}

		public void OnConsume(InputValue value)
		{
			ConsumeInput(value.isPressed);
		}

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void WalkInput(bool newWalkState)
		{
			walk = newWalkState;
		}

		private void PickupInput(bool newPickupState)
		{
			pickup = newPickupState;
		}

		private void Alpha01Input(bool newAlpha01State)
		{
			alpha01 = newAlpha01State;
		}

		private void Alpha02Input(bool newAlpha02State)
		{
			alpha02 = newAlpha02State;
		}

		private void ScrollInput(float newScroll2State)
		{
			scroll = newScroll2State;
		}

		private void DropInput(bool newDropState)
		{
			drop = newDropState;
		}

		private void Fire1Input(bool newFire1State)
		{
			fire1 = newFire1State;
		}

		private void Fire2Input(bool newFire2State)
		{
			fire2 = newFire2State;
		}

		private void Mouse1Input(bool newMouse1State)
		{
			mouse1 = newMouse1State;
		}

		private void ConsumeInput(bool newConsumeState)
		{
			consume = newConsumeState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}