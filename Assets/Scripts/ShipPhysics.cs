using UnityEngine;
using System.Collections;

public class ShipPhysics : MonoBehaviour 
{

	PilotAnimation pilotAnimation;
	//Movement
	//public float max_accel_rate = 200;
	public float max_speed = 200;

	public float accelerationRate = 120;
	public float deccelerationRate = 80;

	//Thrust Output
	float MAX_INDIVIDUAL_THRUST = 1;
	float leftOutput = 0f;
	float rightOutput = 0f;

	float outputDrag = 1.5f;
	float outputRate = 1f;

	bool isThrusting;

	public Transform[] clampPoints;
	float clampIntervalSeconds = .5f;

	public float turnFactor = 300;

	Vector3 targetHeightPosition;

	InputManager inputManager;

	public Animation leftHand;
	public Animation rightHand;

	AnimationClip clip;
	
	// Use this for initialization
	void Start () 
	{
		targetHeightPosition = transform.position;
		StartCoroutine (CR_WaitForRaceStart ());

		inputManager = InputManager.Instance;
		pilotAnimation = GetComponentInChildren<PilotAnimation> ();
	}

	void HandleUpdate () 
	{
		isThrusting = false;

		switch (inputManager.inputType) 
		{
			case InputManager.INPUT_TYPE.KEYBOARD:
			{
				simulatedInput();
			}
			break;
			case InputManager.INPUT_TYPE.MOUSE:
			{
				simulatedInput();
			}
			break;
			case InputManager.INPUT_TYPE.MOBILE:
			{
				leftOutput = inputManager.leftOutputNormalized;
				rightOutput = inputManager.rightOutputNormalized;
				isThrusting = true;
			}
			break;
			case InputManager.INPUT_TYPE.REALSENSE:
			{
				if(inputManager.leftOutputNormalized > 0)
				{
					leftOutput = inputManager.leftOutputNormalized;
					isThrusting = true;
				}
				if(inputManager.rightOutputNormalized > 0)
				{
					rightOutput = inputManager.rightOutputNormalized;		
					isThrusting = true;
				}
			}
			break;
		}

		//Animation
		pilotAnimation.UpdateHandAnimation (0, leftOutput);
		pilotAnimation.UpdateHandAnimation (1, rightOutput);

		//Turning
		if( ( leftOutput - rightOutput ) != 0)
			rigidbody.AddRelativeTorque(0, ( leftOutput - rightOutput ) * turnFactor, 0);

		//Movement
		if (isThrusting) 
		{
			float acceleration = (leftOutput + rightOutput) * .5f * accelerationRate * Time.deltaTime;
			rigidbody.velocity += transform.forward * (acceleration );
		} 
	}

	void simulatedInput()
	{
		if(inputManager.leftOutputNormalized != 0) 
		{
			leftOutput = Mathf.Clamp(leftOutput + outputRate * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);
			isThrusting = true;
		}
		else
			leftOutput = Mathf.Clamp(leftOutput - outputDrag * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);
		
		if(inputManager.rightOutputNormalized != 0) 
		{
			rightOutput = Mathf.Clamp(rightOutput + outputRate * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);
			isThrusting = true;
		}
		else
			rightOutput = Mathf.Clamp(rightOutput - outputDrag * Time.deltaTime, 0, MAX_INDIVIDUAL_THRUST);
	}

	IEnumerator CR_UpdateLoop()
	{
		while (true) 
		{
			HandleUpdate();
			yield return 0;
		}
	}

	IEnumerator CR_WaitForRaceStart()
	{
		while (RaceController.GetState != 2) 
		{
			yield return 0;	
		}
		StartCoroutine (CR_UpdateLoop ());
		StartCoroutine (CR_GroundClamp ());
		StartCoroutine (CR_SlerpToTargetHeightPosition ());
	}

#region GROUND_CLMAP

	//TODO add masking to ground clamp check
	IEnumerator CR_GroundClamp()
	{
		RaycastHit hit;
		float x, y, z = 0;
		Vector3 averageHitPoint = Vector3.zero;
		while (true)
		{
			x = 0;
			y = 0;
			z = 0;
			int hitCount = 0;
			foreach (Transform tf in clampPoints) 
			{
				if (Physics.Raycast (transform.position, Vector3.down, out hit, 20)) 
				{
					averageHitPoint += hit.point;
					x += hit.normal.x;
					y += hit.normal.y;
					z += hit.normal.z;
					hitCount++;
				}
			}
			
			if(hitCount == 0)
				yield return new WaitForSeconds (clampIntervalSeconds);
				
			averageHitPoint /= hitCount;
			//Debug.Log("Avg hit point" + averageHitPoint + "!" + " x:" + x + " y:" + y + " z:" + z + " Hitcount:" + hitCount);
			x /= hitCount;
			y /= hitCount;
			z /= hitCount;
		
			float collisionHeight = averageHitPoint.y;
			//TODO orient myself to the average ground normal. Smooth transition
			//transform.up = new Vector3 (x, y, z);
			targetHeightPosition = new Vector3(transform.position.x, collisionHeight, transform.position.z) + transform.up * 5;
			Debug.Log(targetHeightPosition);
			yield return new WaitForSeconds (clampIntervalSeconds);
		}
	}

	IEnumerator CR_SlerpToTargetHeightPosition()
	{
		while (true) 
		{
			if( !targetHeightPosition.Equals(Vector3.zero))
			{
			   transform.position = Vector3.Slerp(transform.position, targetHeightPosition, Time.deltaTime);
				if(  Mathf.Abs(targetHeightPosition.y - transform.position.y) < .2f)
				{
					targetHeightPosition = Vector3.zero;
				}
			}
			yield return 0;
		}
	}
#endregion
}
	