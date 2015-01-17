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

	public static RaceState raceState;

	public delegate void RaceControllerUpdate();
	public static event RaceControllerUpdate OnUpdate;
	public static event RaceControllerUpdate Newlap;
	

	


	
	public bool canLap = true;
	public int 	lapsCount = 0;
	public int  currentLap = 0;

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

		raceState = RaceState.PRERACE;
		StartCollision.OnCrossing += new StartCollision.CrossingFinishline(UpdateLapCount);
		MidpointCollision.OnHalfway += new MidpointCollision.CrossingMidpoint(UpdateLapStatus);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(OnUpdate != null)
		{
			OnUpdate();
		}
		SwitchState();
	}

	void UpdateLapCount()
	{
		if(currentLap < lapsCount && canLap)
		{
			currentLap++;
			canLap = false;
			
			if(Newlap != null)
				Newlap();
		}
		else
		{
			//Todo: End race here.
		}
	}

	void UpdateLapStatus()
	{
		canLap = true;
	}

	void SwitchState()
	{

	}
}
