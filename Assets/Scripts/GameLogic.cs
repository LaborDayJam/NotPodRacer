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
	public float 	championshipTime = 0f;
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
		GetTopTimes();
		
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
	private void GetTopTimes()
	{
			topTrackTimes = new float[3];
			trackBestLaps = new float[3]; 
			topTrackTimes[0] = PlayerPrefs.GetFloat("IslandsTrack");
			topTrackTimes[1] = PlayerPrefs.GetFloat("IcelandTrack");
			topTrackTimes[2] = PlayerPrefs.GetFloat("AmazonTrack");
			trackBestLaps[0] = PlayerPrefs.GetFloat("IslandsLap");
			trackBestLaps[1] = PlayerPrefs.GetFloat("IcelandLap");
			trackBestLaps[2] = PlayerPrefs.GetFloat("AmazonLap");
			championshipTime = PlayerPrefs.GetFloat("ChampionshipTotal");
	
	}	
	public void SaveTime(string trackName, float time)
	{
		PlayerPrefs.SetFloat(trackName, time);
	}
	#endregion
}
