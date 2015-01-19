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

	public Text timerText;
	
	public delegate void RaceControllerUpdate();
	public static event RaceControllerUpdate OnUpdate;
	public static event RaceControllerUpdate Newlap;
	
	public GameObject [] lights;
	public GameObject    lightHolder;
	
	float time = 0;
	
	public GameObject rsInputObject;
	//TODO private RSInputAdapter rsInput;
	private InputManager inputManager;

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
		// TODO rsInput = rsInputObject.GetComponent<RSInputAdapter>();
		inputManager = InputManager.Instance;
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
		else if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		else if(Input.GetKeyDown(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);

		if(raceState == RaceState.PRERACE && inputManager.leftOutputNormalized != 0 && inputManager.rightOutputNormalized != 0)
			raceState = RaceState.COUNTDOWN;
	
		if(lastState != raceState)
		{
			SwitchState();
			lastState = raceState;
		}
	}

	void UpdateLapCount()
	{
		currentLap++;
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

			/// AUDIO CREDIT CURT VICKTOR BRYANT
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
	IEnumerator EndRace()
	{
		StopCoroutine ("CR_TrackTime");
		yield return new WaitForSeconds(5);
		Application.LoadLevel(0);
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
		StartCoroutine ("CR_TrackTime");
	}

	IEnumerator CR_TrackTime()
	{
		while (true) {
			time += Time.deltaTime;
			timerText.text = time.ToString();
			yield return 0;
				}
	}
	
	void NextLight()
	{
		lights[lightIndex].SetActive(true);
		lightIndex++;			 	
	}
}
