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
	public event Action<Vector2> moveEvent;
	private Controls controls;

	private void OnEnable()
	{
		//Challange should watch
		if (controls == null)
		{
			controls = new Controls();
			controls.Player.SetCallbacks(this);
		}

		controls?.Player.Enable();
	}
	public void OnMove(InputAction.CallbackContext context)
	{
		moveEvent?.Invoke(context.ReadValue<Vector2>());
		Debug.Log(context.ReadValue<Vector2>());
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
