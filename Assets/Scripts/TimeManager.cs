using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour 
{

	public float currentLap = 0f;
	private float totalRaceTime = 0f;

	public Text text;
	
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
			string time = string.Format("{0}''{1}''{2}", "", getMinutes().ToString(), getSeconds().ToString
			                            ());
			if(text != null)
				text.text = time;
		}
	}

	int getSeconds()
	{
		return (int)totalRaceTime % 60;
	}

	int getMinutes()
	{
		return (int)totalRaceTime / 60;
	}
}
