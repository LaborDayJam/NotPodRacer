using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour 
{

	private float currentLap = 0f;
	private float totalRaceTime = 0f;
	
	
	public float saveTime = 0f;
		
	// Use this for initialization
	void Awake () 
	{
		RaceController.OnUpdate += new RaceController.RaceControllerUpdate(UpdateTimer);
		RaceController.Newlap += new RaceController.RaceControllerUpdate(ResetLapTime);	
	}
	
	void OnDestroy()
	{
		RaceController.OnUpdate -= new RaceController.RaceControllerUpdate(UpdateTimer);
		RaceController.Newlap -= new RaceController.RaceControllerUpdate(ResetLapTime);
	}
	
	void ResetLapTime()
	{
		currentLap = 0f;
	}
	// Update is called once per frame
	void UpdateTimer () 
	{
		if(RaceController.GetState == 2)
		{
			currentLap += Time.deltaTime;
			totalRaceTime += Time.deltaTime;
		}
		else if(RaceController.GetState == 4)
		{
			currentLap = 0;
			saveTime = totalRaceTime;
			totalRaceTime = 0f;
		}
	}
}
