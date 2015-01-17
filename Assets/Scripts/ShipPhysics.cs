using UnityEngine;
using System.Collections;

public class ShipPhysics : MonoBehaviour {

	//Movement
	public float max_accel_rate = 200;
	public float max_speed = 40;
	
	public float speed = 0;
	public float accelerationRate = 2;
	public float deccelerationRate = 3;

	//Thrust Output
	float MAX_INDIVIDUAL_THRUST = 1;
	float leftOutput;
	float rightOutput;

	float outputDrag = 1.5f;
	float outputRate = 1f;

	bool isThrusting;

	public Transform leftThruster;
	public Transform rightThruster;


	float openingSpeedBoost = 100;
	// Use this for initialization
	void Start () {
		StartCoroutine (CR_WaitForFirstThrust ());
	}
	
	// Update is called once per frame
	void HandleUpdate () {
		isThrusting = false;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 direction = (leftThruster.position - transform.position);
			direction = new Vector3(direction.z, 0, direction.x).normalized;
			rigidbody.AddForceAtPosition(leftThruster.forward * speed, leftThruster.position ,  ForceMode.Acceleration);
			//rigidbody.AddTorque( (leftThruster.position - transform.position).normalized * 5);
			leftOutput = Mathf.Clamp(leftOutput + outputRate * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);

			isThrusting = true;
		}
		else
			leftOutput = Mathf.Clamp(leftOutput - outputDrag * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 direction = (rightThruster.position - transform.position);
			direction = new Vector3(direction.x, 0, direction.z).normalized;
			rigidbody.AddForceAtPosition(rightThruster.forward * speed, rightThruster.position , ForceMode.Acceleration);
			//rigidbody.AddTorque( (rightThruster.position - transform.position).normalized * 5);
			rightOutput = Mathf.Clamp(rightOutput + outputRate * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);

			isThrusting = true;
		}
		else 
			rightOutput = Mathf.Clamp(rightOutput - outputDrag * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);

		//Movement
		if (isThrusting) {
			speed = Mathf.Clamp(speed + (leftOutput + rightOutput) * .5f * accelerationRate * Time.deltaTime, 0, max_speed);
	
		} else {
			speed = Mathf.Clamp(speed - deccelerationRate * Time.deltaTime, 0, max_speed);
		}

		rigidbody.velocity = transform.forward * (speed + openingSpeedBoost);
	}

	IEnumerator CR_UpdateLoop()
	{
		while (true) {
			HandleUpdate();
			yield return 0;
		}
	}

	IEnumerator CR_WaitForFirstThrust()
	{
		while (!isThrusting) {
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
				isThrusting = true;

			yield return 0;	
		}

		StartCoroutine (CR_UpdateLoop ());
		StartCoroutine (CR_OpeningSpeedBurst ());
	}

	IEnumerator CR_OpeningSpeedBurst()
	{
		openingSpeedBoost = 100;
		while (openingSpeedBoost > 0) {
			openingSpeedBoost -= accelerationRate * Time.deltaTime;
			yield return 0;
		}
	}
}
