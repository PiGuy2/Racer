using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	GameObject player;
	Vector3 diff;
	public Vector3 l;
	public Quaternion r;

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
	}
}
