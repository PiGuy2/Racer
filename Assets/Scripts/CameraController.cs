using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	GameObject player;
	Vector3 diff;
	Vector3 l;
	Quaternion r;

	// Use this for initialization
	void Start ()
	{
		l = this.gameObject.transform.position;
		r = this.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player == null) {
			return;
		}

		this.gameObject.transform.position = player.transform.position + diff;
	}

	public void Follow (GameObject followObject)
	{
		player = followObject;
		diff = this.gameObject.transform.position - player.transform.position;
	}

	public void Reset ()
	{
		this.gameObject.transform.position = l;
		this.gameObject.transform.rotation = r;
	}
}
