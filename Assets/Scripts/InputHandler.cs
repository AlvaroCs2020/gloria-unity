using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{

	public static InputHandler Instance { get; private set; }
	void Awake()
	{
		Instance = this;
	}
	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float inputX = Input.GetAxis("Mouse X");
		float inputY = Input.GetAxis("Mouse Y");

		// Key board								
		if (Input.GetKey(KeyCode.A))
			PlayerController.Instance.moveA();
		if (Input.GetKey(KeyCode.D))
			PlayerController.Instance.moveD();
		if (Input.GetKey(KeyCode.W))
			PlayerController.Instance.moveW();
		if (Input.GetKey(KeyCode.S))
			PlayerController.Instance.moveS();
		if (Input.GetKeyDown(KeyCode.Space))
			PlayerController.Instance.moveSpace();
		// Mouse buttons
		if (Input.GetMouseButtonDown(0))
			PlayerController.Instance.Shoot();
		//else if (Input.GetMouseButton(0)) ;
		//else if (Input.GetMouseButtonUp(0)) ;
		float scroll = Input.mouseScrollDelta.y;
		if( scroll != 0)
			GameHandler.Instance.SwitchGun(scroll);
		

		PlayerController.Instance.move(inputX, inputY);

	}
}
