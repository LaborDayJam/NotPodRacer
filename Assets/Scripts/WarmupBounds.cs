using UnityEngine;
using System.Collections;

public class WarmupBounds : MonoBehaviour
 {
	
	private int maxX = 4000;
	private int minX = -4000;
	private int maxZ = 5000;
	private int minZ = -5000;
	// Use this for initialization
	void Awake () 
	{
		RaceController.OnUpdate += new RaceController.RaceControllerUpdate(WarmupUpdate);
	}
	
	void OnDestroy()
	{
		RaceController.OnUpdate -= new RaceController.RaceControllerUpdate(WarmupUpdate);
	}
	
	void WarmupUpdate()
	{
		if(transform.position.x > maxX)
			transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
		else if(transform.position.x < minX)
			transform.position = new Vector3(minX, transform.position.y, transform.position.z);
			
		if(transform.position.z > maxZ)
			transform.position = new Vector3(maxZ, transform.position.y, transform.position.z);
		else if(transform.position.z < minZ)
			transform.position = new Vector3(minZ, transform.position.y, transform.position.z);	
	}
	
}
