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
	public GameObject [] trackPics;
	public GameObject	 trackName;
	public GameObject    numLaps;
	
	private GameLogic	 gameLogic;
	private Text 		 trackNameText;
	private Text 		 numOfLapsText;
	private int 		 menuNum = 0;
	private int 		 numOfLaps = 0;
	private int 		 trackNameNum = 0;
	#endregion
	
	// Use this for initialization
	void Awake () 
	{
	   gameLogic = GameLogic.instance;
	   trackNameText = trackName.GetComponent<Text>();
	   numOfLapsText = numLaps.GetComponent<Text>();
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
	#region MainMenu
	public void SingleRaceButton()
	{
		MainPanel.SetActive(false);
		title.SetActive(false);
		SinglePanel.SetActive(true);
		menuNum++;
	}
	public void ChampionshipButton()
	{
		MainPanel.SetActive(false);
		ChampionPanel.SetActive(true);
	}
	public void ExitButton()
	{
		MainPanel.SetActive(false);
		ExitPanel.SetActive(true);
	}
	#endregion
	
	#region SingleRaceMode
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
	public void StartRace()
	{
		gameLogic.singleRace = true;
		gameLogic.lapCount = numOfLaps;
		gameLogic.gameStarted = true;
		gameLogic.trackNum = trackNameNum;
		
		Application.LoadLevel(trackNameNum+1);
	}
	
	public void BackButton()
	{
		SinglePanel.SetActive(false);
		title.SetActive(true);
		MainPanel.SetActive(true);
		menuNum--;
		trackNameNum = 0;
		numOfLaps = 0;
		
	}
	#endregion
	
	
	
	
}
