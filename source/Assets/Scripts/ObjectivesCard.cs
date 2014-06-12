using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///
/// </summary>
public class ObjectivesCard : MonoBehaviour {

	// PUBLIC
	public Transform trMainCamera;
	public DepthOfFieldScatter dofScript;
	public Transform trObjectivesCard;
	public bool bnCardIsShown = false;
	public bool bnIgnoreInput = false;

	GUIObjectivesCard guiObjectivesCardScript;

	public AudioClip	sfxShowCard;

	// Ok, the code below is a hideous hack.
	public LevelManager levelManagerScript;

	IDictionary<Color, string> dictColorsToEnglish;


	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */

	void Awake () {
	
		//
		dictColorsToEnglish = new Dictionary<Color, string>();
		dictColorsToEnglish.Add(Color.black, "black");
		dictColorsToEnglish.Add(Color.blue, "blue");
		dictColorsToEnglish.Add(Color.cyan, "cyan");
		dictColorsToEnglish.Add(Color.green, "green");
		dictColorsToEnglish.Add(Color.yellow, "yellow");
		dictColorsToEnglish.Add(Color.red, "red");
		dictColorsToEnglish.Add(Color.magenta, "magenta");
		dictColorsToEnglish.Add(Color.grey, "grey");
		dictColorsToEnglish.Add(Color.white, "white");


		// Get the main camera
		if(trMainCamera == null) {

			trMainCamera = Camera.main.transform;
		}

		if(trObjectivesCard == null) {

			trObjectivesCard = GameObject.Find("ObjectivesCard").transform;
		}

		// Get the DOF script
		dofScript = trMainCamera.gameObject.GetComponent<DepthOfFieldScatter>();
		dofScript.enabled = false;

		// Find the level manager object and script
		GameObject goLevelManager = GameObject.Find("Level Manager");

		if(goLevelManager) {

			levelManagerScript = goLevelManager.GetComponent<LevelManager>();
		}

		// Register myself with the level manager. Why? Well, I'm the only player script for now...
		levelManagerScript.RegisterObjectiveCard(this.transform, this);

		guiObjectivesCardScript = gameObject.GetComponent<GUIObjectivesCard>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!bnIgnoreInput && Input.GetButtonDown("Fire2")) {

			ShowObjectivesCard();
		}
	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	public void ShowObjectivesCard() {

		dofScript.enabled = !dofScript.enabled;
		dofScript.focalTransform = trObjectivesCard;

		bnCardIsShown = !bnCardIsShown;

		guiObjectivesCardScript.EnableGUI(bnCardIsShown);
		trObjectivesCard.gameObject.renderer.enabled = !bnCardIsShown;

		// Play sfx
		if(sfxShowCard) {

			AudioSource.PlayClipAtPoint(sfxShowCard, this.transform.position);
		}

		//if(!bnCardIsShown) {
		//	trObjectivesCard.animation["ObjectivesCardDisplay"].speed = -1;

		//	if(trObjectivesCard.animation["ObjectivesCardDisplay"].time == 0)
		//		trObjectivesCard.animation["ObjectivesCardDisplay"].time = trObjectivesCard.animation["ObjectivesCardDisplay"].length;
		//}
		//else {

		//	trObjectivesCard.animation["ObjectivesCardDisplay"].speed = 1;
		//}

		//trObjectivesCard.animation.Play();
	}

	/// <summary>
	/// Called from the level manager when the level ends. Will force the card to be shown and the input to
	/// show/hide the card will be ignored
	/// </summary>
	public void ForceCardToBeShow() {

		bnIgnoreInput = true;
		dofScript.enabled = true;
		dofScript.focalTransform = trObjectivesCard;
		trObjectivesCard.gameObject.renderer.enabled = false;

		// Play sfx
		if(sfxShowCard) {

			AudioSource.PlayClipAtPoint(sfxShowCard, this.transform.position);
		}
	}

	/// <summary>
	/// Receive info from the target and set the text for the cards
	/// </summary>
	public void SetTargetInfo(Color[] targetColors ) {

		string stTargetText = "\tTarget info:\n(find and kill it)\n\n";

		stTargetText += "- " + dictColorsToEnglish[targetColors[0]] + " hair\n";
		stTargetText += "- " + dictColorsToEnglish[targetColors[1]] + " shirt\n";
		stTargetText += "- " + dictColorsToEnglish[targetColors[2]] + " trousers\n";
		stTargetText += "- " + dictColorsToEnglish[targetColors[3]] + " shoes";
		
		// Set the text to the card
		guiObjectivesCardScript.stTargetTips = stTargetText;
	}

	/// <summary>
	/// Receive info from the stalker and set the text for the cards
	/// </summary>
	public void SetStalkerInfo(Color[] targetColors ) {

		string stStalkerText = "\tStalker info:\n(avoid him!)\n\n";

		stStalkerText += "- " + dictColorsToEnglish[targetColors[0]] + " hair\n";
		stStalkerText += "- " + dictColorsToEnglish[targetColors[1]] + " shirt\n";
		stStalkerText += "- " + dictColorsToEnglish[targetColors[2]] + " trousers\n";
		stStalkerText += "- " + dictColorsToEnglish[targetColors[3]] + " shoes";
		
		// Set the text to the card
		guiObjectivesCardScript.stStalkerTips = stStalkerText;
	}
}
