using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceController : MonoBehaviour 
{

	public enum RaceState
	{
		PRERACE,
		COUNTDOWN,
		RACING,
		PAUSED,
		FINISHED
	};

	public static RaceState raceState;


	public delegate void RaceControllerUpdate();
	public static event RaceControllerUpdate OnUpdate;
	public static event RaceControllerUpdate Newlap;
	
	public GameObject [] 	lights;
	public GameObject    	lightHolder;
	public GameObject 		rsInputObject;
	public Text 			currentLapTimeText;
	public Text 			bestLapTimeText;
	
	public bool 			canLap = true;
	public int 				lapsCount = 0;
	public int  			currentLap = 0;
	
	private InputManager 	inputManager;
	private TimeManager 	timeManager;
	private GameLogic 		gameLogic;
	private RaceState 		lastState;
	private bool 			raceStart = false;
	private int 			lightIndex = 0;

	
	public static int GetState
	{
		get{return (int)raceState;}
		set{raceState = (RaceState)value;}
	}

	public bool GetLapStatus
	{
		get{return canLap;}
		set{canLap = value;}
	}

	// Use this for initialization
	void Awake () 
	{
		StartCollision.OnCrossing 	+= new StartCollision.CrossingFinishline(UpdateLapCount);
		MidpointCollision.OnHalfway += new MidpointCollision.CrossingMidpoint(UpdateLapStatus);
		raceState = RaceState.PRERACE;
		lastState = raceState;
		
	}
	
	void OnDestroy()
	{
		StartCollision.OnCrossing 	-= new StartCollision.CrossingFinishline(UpdateLapCount);
		MidpointCollision.OnHalfway -= new MidpointCollision.CrossingMidpoint(UpdateLapStatus);
	}
	
	void Start()
	{
		inputManager = InputManager.Instance;
		gameLogic = GameLogic.instance;
		lapsCount = gameLogic.lapCount;
		timeManager = GetComponent<TimeManager> ();		
	}
	// Update is called once per frame
	void Update () 
	{
		if(OnUpdate != null)
		{
			OnUpdate();
		}
		
		if(raceState == RaceState.PRERACE && (inputManager.leftOutputNormalized != 0 || inputManager.rightOutputNormalized != 0))
			raceState = RaceState.COUNTDOWN;
		else if(raceState == RaceState.RACING)
			TextHandler();
			
		if(lastState != raceState)
		{
			SwitchState();
			lastState = raceState;
		}
	}

	void UpdateLapCount()
	{
		currentLap++;
		
		if(currentLap == 1)
		{
			
			gameLogic.lapTimes[currentLap - 1] = timeManager.currentLap;
			CheckBestTime();
		}
		else if(currentLap == 2)
		{
			gameLogic.lapTimes[currentLap - 1] = timeManager.currentLap;
			CheckBestTime();
		}
		else if(currentLap == 3)
		{
			gameLogic.lapTimes[currentLap - 1] = timeManager.currentLap;
			CheckBestTime();
		}	
		
		if(currentLap < lapsCount && canLap)
		{
			canLap = false;
											
			if(Newlap != null)
				Newlap();
		}
		else
		{
			raceState = RaceState.FINISHED;
			StartCoroutine("EndRace");
			
		}
	}
	
	void CheckBestTime()
	{
		if(gameLogic.trackBestLaps[gameLogic.trackNum] < gameLogic.lapTimes[currentLap - 1])
			gameLogic.trackBestLaps[gameLogic.trackNum] = gameLogic.lapTimes[currentLap - 1];	
	}
	
	void UpdateLapStatus()
	{
		canLap = true;
	}

	void SwitchState()
	{
		switch(raceState)
		{
			case RaceState.COUNTDOWN:
			{
				StartCoroutine("StartRace");
				break;
			}
	
		}
	}
	
	void TextHandler()
	{
		currentLapTimeText.text = timeManager.convertTimeToFormat(timeManager.currentLap);
		bestLapTimeText.text = timeManager.convertTimeToFormat(gameLogic.trackBestLaps[gameLogic.trackNum]);
	}
	
	IEnumerator EndRace()
	{
		
		gameLogic.totalTrackTime = timeManager.totalRaceTime;
		timeManager.StopTime();
		gameLogic.gameStarted = false;
		yield return new WaitForSeconds(3);
		Application.LoadLevel(5);
		
	}
	
	IEnumerator StartRace()
	{
		if(!raceStart)
		{
			AudioManager.instance.PlayOneShot(0, lightHolder.transform.position);
			raceStart = true;
		}
		
		while(lightIndex < lights.Length)
		{
			NextLight();
			yield return new WaitForSeconds(.95f);
		}
		
		raceState = RaceState.RACING;
		lightHolder.SetActive(false);
		timeManager.StartTime ();
	}

	void NextLight()
	{
		lights[lightIndex].SetActive(true);
		lightIndex++;			 	
	}
}
