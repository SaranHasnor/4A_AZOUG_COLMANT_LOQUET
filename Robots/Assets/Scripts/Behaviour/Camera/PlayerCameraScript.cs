using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{ // Manages the position of the camera and the player's camera-related input

	[SerializeField]
	private float _maxCameraZoomSpeed = 15.0f;

	[SerializeField]
	private float _maxCameraAngularSpeed = 90.0f;

	[SerializeField]
	private float _magicNumber = 0.9f;

	[SerializeField]
	private Vector3 _anchorPoint;

	private float _desiredCameraDistance = 10.0f;

	// The two following angles describe the angle created by the camera and the origin
	private float _desiredCameraHorzAngle = 45.0f; // TODO: Prevent this from being farther than half a loop away from us
	private float _desiredCameraVertAngle = 45.0f;

	void Update()
	{ // I feel like this has gotten over-complicated over time
		Vector3 cameraPos = this.transform.position - _anchorPoint;

		Quaternion desiredAngle = Quaternion.Euler(-_desiredCameraVertAngle, -_desiredCameraHorzAngle, 0.0f);
		Quaternion currentAngle = Quaternion.LookRotation(cameraPos);

		float diff = Quaternion.Angle(currentAngle, desiredAngle);
		float maxDelta = Mathf.Clamp(diff * _magicNumber, 3.0f, _maxCameraAngularSpeed) * Time.deltaTime;
		Quaternion targetAngle = Quaternion.RotateTowards(currentAngle, desiredAngle, maxDelta);

		//float currentCameraDistance = cameraPos.magnitude;
		Vector3 desiredPosition = _anchorPoint + targetAngle * Vector3.forward * _desiredCameraDistance;
		float diff2 = Vector3.Distance(cameraPos, desiredPosition);
		float maxDelta2 = Mathf.Clamp(diff2 * _magicNumber, 1.0f, _maxCameraZoomSpeed) * Time.deltaTime;
		float targetCameraDistance = Vector3.MoveTowards(cameraPos, desiredPosition, maxDelta2).magnitude;

		Vector3 newPosition = _anchorPoint + targetAngle * Vector3.forward * targetCameraDistance;

		this.transform.position = newPosition;
		this.transform.LookAt(_anchorPoint); // I was going to recode it but I guess this works

		UpdateInput();
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

		if (Input.GetKey(KeyCode.KeypadPlus))
		{
			_desiredCameraDistance -= 0.2f;
		}
		if (Input.GetKey(KeyCode.KeypadMinus))
		{
			_desiredCameraDistance += 0.2f;
		}

		_desiredCameraVertAngle = Mathf.Clamp(_desiredCameraVertAngle, -85.0f, 85.0f);
		_desiredCameraDistance = Mathf.Clamp(_desiredCameraDistance, 1.0f, 50.0f);
	}
}
