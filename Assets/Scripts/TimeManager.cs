using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour 
{
	private GameLogic gameLogic;
	
	public float currentLap = 0f;
	public float totalRaceTime = 0f;
	private float bestLapTime = 0;

	public float currentLapTime;
	public float saveTime = 0f;
		
	// Use this for initialization
	void Awake () 
	{
		gameLogic = GameLogic.instance;
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

	public void StartTime()
	{
		StartCoroutine ("CR_TrackTime");
	}

	public void StopTime()
	{
		StopCoroutine ("CR_TrackTime");
	}

	IEnumerator CR_TrackTime()
	{
		bool noBestLapTime = (bestLapTime == 0) ?  true : false;
		while (true) 
		{
			currentLapTime += Time.deltaTime;
			yield return 0;
		}
	}
	
	public string convertTimeToFormat(float time)
	{
		string minutes = Mathf.Floor(time / 60).ToString("00");
		string seconds = (Mathf.Floor(time) % 60).ToString("00");
		string milliseconds = (Mathf.Floor((time*100) % 100)).ToString("00");
		return string.Format("{0:00}'{1:00}'{2:00}", minutes, seconds,milliseconds);
	}
	
}
