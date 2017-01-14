using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	GameObject player;
	Vector3 diff;
	Vector3 l;
	Quaternion r;
	public float ro;
	public float curR;
	public float lastR;
	Vector3 n;
	bool sb;
	bool s;

	// Use this for initialization
	void Start ()
	{
		l = this.gameObject.transform.position;
		r = this.gameObject.transform.rotation;
		s = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player == null) {
			return;
		}

//		this.gameObject.transform.position = player.transform.position + diff;

//		curR = player.transform.rotation.y;
		//this.gameObject.transform.RotateAround (player.transform.position, Vector3.up, (curR - lastR) * 180);
		//this.gameObject.transform.RotateAround (Vector3.up, player.transform.position, (curR - lastR) * 180);
//		lastR = curR;
	}

	void LateUpdate ()
	{
		if (s) {
			float desiredAngle = player.transform.eulerAngles.y + 90;
			Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
			n = player.transform.position - (rotation * diff);
			n.y += 7;
			transform.position = n;
			transform.LookAt (player.transform);
		} else if (sb) {
			s = true;
		}
	}

	public void Follow (GameObject followObject)
	{
		player = followObject;
		diff = this.gameObject.transform.position - player.transform.position;
		ro = followObject.transform.rotation.y;
		lastR = ro;
		sb = true;
	}

	public void Reset ()
	{
		this.gameObject.transform.position = l;
		this.gameObject.transform.rotation = r;
	}
}
