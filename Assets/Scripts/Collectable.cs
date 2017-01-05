using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	private Vector3 rot;
	public Boost boostType;
	private Material alt;
	private Shader altS;

	// Use this for initialization
	void Start ()
	{
		rot = new Vector3 (0, 1, 0);
		if ((int)boostType == 0) {
			ChangeColor (this.gameObject, Color.red);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.Rotate (rot, Space.World);
	}

	public void Collect (GameObject player)
	{
		player.GetComponent<PlayerController> ().Boost (boostType);
		this.gameObject.SetActive (false);
	}

	public enum Boost
	{
		none,
		speed,
		rocket

	}

	private void ChangeColor (GameObject target, Color c)
	{
		altS = Shader.Find ("Standard");
		alt = new Material (altS);
		// alt.SetColor ("Albedo", Color.red);
		alt.color = c;
		Renderer rend = target.GetComponent<Renderer> ();
		rend.material = alt;
	}
}