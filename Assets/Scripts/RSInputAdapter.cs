using UnityEngine;
using System.Collections;

public class RSInputAdapter : MonoBehaviour {
	public ThrusterTracker leftHand;
	public ThrusterTracker rightHand;

	public float leftOutputNormalized;
	public float rightOutputNormalized;

	// Use this for initialization
	void Start () {
		StartCoroutine(CR_DetectInput());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator CR_DetectInput()
	{
		while(true)
		{
			if(leftHand.isTracking)
				leftOutputNormalized = (leftHand.maxDepth - leftHand.handDepth)/(leftHand.maxDepth - leftHand.minDepth);
			else
				leftOutputNormalized = 0;

			if(rightHand.isTracking)
				rightOutputNormalized = (rightHand.maxDepth - rightHand.handDepth)/(rightHand.maxDepth - rightHand.minDepth);
			else
				rightOutputNormalized = 0;

			yield return 0;
		}

	}
}
