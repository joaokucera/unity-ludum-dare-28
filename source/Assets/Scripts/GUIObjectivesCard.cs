using UnityEngine;
using System.Collections;

public class GUIObjectivesCard : MonoBehaviour {

	public GUISkin titleSkin;
	public GUISkin textSkin;

	public Texture2D[] backgroundTexture;

	public float width;
	public float height;

	public float fCardX;
	public float fCardY;
	float fTargetTextOffsetH = 100.0f;
	float fTargetTextOffsetV = 120.0f;

	public string menuTitleText;

	public string stTargetTips = "";
	public string stStalkerTips = "";

	public Texture2D txIconFailed;
	public Texture2D txIconOk;

	Texture2D txIconTextureLeft;
	Texture2D txIconTextureRight;

	private int index = 0;

	bool bnEnableGUI = false;
	// Ok, the code below is a hideous hack.
	public LevelManager levelManagerScript;

	void Awake() {

		// Find the level manager object and script
		GameObject goLevelManager = GameObject.Find("Level Manager");

		if(goLevelManager) {

			levelManagerScript = goLevelManager.GetComponent<LevelManager>();
		}

		// Register myself with the level manager. Why? Well, I'm the only player script for now...
		levelManagerScript.RegisterGUIObjectivesCard(this);

	}

	void OnGUI()
	{

		if(!bnEnableGUI)
			return;

		GUI.skin = titleSkin;

		GUI.DrawTexture(new Rect(Screen.width * 0.5f - width * 0.5f, Screen.height * 0.5f - height * 0.5f, width, height), backgroundTexture[index]);

		Vector2 titleTextSize = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(menuTitleText));
		GUI.Label(new Rect(Screen.width / 2f - titleTextSize.x / 2f, Screen.height * 0.5f - height * 0.5f + titleTextSize.y / 10f, width, titleTextSize.y), menuTitleText);

		// Objectives
		GUI.skin = textSkin;
		GUI.Label(new Rect((Screen.width * 0.5f - width * 0.5f) + fTargetTextOffsetH, (Screen.height * 0.5f - height * 0.5f) + fTargetTextOffsetV, Screen.width / 2, Screen.height), stTargetTips);

		GUI.Label(new Rect(Screen.width * 0.5f + fTargetTextOffsetH, Screen.height * 0.5f - height * 0.5f + fTargetTextOffsetV, Screen.width / 2, Screen.height), stStalkerTips);

		if(txIconTextureLeft != null)
			GUI.DrawTexture(new Rect((Screen.width * 0.5f - width * 0.5f) + fTargetTextOffsetH, (Screen.height * 0.5f - height * 0.5f) + fTargetTextOffsetV, 256, 256), txIconTextureLeft);
		if(txIconTextureRight != null)
			GUI.DrawTexture(new Rect((Screen.width * 0.5f) + fTargetTextOffsetH, (Screen.height * 0.5f - height * 0.5f) + fTargetTextOffsetV, 256, 256), txIconTextureRight);
	}

	public void EnableGUI(bool bnEnableOrDisable) {

		bnEnableGUI = bnEnableOrDisable;
	}

	public void IconShowMissedTarget() {

		bnEnableGUI = true;
		txIconTextureLeft = txIconFailed;
	}

	public void IconShowRightTarget() {

		bnEnableGUI = true;
		txIconTextureLeft = txIconOk;
	}

	/// <summary>
	/// Change the icon material, showing that the player killed the right target
	/// </summary>
	public void IconShowCaughtByStalker() {

		bnEnableGUI = true;
		txIconTextureRight = txIconFailed;
	}
}
