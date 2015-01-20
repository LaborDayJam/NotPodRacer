using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour 
{

	public float currentLap = 0f;
	private float totalRaceTime = 0f;
	private float bestLapTime = 0;

	public Text currentLapTimeText;
	public Text bestLapTimeText;

	public float currentLapTime;
	public float saveTime = 0f;
		
	// Use this for initialization
	void Awake () 
	{
		RaceController.OnUpdate += new RaceController.RaceControllerUpdate(UpdateTimer);
		RaceController.Newlap += new RaceController.RaceControllerUpdate(ResetLapTime);	
		bestLapTime = PlayerPrefs.GetFloat ("BestTime", 0);
		bestLapTimeText.text = convertTimeToFormat (bestLapTime);
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
		if (currentLapTime < bestLapTime || bestLapTime == 0) {
			PlayerPrefs.SetFloat ("BestTime", currentLapTime);
			PlayerPrefs.Save();		
			bestLapTimeText.text = currentLapTimeText.text;
		}
	}

	IEnumerator CR_TrackTime()
	{
		bool noBestLapTime = (bestLapTime == 0) ?  true : false;
		while (true) {
			currentLapTime += Time.deltaTime;
			currentLapTimeText.text = convertTimeToFormat(currentLapTime);

			if(noBestLapTime)
				bestLapTimeText.text = currentLapTimeText.text;
			yield return 0;
		}
	}
	
	string convertTimeToFormat(float time)
	{
		string minutes = Mathf.Floor(time / 60).ToString("00");
		string seconds = (Mathf.Floor(time) % 60).ToString("00");
		string milliseconds = (Mathf.Floor((time*100) % 100)).ToString("00");
		return string.Format("{0:00}'{1:00}'{2:00}", minutes, seconds,milliseconds);
	}
	
}
