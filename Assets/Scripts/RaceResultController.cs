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
		
	public Text [] 		champBestLaps;
	public Text [] 		champTrackTotals;
	public Text    		totalChampTime;
	public Text    		totalBestChampTime;	

	private TimeManager timeManager;
	private GameLogic 	gameLogic;
	
	private int 		bestLap =-1;
	
	public void Awake()
	{
		timeManager = GetComponent<TimeManager>();
		gameLogic = GameLogic.instance;
		
		if(gameLogic.singleRace)
			buttons[1].SetActive(false);
		else
			buttons[0].SetActive(false);
	
		SetLapTimes();
		SetRaceInformation();
		SetTrackTime();
	}
	
	void SetChampInfo()
	{
	
		for(int i = 0; i < 3; i++)
		{
			gameLogic.championshipTotalTime += gameLogic.championshipTrackTotals[i];
			
			
		}
		
		if(gameLogic.championshipTotalTime > gameLogic.championshipBestTime)
		{
			pbIcons[4].SetActive(true);
			gameLogic.championshipBestTime = gameLogic.championshipTotalTime;
		}
		
		totalChampTime.text = timeManager.convertTimeToFormat(gameLogic.championshipTotalTime);
		totalBestChampTime.text = timeManager.convertTimeToFormat(gameLogic.championshipBestTime);
	
		for (int i= 0; i < 3; i++)
		{
			champBestLaps[i].text = timeManager.convertTimeToFormat(gameLogic.championshipBestLaps[i]);
			champTrackTotals[i].text = timeManager.convertTimeToFormat(gameLogic.championshipTrackTotals[i]);
		}
	}
	
	void SetLapTimes()
	{
		for(int i = 0; i < gameLogic.lapTimes.Length; i++)
		{
			lapTimeObjects[i].gameObject.SetActive(true);
			
			if(gameLogic.lapTimes[i] > 0)
			{
				
				lapTimes[i].text = timeManager.convertTimeToFormat(gameLogic.lapTimes[i]);
	
				if(gameLogic.lapTimes[i] > gameLogic.trackBestLaps[gameLogic.trackNum])
				{
				 	gameLogic.trackBestLaps[gameLogic.trackNum] = gameLogic.lapTimes[i]; 
				 	bestLap = i;
				}
				
				if(gameLogic.lapTimes[i] > gameLogic.championshipBestLaps[gameLogic.trackNum])
				{
					gameLogic.championshipBestLaps[gameLogic.trackNum] = gameLogic.lapTimes[i];
				}
			 
			}
			else
				lapTimeObjects[i].gameObject.SetActive(false);
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
		if(gameLogic.totalTrackTime > gameLogic.trackBestTimes[gameLogic.trackNum])
		{
			gameLogic.trackBestTimes[gameLogic.trackNum] = gameLogic.totalTrackTime;
			pbIcons[3].SetActive(true);
		}
		
		if(!gameLogic.singleRace)
			gameLogic.championshipTrackTotals[gameLogic.trackNum] = gameLogic.totalTrackTime;
			
		trackTotalTime.text = timeManager.convertTimeToFormat(gameLogic.totalTrackTime);
	}
	public void OnReset()
	{
		for(int i = 0; i < 3; i++)
			gameLogic.lapTimes[i] = 0;
		
		gameLogic.totalTrackTime = 0;	
		Application.LoadLevel(gameLogic.trackNum + 1);
		
	}
	
	public void OnDone()
	{
		for(int i = 0; i < 3; i++)
			gameLogic.lapTimes[i] = 0;
		
		gameLogic.totalTrackTime = 0;
		
		Application.LoadLevel(0);
	}
	
	public void OnNext()
	{
		gameLogic.lapCount = 3;
		
		if(gameLogic.trackNum <= 2)
		{
			gameLogic.trackNum++;
			for(int i = 0; i < 3; i++)
				gameLogic.lapTimes[i] = 0;
			
			gameLogic.totalTrackTime = 0;
			Application.LoadLevel(6);
			
		}
		else
		{
			buttons[1].SetActive(false);
			buttons[0].SetActive(true);
			singlePanel.SetActive(false);
			champPanel.SetActive(true);
			SetChampInfo();
		}
			
		
	}
}
