using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
	public Text trackNameText;
	
	private int trackNameNum = 0;
	// Use this for initialization
	void Start () 
	{
		trackNameNum = GameLogic.instance.trackNum;
		
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
		
		StartCoroutine("WaitToLoad");
	}
	
	
	IEnumerator WaitToLoad()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel(GameLogic.instance.trackNum + 1);

	}
	// Update is called once per frame

}
