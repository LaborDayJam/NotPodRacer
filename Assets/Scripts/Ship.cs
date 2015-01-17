#define KEYBOARD
using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	public float max_accel_rate = 10;
	public float max_speed = 12;
	public float max_output = 2;
	
	public float speed = 0;
	public float accelerationRate = 2;
	public float outputRate = 2;

	public Vector3 direction;
	public float directionXClamp = .4f;

	public float leftOutput = 0;
	public float rightOutput = 0;

	public float drag = 1;

	int isThrusting;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
	}
	
	void HandleInput()
	{
		isThrusting = 0;

#if KEYBOARD
		if (Input.GetKey (KeyCode.LeftArrow)) {
			leftOutput = Mathf.Clamp( leftOutput + (outputRate * Time.deltaTime), 0, max_output);
			Debug.Log("LEFT");
			isThrusting = 1;
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			rightOutput = Mathf.Clamp( rightOutput + (outputRate  * Time.deltaTime), 0 ,max_output);	
			Debug.Log("RIGHT");
			isThrusting = 1;
		}

		//simulate not thrusting
		leftOutput = Mathf.Clamp( leftOutput - drag * Time.deltaTime, 0, max_output);
		rightOutput = Mathf.Clamp( rightOutput - drag  * Time.deltaTime, 0 ,max_output);	
#endif
		
		direction = new Vector3 (Mathf.Clamp(rightOutput - leftOutput, -directionXClamp, directionXClamp) , 0, isThrusting);
		
		if (isThrusting == 1)
			speed += accelerationRate * Time.deltaTime;
		else
			speed -= drag * Time.deltaTime;

		speed = Mathf.Clamp (speed, 0, max_speed);

		transform.position += direction * speed;
		Debug.Log (direction.ToString () + "  " + speed.ToString ());
	}
}