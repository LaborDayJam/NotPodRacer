using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

	public static GameLogic	instance;
	
	
	public float[] lapTimes;
	public float[] championshipTotals;
	
	public float   singleRacetotalTime;
	
	public bool gameStarted;
	public bool singleRace;
	
	public int lapCount = 0;
	public int trackNum = 0;
	
	#region SaveLogic Variables
	public float[] 	topTrackTimes;
	public float[]  trackBestLaps;
	public float 	championshipBestTime = 0f;
	public float 	championshipTotalTime = 0f;
	public float 	totalTracTime;
	#endregion
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
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
	}
	
	
	public float ReturnRaceTotalTime()
	{
		for(int i = 0; i < lapCount; i++)
		{
			singleRacetotalTime += lapTimes[i];
		}
		
		return singleRacetotalTime;
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
		topTrackTimes = new float[3];
		trackBestLaps = new float[3];
		 
		topTrackTimes = PlayerPrefsX.GetFloatArray("TopTrackTimes");
		trackBestLaps = PlayerPrefsX.GetFloatArray("TracksBestLaps");
		championshipBestTime = PlayerPrefs.GetFloat("ChampionshipTime");
	
	}	
	
	public void SaveTime(string trackName, float time)
	{
		PlayerPrefs.SetFloat(trackName, time);
	}
	
	private void SetSaveInfo()
	{
		topTrackTimes = new float[3];
		trackBestLaps = new float[3];
	 	
	 	float champTime = 0;
	 	
	 	for(int i = 0; i < 3; i++)
	 	{
			topTrackTimes[i] = 0f;
			trackBestLaps[i] = 0f;
	 	}
	 	
		PlayerPrefsX.SetFloatArray("TopTrackTimes", topTrackTimes);
		PlayerPrefsX.SetFloatArray("TracksBestLaps", trackBestLaps);
		PlayerPrefs.SetFloat("ChampionshipTime", champTime);
	}
	#endregion
}
