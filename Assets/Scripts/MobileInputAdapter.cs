using UnityEngine;
using System.Collections;

public class MobileInputAdapter : MonoBehaviour {

	float screenWidth;
	float screenHeight;

	public float leftOutput;
	public float rightOutput;

	public bool isTrackingLeftHand;
	public bool isTrackingRightHand;

	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height * .8f;
		isTrackingLeftHand = false;
		isTrackingRightHand = false;
	}

	void Update () 
	{
		DetectInput ();
	}

	void DetectInput()
	{
		Touch currentTouch;
		for (int i = 0; i < Input.touchCount; i++) {
			currentTouch = Input.GetTouch(i);

			if(currentTouch.phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled)
			{
				//if(currentTouch.position.x <= screenWidth)
					//isTrackingLeftHand = false;
				//else 
					//isTrackingRightHand = false;
			}
			else
			{
				if(currentTouch.position.x <= (screenWidth * .5f))
				{
					//isTrackingLeftHand = true;
					leftOutput = Mathf.Clamp(currentTouch.position.y, 0, screenHeight * .65f) / (screenHeight * .65f);
				}
				else 
				{
					//isTrackingRightHand = true;
					rightOutput = Mathf.Clamp(currentTouch.position.y, 0, screenHeight * .65f) / (screenHeight * .65f);

				}
				
				Debug.Log(" LeftOutput : " + leftOutput + "  RightOutput: " + rightOutput);
			}
		}
	}
}
