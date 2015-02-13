using UnityEngine;
using System.Collections;

public class MidpointCollision : MonoBehaviour 
{

	public delegate void CrossingMidpoint();
	public static CrossingMidpoint OnHalfway;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			if(OnHalfway != null)
				OnHalfway();
		}
	}
}
