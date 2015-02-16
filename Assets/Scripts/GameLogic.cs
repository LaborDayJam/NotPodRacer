using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

	public static GameLogic	instance;
	
	
	public float[] lapTimes;
	public float[] championshipTrackTotals;
	public float[] championshipBestLaps;  

	
	public bool gameStarted;
	public bool singleRace;
	
	public int lapCount = 0;
	public int trackNum = 0;
	
	#region SaveLogic Variables
	public float[] 	trackBestTimes;
	public float[]  trackBestLaps;
	public float 	championshipBestTime = 0f;
	public float 	championshipTotalTime = 0f;
	public float 	totalTrackTime;
	#endregion
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (instance == null)
			instance = this;
		else 
		{
			Debug.LogWarning("Duplicate GameLogic. Deleting this game object");	
			Destroy (this.gameObject);
		}
		
		if(!CheckFirstRun())
		{
			GetTopTimes();
		}
		else 
		{
			SetSaveInfo();
		}
		lapTimes = new float[3];
		championshipBestLaps = new float[3];
		championshipTrackTotals = new float[3];
	}
	
	void OnDestroy()
	{
		SaveTime();
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			PlayerPrefs.DeleteAll();
		}
		
	}
	#region SaveLogic
	private bool CheckFirstRun()
	{
		
		if(PlayerPrefs.GetInt("FirstRun") > 0)
		{
			return false;
		}
		else
		{
			PlayerPrefs.SetInt("FirstRun", 5);
			return true;
		}
	}
	
	private void GetTopTimes()
	{
		trackBestTimes = new float[3];
		trackBestLaps = new float[3];
		 
		trackBestTimes = PlayerPrefsX.GetFloatArray("TopTrackTimes");
		trackBestLaps = PlayerPrefsX.GetFloatArray("TracksBestLaps");
		championshipBestTime = PlayerPrefs.GetFloat("ChampionshipTime");
	
	}	
	
	public void SaveTime()
	{
		if(championshipBestTime > PlayerPrefs.GetFloat("ChampionshipTime"))
		{
			PlayerPrefs.SetFloat("ChampionshipTime",championshipBestTime);
		}
		
		PlayerPrefsX.SetFloatArray("TrackBestLaps", trackBestLaps);
		PlayerPrefsX.SetFloatArray("TopTrackTimes", trackBestTimes);
	}
	
	private void SetSaveInfo()
	{
		trackBestTimes = new float[3];
		trackBestLaps = new float[3];
	 	
	 	float champTime = 0;
	 	
	 	for(int i = 0; i < 3; i++)
	 	{
			trackBestTimes[i] = 0f;
			trackBestLaps[i] = 0f;
	 	}
	 	
		PlayerPrefsX.SetFloatArray("TopTrackTimes", trackBestTimes);
		PlayerPrefsX.SetFloatArray("TracksBestLaps", trackBestLaps);
		PlayerPrefs.SetFloat("ChampionshipTime", champTime);
	}
	#endregion
}
