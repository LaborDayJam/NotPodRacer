#define KEYBOARD
using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	//Movement
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

	//Clamp
	public Transform[] clampPoints;
	public float clampIntervalSeconds = .5f;
	bool isGroundClampEnabled = true;



	// Use this for initialization
	void Start () {
		if (isGroundClampEnabled)
			StartCoroutine (CR_GroundClamp ());

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

	//TODO add masking to ground clamp check
	IEnumerator CR_GroundClamp()
	{
		RaycastHit hit;
		float x, y, z = 0;
		while (true) {
			x = 0; y = 0; z = 0;
			foreach (Transform tf in clampPoints) {
				if(Physics.Raycast(transform.position, Vector3.down, out hit, 20))
				{
					Debug.Log("Hit " + hit.transform.name + "  " + hit.normal);
					x += hit.normal.x;
					y += hit.normal.y;
					z += hit.normal.z;
				}
			}
			x /= clampPoints.Length;
			y /= clampPoints.Length;
			z /= clampPoints.Length;

			//TODO orient myself to the average ground normal. Smooth transition

			yield return new WaitForSeconds(clampIntervalSeconds);
		}
	}
}