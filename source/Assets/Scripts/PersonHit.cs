using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PersonHit : MonoBehaviour {

	PersonBehavior behaviorScript;
	public AudioClip	sfxPersonDying;

	// Use this for initialization
	void Start () {
	
		behaviorScript = gameObject.GetComponent<PersonBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// What to do when this person is hit by the syringe
	public bool Hit() {

		bool rv = false;
		PersonBehavior behaviorScript = gameObject.GetComponent<PersonBehavior>();

		//if(this.transform.tag == "Target") {

		//	// Correct target
		//	rv = true;

		//	// DEBUG
		//	Debug.Log("Target hit");
		//}

		if(behaviorScript.typePersonBehavior == TypePersonBehavior.Target)		 {

			rv = true;

			// DEBUG
			Debug.Log("Target hit");
		}

		// TODO: play a SFX
		

		// Kill this person
		behaviorScript.bnIsDead = true;

		if(sfxPersonDying != null) {

			AudioSource.PlayClipAtPoint(sfxPersonDying, transform.position);
		}

		return rv;
	}

	/// <summary>
	/// Check if the stalker got the player
	/// </summary>
	void OnTriggerEnter(Collider hit) {

		if(hit.gameObject.tag == "Player") {
			if(behaviorScript.typePersonBehavior == TypePersonBehavior.Stalker) {

				PlayerDeath playerDeathScript = hit.transform.gameObject.GetComponent<PlayerDeath>();
				playerDeathScript.PlayerIsDead();
				// DEBUG
				Debug.Log("stalker got the player");
			}
		}
	}
}
