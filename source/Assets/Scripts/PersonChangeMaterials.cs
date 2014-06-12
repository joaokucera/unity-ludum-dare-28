using UnityEngine;
using System.Collections;

/// <summary>
/// Class to change the material on the person model
/// </summary>
public class PersonChangeMaterials : MonoBehaviour {

	// PUBLIC
	public Transform trHead;
	public Transform trTorso;
	public Transform trLegs;
	public Transform trShoesLeft;
	public Transform trShoesRight;
	
	public Color[] personColors;
 	
	void Awake() {

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPersonColors(Color[] colors) {

		personColors = colors;
		ChangeHead(personColors[0]);
		ChangeTorso(personColors[1]);
		ChangeLegs(personColors[2]);
		ChangeShoes(personColors[3]);
	}

	/// <summary>
	/// Change the material of the head
	/// </summary>
	public void ChangeHead(Color newColor) {

		if(trHead)
			trHead.gameObject.renderer.material.color = newColor;
	}

	/// <summary>
	/// Change the material of the torso
	/// </summary>
	public void ChangeTorso(Color newColor) {

		if(trTorso)
			trTorso.gameObject.renderer.material.color = newColor;
	}

	/// <summary>
	/// Change the material of the torso
	/// </summary>
	public void ChangeLegs(Color newColor) {

		if(trLegs)
			trLegs.gameObject.renderer.material.color = newColor;
	}

	/// <summary>
	/// Change the material of the torso
	/// </summary>
	public void ChangeShoes(Color newColor) {

		if(trShoesLeft)
			trShoesLeft.gameObject.renderer.material.color = newColor;
		if(trShoesRight)
			trShoesRight.gameObject.renderer.material.color = newColor;
	}

	///// <summary>
	///// Change the material of the head
	///// </summary>
	//public void ChangeHead(Material newMaterial) {

	//	trHead.gameObject.renderer.material = newMaterial;
	//}

	///// <summary>
	///// Change the material of the torso
	///// </summary>
	//public void ChangeTorso(Material newMaterial) {

	//	trTorso.gameObject.renderer.material = newMaterial;
	//}

	///// <summary>
	///// Change the material of the torso
	///// </summary>
	//public void ChangeLegs(Material newMaterial) {

	//	trLegs.gameObject.renderer.material = newMaterial;
	//}

	///// <summary>
	///// Change the material of the torso
	///// </summary>
	//public void ChangeShoes(Material newMaterial) {

	//	trShoes.gameObject.renderer.material = newMaterial;
	//}
}
