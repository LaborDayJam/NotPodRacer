using UnityEngine;
using System.Collections;

public class StartCollision : MonoBehaviour {

	public delegate void CrossingFinishline();
	public static CrossingFinishline OnCrossing;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Player")
		{
			if(OnCrossing != null)
				OnCrossing();
		}
	}
}
