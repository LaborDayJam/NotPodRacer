using UnityEngine;
using System.Collections;

public class PilotAnimation : MonoBehaviour {

	public Animation leftHand;
	public Animation rightHand;
	
	public void UpdateHandAnimation(int handIndex, float output)
	{
		AnimationState targetAnim;
		Animation hand;
		string animName;
		if (handIndex ==  0) {
			hand = leftHand;
			animName = "leftControl";
			targetAnim = leftHand [animName];
		} else {
			hand = rightHand;
			animName = "rightControl";
			targetAnim = rightHand [animName];
		}
		targetAnim.time = output ;
		targetAnim.speed = 0;
		hand.Play (animName);
	}

}
