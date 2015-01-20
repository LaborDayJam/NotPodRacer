using UnityEngine;
using System.Collections;

public class MidpointCollision : MonoBehaviour {

	public delegate void CrossingMidpoint();
	public static CrossingMidpoint OnHalfway;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			if(OnHalfway != null)
				OnHalfway();
		}
	}
}
