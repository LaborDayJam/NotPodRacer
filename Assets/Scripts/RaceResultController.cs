using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceResultController : MonoBehaviour 
{
	public GameObject[] trackpics;
	public GameObject[] pbIcons;
	public GameObject[] lapTimeObjects;
	public GameObject[] buttons;
	public GameObject   singlePanel;
	public GameObject   champPanel;
	
	public Text[] 		lapTimes;
	public Text			trackName;
	public Text			lapNumber;
	public Text			trackTotalTime;
	

	private TimeManager timeManager;
	private GameLogic 	gameLogic;
	
	private int 		bestLap =-1;
	public void Awake()
	{
		timeManager = GetComponent<TimeManager>();
		gameLogic = GameLogic.instance;
		if(gameLogic.singleRace)
		{
			SetLapTimes();
			SetRaceInformation();
			buttons[1].SetActive(false);
		}
		else
		{
			SetLapTimes();
			SetRaceInformation();
			buttons[0].SetActive(false);
		}
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

				if(gameLogic.lapTimes[i] > gameLogic.trackBestLaps[gameLogic.trackNum])
				{
				 	gameLogic.trackBestLaps[gameLogic.trackNum] = gameLogic.lapTimes[i]; 
				 	bestLap = i;
				}
				
			}
			else
				lapTimes[i].gameObject.SetActive(false);
		}
		
		if(bestLap >= 0)
			pbIcons[bestLap].SetActive(true);
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
	
	void SetTrackTime()
	{
		if(gameLogic.totalTracTime > gameLogic.topTrackTimes[gameLogic.trackNum])
		{
			gameLogic.topTrackTimes[gameLogic.trackNum] = gameLogic.totalTracTime;
			pbIcons[3].SetActive(true);
		}
		
		if(!gameLogic.singleRace)
			gameLogic.championshipTotals[gameLogic.trackNum] = gameLogic.totalTracTime;
			
		trackTotalTime.text = timeManager.convertTimeToFormat(gameLogic.totalTracTime);
	}
	public void OnReset()
	{
		for(int i = 0; i < 3; i++)
			gameLogic.lapTimes[i] = 0;
		
		gameLogic.totalTracTime = 0;	
		Application.LoadLevel(gameLogic.trackNum + 1);
		
	}
	
	public void OnDone()
	{
		for(int i = 0; i < 3; i++)
			gameLogic.lapTimes[i] = 0;
		
		gameLogic.totalTracTime = 0;
		
		Application.LoadLevel(0);
	}
	
	public void OnNext()
	{
		if(gameLogic.trackNum <= 2)
		{
			gameLogic.trackNum++;
		}
		else
		{
			buttons[1].SetActive(false);
			buttons[0].SetActive(true);
			singlePanel.SetActive(false);
			champPanel.SetActive(true);
			
		}
			
		for(int i = 0; i < 3; i++)
			gameLogic.lapTimes[i] = 0;
		
		
		gameLogic.totalTracTime = 0;
		Application.LoadLevel(6);

	}
}

// #Todo: save best lap for each track during champ mode. 
// #Todo: load best lap for each track during champ mode in camp panel
