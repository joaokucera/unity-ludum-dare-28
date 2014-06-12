using UnityEngine;
using System.Collections;

/// <summary>
///
/// </summary>
public class PlayerAttack : MonoBehaviour {

	// PUBLIC
	public Transform	trPlayerWeaponMesh;	//< Mesh with animations
	public string			stAttackAnimation;	//< Attack animation

	public Transform	trMainCamera;
	public MouseLook	mouseLookScript;
	public MouseLook	mouseLookFromCameraScript;
	public bool				bnAlreadyAttacked = false;

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */

	void Awake() {

		mouseLookScript = gameObject.GetComponent<MouseLook>();
		mouseLookFromCameraScript = trMainCamera.gameObject.GetComponent<MouseLook>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!bnAlreadyAttacked && Input.GetButtonDown("Fire1")) {

			Attack();
		}
	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	public void Attack() {

		if(!trPlayerWeaponMesh.animation.IsPlaying(stAttackAnimation)) {

			DisableMouseLook();
			trPlayerWeaponMesh.animation.Play(stAttackAnimation);
			StartCoroutine(WaitToRestoreMouseLook(trPlayerWeaponMesh.animation[stAttackAnimation].length));
		}

		bnAlreadyAttacked = true;
	}

	/// <summary>
	/// Disables the mouse look while attacking
	/// </summary>
	public void DisableMouseLook() {

		mouseLookScript.enabled = false;
		mouseLookFromCameraScript.enabled = false;
	}

	/// <summary>
	/// Disables the mouse look while attacking
	/// </summary>
	public void EnableMouseLook() {

        //mouseLookScript.enabled = true;
        //mouseLookFromCameraScript.enabled = true;
	}

	/// <summary>
	/// Restores the mouse look after the animation is over
	/// </summary>
	IEnumerator WaitToRestoreMouseLook(float fAfterSeconds) {

		yield return new WaitForSeconds(fAfterSeconds);
		EnableMouseLook();
	}
}
