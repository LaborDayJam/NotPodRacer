using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;
	public AudioClip [] clips;
	
	public GameObject soundObject;

	// Use this for initialization
	void Awake () 
	{
	
		if(instance == null)
			instance = this;
		else 
			Destroy (this.gameObject);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public  void PlayOneShot(int sfx, Vector3 position)
	{
		GameObject clone = Instantiate(soundObject, position, Quaternion.identity)as GameObject;
		clone.audio.PlayOneShot(clips[sfx]);
	}
	
	public  void Play(bool loop, int track)
	{
		
	}
}
