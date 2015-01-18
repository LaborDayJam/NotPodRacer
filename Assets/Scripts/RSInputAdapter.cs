using UnityEngine;
using System.Collections;

public class RSInputAdapter : MonoBehaviour {
#if !UNITY_EDITOR_OSX
	public ThrusterTracker leftHand;
	public ThrusterTracker rightHand;
#endif

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
#if !UNITY_EDITOR_OSX
			if(leftHand.isTracking)
				leftOutputNormalized = (leftHand.maxDepth - leftHand.handDepth)/(leftHand.maxDepth - leftHand.minDepth);
			else
				leftOutputNormalized = 0;

			if(rightHand.isTracking)
				rightOutputNormalized = (rightHand.maxDepth - rightHand.handDepth)/(rightHand.maxDepth - rightHand.minDepth);
			else
				rightOutputNormalized = 0;
#endif
			yield return 0;
		}

	}
}
