using UnityEngine;
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

	public RaceState raceState;

	public delegate void RaceControllerUpdate();
	public static event RaceControllerUpdate OnUpdate;

	


	
	public bool canLap = true;
	public int 	lapsCount = 0;
	public int  currentLap = 0;

	public int GetState
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
		raceState = RaceState.PRERACE; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(OnUpdate != null)
		{
			OnUpdate();
		}
	}
}
