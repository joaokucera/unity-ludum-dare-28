using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class to create a completely new person, with unique look
/// </summary>
public class PersonGenerator : MonoBehaviour {

	// PUBLIC
	//public class CPerson{

	//	public Transform	tr;
	//	public Color[]		stPersonColors;	//< Head, torso, legs, shoes
	//}

	public class CPersonColors {

		public Color[]	uniquePersonColors;
	}

	 Color[]	headColors = { Color.black, Color.red, Color.yellow, Color.grey };
	 Color[]	torsoColors = { Color.blue, Color.cyan, Color.grey, Color.green, Color.magenta, Color.red, Color.yellow };
	 Color[]	legsColors = { Color.blue, Color.cyan, Color.grey, Color.green, Color.magenta, Color.red, Color.yellow };
	 Color[]	shoesColors = { Color.black, Color.blue, Color.grey, Color.green, Color.red, Color.yellow, Color.white };
	 Color[]	skinColors = { Color.black, Color.blue, Color.grey, Color.green, Color.red, Color.yellow, Color.white };

	//public Transform			trPersonPrefab;	//< Prefab to generate the person

	//public List<CPerson>	ltPersons;			//< List of all persons in the game
	public List<CPersonColors>	ltPersonsColors;
	public CPersonColors	targetColors;		//< just a shortcut to the target colors
	public CPersonColors	stalkerColors;	//< just a shortcut to the stalker colors
	public GameObject[]		goAllPersonInTheLevel;
    public GameObject[]     goAllWorkersInTheLevel;
	public GameObject			goObjectivesCard;
	public ObjectivesCard	objectivesCardScript;

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */

	void Awake() {

		goObjectivesCard = GameObject.Find("ObjectivesCardControl");
		// DEBUG
		if(goObjectivesCard == null) {

			// DEBUG
			Debug.LogError(this.transform + " Could not find any object named ObjectivesCardControl.");
		}
		objectivesCardScript = goObjectivesCard.GetComponent<ObjectivesCard>();
	}

	// Use this for initialization
	void Start () {
	
		// Start the person's list
		//ltPersons = new List<CPerson>();
		ltPersonsColors = new List<CPersonColors>();

		GetAllPersonsInTheLevel();

		// Generate all possible combinations of people :P
		GenerateAllPossiblePersons();

		RandomizePeopleAppeareance();

		PickTarget();
		PickStalker();
		//for(int n=0; n < nTotalPersonsToGenerate; n++) {
		//	GenerateAPersonFromTheList();
		//}
	}
	
	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */

	/// <summary>
	/// Create all possible combinations of persons
	/// </summary>
	void GenerateAllPossiblePersons() {

		for(int nHeadIdx = 0; nHeadIdx < headColors.Length; nHeadIdx++) {

			for(int nTorsoIdx = 0; nTorsoIdx < torsoColors.Length; nTorsoIdx++) {

				for(int nLegsIdx = 0; nLegsIdx < legsColors.Length; nLegsIdx++) {

					for(int nShoesIdx = 0; nShoesIdx < shoesColors.Length; nShoesIdx++) {

						// Generate the person
						//string stGeneratedId = nHeadIdx.ToString() + nTorsoIdx.ToString() + nLegsIdx.ToString() + nShoesIdx.ToString();
						//CPerson person = new CPerson();
						//person.stPersonColors = new Color[] { headColors[nHeadIdx], torsoColors[nTorsoIdx], legsColors[nLegsIdx], shoesColors[nShoesIdx] }; // Unique color combination

						// Add the person to the list
						//ltPersons.Add(person);
						CPersonColors personColor = new CPersonColors();
						personColor.uniquePersonColors = new Color[] { headColors[nHeadIdx], torsoColors[nTorsoIdx], legsColors[nLegsIdx], shoesColors[nShoesIdx] }; // Unique color combination
						ltPersonsColors.Add(personColor);
					}
				}
			}
		}

		// DEBUG
		Debug.Log(this.transform + " People generated: " + ltPersonsColors.Count);
	}


	/// <summary>
	/// Get all people from the level
	/// </summary>
	void GetAllPersonsInTheLevel() {

		goAllPersonInTheLevel = GameObject.FindGameObjectsWithTag("Person");

        goAllWorkersInTheLevel = GameObject.FindGameObjectsWithTag("Worker");
	}

	/// <summary>
	/// Picks one person, pick one appearance combination
	/// </summary>
	void RandomizePeopleAppeareance() {

		foreach(GameObject go in goAllPersonInTheLevel) {

			// Pick one from the all color combinations
			int nIdxColorCombinations = Random.Range(0, ltPersonsColors.Count);
			// Get the script from the person
			PersonChangeMaterials personChangeScript = go.GetComponent<PersonChangeMaterials>();
			// and set it's colors
			personChangeScript.SetPersonColors(ltPersonsColors[nIdxColorCombinations].uniquePersonColors);
		}

        foreach (GameObject go in goAllWorkersInTheLevel)
        {
            // Pick one from the all color combinations
            int nIdxColorCombinations = Random.Range(0, ltPersonsColors.Count);
            // Get the script from the person
            PersonChangeMaterials personChangeScript = go.GetComponent<PersonChangeMaterials>();
            // and set it's colors
            personChangeScript.SetPersonColors(ltPersonsColors[nIdxColorCombinations].uniquePersonColors);
        }
	}

