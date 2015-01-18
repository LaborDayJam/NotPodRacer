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
	
	public GameObject [] lights;
	public GameObject    lightHolder;
	
	
	
	
	public bool canLap = true;
	public int 	lapsCount = 0;
	public int  currentLap = 0;
	
	private RaceState lastState;
	private bool raceStart = false;
	private int lightIndex = 0;
	
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
		lastState = raceState;
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
		
		if(Input.GetKeyDown(KeyCode.D))
			raceState = RaceState.COUNTDOWN;
		
		if(lastState != raceState)
		{
			SwitchState();
			lastState = raceState;
		}
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
		switch(raceState)
		{
			case RaceState.COUNTDOWN:
			{
		
				StartCoroutine("StartRace");
				break;
			}
			
		}
	}
	
	IEnumerator StartRace()
	{
		if(!raceStart)
		{
			AudioManager.instance.PlayOneShot(1, lightHolder.transform.position);
			raceStart = true;
		
		}
		
		while(lightIndex < lights.Length)
		{
			NextLight();
			yield return new WaitForSeconds(.95f);
			
		}
		raceState = RaceState.RACING;
		lightHolder.SetActive(false);
	}
	
	void NextLight()
	{
		lights[lightIndex].SetActive(true);
		lightIndex++;			 	
	}
}
