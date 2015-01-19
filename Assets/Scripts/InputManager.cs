//#define INPUT_REALSENSE

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public enum INPUT_TYPE { MOUSE, KEYBOARD, REALSENSE};

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
	
	// Update is called once per frame
	void Update () {

		//KEYBOARD INPUT 
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			leftOutputNormalized = 1;
			inputType = INPUT_TYPE.KEYBOARD;
		}
		else{
			leftOutputNormalized = 0;
			inputType = INPUT_TYPE.KEYBOARD;
		}

		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			rightOutputNormalized = 1;
			inputType = INPUT_TYPE.KEYBOARD;
		}
		else {
			rightOutputNormalized = 0;	
			inputType = INPUT_TYPE.KEYBOARD;
		}


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
}
