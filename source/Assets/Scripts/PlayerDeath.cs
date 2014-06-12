using UnityEngine;
using System.Collections;

/// <summary>
/// What to do when the player is caught by the stalker
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerDeath : MonoBehaviour {

    FPSInputController fpsScript;
	CharacterMotor	movementScript;
    MouseLook mouseScript;
	public AudioClip	sfxDeath;
	public LevelManager levelManagerScript;

	// Use this for initialization
	void Awake () {
	
		movementScript = gameObject.GetComponent<CharacterMotor>();
        mouseScript = gameObject.GetComponent<MouseLook>();
        fpsScript = gameObject.GetComponent<FPSInputController>();

		// Find the level manager object and script
		GameObject goLevelManager = GameObject.Find("Level Manager");

		if(goLevelManager) {

			levelManagerScript = goLevelManager.GetComponent<LevelManager>();
		}
	}

	
	public void PlayerIsDead() {

		// 1 - disable the player control (enables only look)
		movementScript.enabled = false;
        mouseScript.enabled = false;
        fpsScript.enabled = false;
        Camera.main.GetComponent<MouseLook>().enabled = false;

		//if(sfxDeath) {

		//	audio.PlayOneShot(sfxDeath);
		//}
		//animation.Play();
		// 2 - blur it's vision
		// 3 - call the card showing your failure
		levelManagerScript.LevelLostCaughtByStalker();
	}
}
