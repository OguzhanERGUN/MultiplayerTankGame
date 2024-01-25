using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
	public event Action<bool> primaryFireEvent;
	private Controls controls;

	private void OnEnable()
	{
		if (controls != null)
		{
			controls = new Controls();
			controls.Player.SetCallbacks(this);
		}

		controls.Player.Enable();
	}
	public void OnMove(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}

	public void OnPrimaryFire(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			primaryFireEvent?.Invoke(true);
		}
		else if (context.canceled)
		{
			primaryFireEvent?.Invoke(false);
		}
	}
}
