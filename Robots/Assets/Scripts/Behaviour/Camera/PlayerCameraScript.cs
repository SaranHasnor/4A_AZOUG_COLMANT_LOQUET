using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{ // Manages the position of the camera and the player's camera-related input
	[SerializeField]
	private float _maxCameraAngularSpeed = 45.0f;

	[SerializeField]
	private float _magicNumber = 0.9f;

	private float _desiredCameraDistance = 10.0f;

	// The two following angles describe the angle created by the camera and the origin
	private float _desiredCameraHorzAngle = 45.0f;
	private float _desiredCameraVertAngle = 45.0f;

	void Update()
	{
		Quaternion desiredAngle = Quaternion.Euler(-_desiredCameraVertAngle, -_desiredCameraHorzAngle, 0.0f);
		Quaternion currentAngle = Quaternion.LookRotation(this.transform.position - Vector3.zero);

		float diff = Quaternion.Angle(currentAngle, desiredAngle);
		float maxDelta = Mathf.Clamp(diff * _magicNumber, 1.0f, _maxCameraAngularSpeed) * Time.deltaTime;
		Quaternion targetAngle = Quaternion.RotateTowards(currentAngle, desiredAngle, maxDelta);

		Vector3 desiredPosition = targetAngle * Vector3.forward * _desiredCameraDistance;

		this.transform.position = desiredPosition;
		this.transform.LookAt(Vector3.zero); // Temporary

		UpdateInput();

		//Debug.Log(_desiredCameraHorzAngle);
		//Debug.Log(_desiredCameraVertAngle);
	}

	private void UpdateInput()
	{ // Raw input for now
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_desiredCameraHorzAngle -= 2.5f;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			_desiredCameraHorzAngle += 2.5f;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			_desiredCameraVertAngle -= 2.5f;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_desiredCameraVertAngle += 2.5f;
		}
	}
}