	void PickTarget() {

		// Get someone from the list to be the target
		int nTargetIdx = Random.Range(0, goAllPersonInTheLevel.Length);
		PersonBehavior personBehaviorScript = goAllPersonInTheLevel[nTargetIdx].GetComponent<PersonBehavior>();
		personBehaviorScript.typePersonBehavior = TypePersonBehavior.Target;

		// Keep the target info
		PersonChangeMaterials personChangeScript = goAllPersonInTheLevel[nTargetIdx].GetComponent<PersonChangeMaterials>();
		objectivesCardScript.SetTargetInfo(personChangeScript.personColors);
	}

	void PickStalker() {

		// Get someone from the list to be the target
		int nStalkerIdx = Random.Range(0, goAllPersonInTheLevel.Length);
		PersonBehavior personBehaviorScript = goAllPersonInTheLevel[nStalkerIdx].GetComponent<PersonBehavior>();
		personBehaviorScript.typePersonBehavior = TypePersonBehavior.Stalker;

		// Keep the stalker info
		PersonChangeMaterials personChangeScript = goAllPersonInTheLevel[nStalkerIdx].GetComponent<PersonChangeMaterials>();
		objectivesCardScript.SetStalkerInfo(personChangeScript.personColors);
	}	

	/// <summary>
	/// Pick a person from the list and create it in the hierarchy
	/// </summary>
	//void GenerateAPersonFromTheList() {

	//	// 1 - Pick a person
	//	int nPersonIdx = Random.Range(0, ltPersons.Count);
	//	
	//	Transform trNewPerson = Instantiate(trPersonPrefab, this.transform.position + new Vector3(0,0,nPersonIdx), this.transform.rotation) as Transform;
	//	CPerson person = ltPersons[nPersonIdx];
	//	PersonChangeMaterials	personMaterialsScripts = trNewPerson.gameObject.GetComponent<PersonChangeMaterials>();

	//	// Apply the materials
	//	if(personMaterialsScripts) {

	//		//personMaterialsScripts.ChangeHead(headMaterials[PersonId2HeadIdx(person.stId)]);
	//		personMaterialsScripts.ChangeHead(GetHeadMaterialForId(person.stId));
	//		personMaterialsScripts.ChangeTorso(GetTorsoMaterialForId(person.stId));
	//		personMaterialsScripts.ChangeLegs(GetLegsMaterialForId(person.stId));
	//		personMaterialsScripts.ChangeShoes(GetShoesMaterialForId(person.stId));
	//	}
	//}

	/// <summary>
	/// Get the head material for this id
	/// </summary>
	//Material GetHeadMaterialForId(string stId) {

	//	int nMaterialIdx = int.Parse(stId.Substring(0,1));

	//	return headMaterials[nMaterialIdx];
	//}

	/// <summary>
	/// Get the torso material for this id
	/// </summary>
	//Material GetTorsoMaterialForId(string stId) {

	//	int nMaterialIdx = int.Parse(stId.Substring(1,1));

	//	return torsoMaterials[nMaterialIdx];
	//}

	/// <summary>
	/// Get the legs material for this id
	/// </summary>
	//Material GetLegsMaterialForId(string stId) {

	//	int nMaterialIdx = int.Parse(stId.Substring(2,1));

	//	return legsMaterials[nMaterialIdx];
	//}

	/// <summary>
	/// Get the shoes material for this id
	/// </summary>
	//Material GetShoesMaterialForId(string stId) {

	//	int nMaterialIdx = int.Parse(stId.Substring(3,1));

	//	return shoesMaterials[nMaterialIdx];
	//}


	/// <summary>
	/// Check if this id is unique in the list of the generated persons
	/// </summary>
	//bool CheckForUniqueId(string stPersonId) {

	//	bool rv = true;	// We start assuming that this id is unique

	//	if(ltPersons.Count <= 0)
	//		return rv;

	//	foreach(CPerson person in ltPersons) {

	//		if(person.stId == stPersonId)
	//			rv = false;
	//	}

	//	return rv;
	//}

	/// <summary>
	/// Randomize all aspects from a person
	/// </summary>
	//void RandomizeAPerson() {

	//	// Head
	//	int nHead = Random.Range(0, headMaterials.Length);
	//	int nTorso = Random.Range(0, torsoMaterials.Length);
	//	int nLegs = Random.Range(0, legsMaterials.Length);
	//	int nShoes = Random.Range(0, shoesMaterials.Length);

	//	string stGeneratedId = nHead.ToString()+nTorso.ToString()+nLegs.ToString()+nShoes.ToString();
	//	
	//	// Check if this id is valid, i.e., if nobody has the exact same id
	//	bool isThisIdUnique = false;
	//	
	//	//while(!isThisIdUnique) {

	//		isThisIdUnique = CheckForUniqueId(stGeneratedId);
	//	//}

	//	if(isThisIdUnique) {


	//		// 1 - Instantiate the person prefab
	//		if(!trPersonPrefab) {

	//			// DEBUG
	//			Debug.LogError(this.transform + " No prefab for the Person. Put it in the Inspector");
	//		}

	//		Transform trNewPerson = Instantiate(trPersonPrefab, this.transform.position, this.transform.rotation) as Transform;

	//		// 2 - Create a new instance of Person and update it's data
	//		CPerson person = new CPerson();
	//		person.tr = trNewPerson;
	//		person.stId = stGeneratedId;

	//		// 3 - Add it to the person's list
	//		ltPersons.Add(person);

	//		// DEBUG
	//		Debug.Log(this.transform + " Added a new person to the list: " + stGeneratedId + ". Total of persons: " + ltPersons.Count);
	//	}
	//	else {

	//		// Oops, this person already exists
	//		// DEBUG
	//		Debug.LogWarning(this.transform + " Clone generated! " + stGeneratedId);
	//	}
	//}

}
