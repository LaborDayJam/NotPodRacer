//#define INPUT_REALSENSE

using UnityEngine;
using System.Collections;

//NOTE This class listens for the intial input source and then only listens to that initial source
public class InputManager : MonoBehaviour {
	public enum INPUT_TYPE { NONE, MOUSE, KEYBOARD, REALSENSE};

	private static InputManager instance;
	public static InputManager Instance { get { return instance; } }

	public INPUT_TYPE inputType;

	public float leftOutputNormalized;
	public float rightOutputNormalized;

#if INPUT_REALSENSE
	RSInputAdapter rsInputAdapter;
#endif
	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);
	}

	void Start()
	{
		inputType = INPUT_TYPE.NONE;
	}

	void ListenForKeyboardInput()
	{
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			leftOutputNormalized = 1;
			inputType = INPUT_TYPE.KEYBOARD;
		}
		else{
			leftOutputNormalized = 0;
		}
		
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			rightOutputNormalized = 1;
			inputType = INPUT_TYPE.KEYBOARD;
		}
		else {
			rightOutputNormalized = 0;	
		}
	}

	void ListenForMouseInput()
	{
		//MOUSE INPUT
		if (Input.GetMouseButton (0)) {
			leftOutputNormalized = 1;	
			inputType = INPUT_TYPE.MOUSE;
		}
		else
			leftOutputNormalized = 0;
		
		if (Input.GetMouseButton (1)) {
			rightOutputNormalized = 1;	
			inputType = INPUT_TYPE.MOUSE;
		}
		else
			rightOutputNormalized = 0;
	}

	void ListenForRealSenseInput()
	{	
		#if INPUT_REALSENSE
		if(rsInputAdapter.leftHand.isTracking){
			leftOutputNormalized = rsInputAdapter.leftOutputNormalized;
			inputType = INPUT_TYPE.REALSENSE;
		}
		if(rsInputAdapter.rightHand.isTracking){
			rightOutputNormalized = rsInputAdapter.rightOutputNormalized;
			inputType = INPUT_TYPE.REALSENSE;
		}
		#endif
	}


	void Update () {
		switch(inputType)
		{
			case INPUT_TYPE.NONE:
			{
				ListenForKeyboardInput();
				ListenForMouseInput();
				ListenForRealSenseInput();
			}break;
			case INPUT_TYPE.KEYBOARD:
			{
				ListenForKeyboardInput();
			}break;
			case INPUT_TYPE.MOUSE:
			{
				ListenForMouseInput();
			}break;
			case INPUT_TYPE.REALSENSE:
			{
				ListenForRealSenseInput();
			}break;
		}
	}
}
