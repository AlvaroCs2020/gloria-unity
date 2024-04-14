using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunSystem;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 5f;
	public float rotationSpeed = 10f;
	public float jumpForce = 8f;
	public float gravity = 100f;


	public Transform trCamera;
	//public Camera camera;
	private float _cameraVerticalRotation = 0f;
	private bool _IsGunSystemActive = false;

	private float _health = 1000f;
	private CharacterController _characterController;
	private Vector3 _moveDirection = Vector3.zero;
	public static PlayerController Instance { get; private set; }
	public event Action<Vector3, Vector3> ShootEvent; //subject

	void Awake()
	{
		Instance = this;
		_characterController = GetComponent<CharacterController>();
		//_camera = GetComponentInChildren<CameraController>();

	}
	public void moveA()
	{
		_moveDirection = -transform.right;
	}
	public void moveD()
	{
		_moveDirection = transform.right;
	}
	public void moveW()
	{
		_moveDirection = transform.forward;
	}
	public void moveS()
	{
		_moveDirection = -transform.forward;
	}
	public void moveSpace()
	{
		_moveDirection = transform.up*jumpForce/movementSpeed; //mathy
	}
	public Transform PlayerTransform()
	{
		return transform;
	}

	void OnDrawGizmos()
	{
		Vector3 startPoint = trCamera.position;
		Vector3 endPoint = trCamera.position +  trCamera.forward * 10;
		Gizmos.DrawLine(startPoint, endPoint);
	}
	public void Shoot() 
	{
		if (!_IsGunSystemActive) return;
		ShootEvent?.Invoke(trCamera.position, trCamera.forward);
		//GunHandler.Instance.Shoot(trCamera.position, trCamera.forward);
	}
	public void move(float inputX, float inputY)
	{
		inputX *= rotationSpeed;
		inputY *= rotationSpeed;
		_cameraVerticalRotation -= inputY;
		_cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
		trCamera.localEulerAngles = Vector3.right * _cameraVerticalRotation;
		transform.Rotate(Vector3.up * inputX);
		// Apply gravity to simulate realistic movement

		_moveDirection.y -= gravity * Time.deltaTime;
		_characterController.Move(_moveDirection * movementSpeed * Time.deltaTime);
		_moveDirection = Vector3.zero;
	}
	public void ActivateGunSystem(){ _IsGunSystemActive = true;}

	public float UpdateHealth(float deltaHealth)
	{
		return _health += deltaHealth;
	}
}