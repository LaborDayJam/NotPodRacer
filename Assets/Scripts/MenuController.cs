using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	#region Panels
	public GameObject MainPanel;
	public GameObject SinglePanel;
	public GameObject ChampionPanel;
	public GameObject ExitPanel;
	public GameObject title;
	#endregion
	
	#region SinglePanel 
	public GameObject []trackPics;
	public GameObject	trackName;
	public GameObject   numLaps;
	public Text         bestTrackTimeText;
	public Text         bestLapTimeText;
	
	private GameLogic	gameLogic;
	private TimeManager timeManager;
	private Text 		trackNameText;
	private Text 		numOfLapsText;
	private int 		menuNum = 0;
	private int 		numOfLaps = 0;
	private int 		trackNameNum = 0;
	#endregion
	
	#region ChampionshipPanel
	public Text   [] bestTrackTotalTimesText;
	public Text   [] bestTrackLapTimesText;
	public Text      bestChampionshipTotalTimeText;
	private float [] bestTrackTotalTimes;
	private float [] bestTrackLapTimes;
	private float 	 bestChampionshipTotalTime;	
	#endregion
	// Use this for initialization
	void Awake () 
	{
	   gameLogic = GameLogic.instance;
	   timeManager = GetComponent<TimeManager>();
	   trackNameText = trackName.GetComponent<Text>();
	   numOfLapsText = numLaps.GetComponent<Text>();
	   GetStats();
	   
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	  	if(menuNum == 0)
	  	{
	  	
	  	}
	  	else if(menuNum == 1)
	  	{
			CheckNumLaps();
			CheckTrack();
			GetPic();
		}
		else if(menuNum == 2)
		{
			
		}
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			PlayerPrefs.DeleteAll();
	
		}
	}
	
	private void GetStats()
	{
		bestTrackTotalTimes = new float[3];
		bestTrackLapTimes = new float[3];
		for(int i = 0; i > 3; i++)
		{
			bestTrackTotalTimes[i] = gameLogic.trackBestTimes[i];
			bestTrackLapTimes[i] = gameLogic.lapTimes[i];
		}
		bestChampionshipTotalTime = gameLogic.championshipBestTime;
	}
	
	public void StartRace()
	{
		if(menuNum == 1)
		{
			gameLogic.singleRace = true;
			gameLogic.lapCount = numOfLaps;
			gameLogic.gameStarted = true;
			gameLogic.trackNum = trackNameNum;
			Application.LoadLevel(6);
		}
		else if(menuNum == 2)
		{
			gameLogic.singleRace = false;
			gameLogic.lapCount = 3;
			gameLogic.gameStarted = true;
			gameLogic.trackNum = trackNameNum;
			Application.LoadLevel(6);
		}
		
	}
	
	public void BackButton()
	{
		SinglePanel.SetActive(false);
		ChampionPanel.SetActive(false);
		title.SetActive(true);
		MainPanel.SetActive(true);
		menuNum = 0;
		trackNameNum = 0;
		numOfLaps = 0;
		
	}
	
	#region MainMenu
	public void SingleRaceButton()
	{
		MainPanel.SetActive(false);
		title.SetActive(false);
		SinglePanel.SetActive(true);
		GetTrackInfo();
		menuNum = 1;
	}
	public void ChampionshipButton()
	{
		MainPanel.SetActive(false);
		title.SetActive(false);
		ChampionPanel.SetActive(true);
		GetChampionsipInfo();
		menuNum = 2;
	}
	public void ExitButton()
	{
		MainPanel.SetActive(false);
		ExitPanel.SetActive(true);
	}
	#endregion
	
	#region SingleRaceMode
	private void GetTrackInfo()
	{
		bestTrackTimeText.text = timeManager.convertTimeToFormat(bestTrackTotalTimes[trackNameNum]);
		bestLapTimeText.text = timeManager.convertTimeToFormat(bestTrackLapTimes[trackNameNum]);
	}
	
	public void TrackR()
	{
		trackNameNum++;	
	}
	
	public void TRackL()
	{
		trackNameNum--;
	}
	
	public void NumLapsL()
	{
		if(trackNameNum != 3)
			numOfLaps--;
		else 
			numOfLaps = 0;
	}
	
	public void NumLapsR()
	{	
		if(trackNameNum != 3)
			numOfLaps++;
		else
			numOfLaps = 0;
	}
	
	
	private void CheckNumLaps()
	{
		if(numOfLaps > 3)
			numOfLaps = 3;
		else if(numOfLaps < 1 && trackNameNum != 3)
			numOfLaps = 1;
		else if (trackNameNum == 3)
			numOfLaps = 0;
		
		numOfLapsText.text = numOfLaps.ToString();
	}
	
	private void CheckTrack()
	{
		if(trackNameNum > trackPics.Length -1)
			trackNameNum = 0;
		else if(trackNameNum < 0)
			trackNameNum = trackPics.Length-1;
		
		string name;
		if(trackNameNum == 0)
			name = "Islands";
		else if(trackNameNum == 1)
			name = "Iceland";
		else if(trackNameNum == 2)
			name = "Amazon";
		else if(trackNameNum == 3)
			name = "Warmup";
		else
			name = "";
		
		trackNameText.text = name;
	}
	
	private void GetPic()
	{
		for(int i = 0; i < trackPics.Length; i++)
		{
			trackPics[i].SetActive(false);
		}
		trackPics[trackNameNum].SetActive(true);
	}
	#endregion
	
	#region ChampionshipMode
	private void GetChampionsipInfo()
	{
		for (int i = 0; i < 3; i++)
		{
			bestTrackTotalTimesText[i].text = timeManager.convertTimeToFormat(bestTrackTotalTimes[i]);
			bestTrackLapTimesText[i].text = timeManager.convertTimeToFormat(bestTrackLapTimes[i]);
		}
		bestChampionshipTotalTimeText.text = timeManager.convertTimeToFormat(bestChampionshipTotalTime);
		
	}
	#endregion
	
	
}
