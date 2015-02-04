using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceResultController : MonoBehaviour 
{
	
	public GameObject[] pbIcons;
	public Text[] 		lapTimes;
	public Text			trackName;
	public Text			lapNumber;

	private TimeManager timeManager;
	private GameLogic 	gameLogic;
	
	
	public void Awake()
	{
		timeManager = GetComponent<TimeManager>();
		gameLogic = GameLogic.instance;
		
		SetLapTimes();
		SetRaceInformation();
	}
	
	void SetLapTimes()
	{
		lapTimes = new Text[gameLogic.lapTimes.Length];
		
		for(int i = 0; i < gameLogic.lapTimes.Length; i++)
		{
			lapTimes[i].gameObject.SetActive(true);
			if(gameLogic.lapTimes[i] > 0)
			{
				
				lapTimes[i].text = timeManager.convertTimeToFormat(gameLogic.lapTimes[i]);
				gameLogic.lapTimes[i] = 0;
			}
			else
				lapTimes[i].gameObject.SetActive(false);
		}
	}
	
	void SetRaceInformation()
	{
		string name;
		if(gameLogic.trackNum == 0)
			name = "Islands";
		else if(gameLogic.trackNum == 1)
			name = "Iceland";
		else if(gameLogic.trackNum == 2)
			name = "Amazon";
		else if(gameLogic.trackNum == 3)
			name = "Warmup";
		else
			name = "";
		
		trackName.text = name;
		
		lapNumber.text = gameLogic.lapCount.ToString();
	}
	
	public void OnReset()
	{
		
	}
	
	public void OnDone()
	{
	
	}
}
