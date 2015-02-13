using UnityEngine;
using System.Collections;

public class StartCollision : MonoBehaviour 
{

	public delegate void CrossingFinishline();
	public static CrossingFinishline OnCrossing;

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			if(OnCrossing != null)
				OnCrossing();
		}
	}
}
