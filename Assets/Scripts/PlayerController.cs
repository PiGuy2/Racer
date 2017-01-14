using UnityEngine;
using UnityEngine.Networking;
using System;
using Extension;

public class PlayerController : NetworkBehaviour
{
	// Settable
	// private int adjustment = 1;
	// -----

	public int multiplier = 1;

	//
	public float forwardDistance;

	private Vector3 dest;
	// private Vector3 newLoc;

	private float amount;
	public GameObject g;
	public int[] powerups;
	public float lastTime;

	// Boosts
	public float speedBoost;

	// Lerp
	float startTime;

	void Start ()
	{
		int num = Enum.GetNames (typeof(Collectable.Boost)).Length;
		print (num);
		powerups = new int[num];
		lastTime = 0;
		for (int i = 0; i < num; i++) {
			powerups [i] = -1;
		}

		speedBoost = 1;

		// Lerp
		//

		//test
//		Collectable.Boost b = Collectable.Boost.rocket;
//		print ("Test");
//		print (b.GetTime ());
	}

	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}

		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 100.0f;
		float y = Input.GetAxis ("Vertical") * Time.deltaTime * 5.0f;

		// amount = y * adjustment;

		// forwardDistance += amount * multiplier;
		y = (y * speedBoost) * multiplier;
		y = y * -1;

		// newLoc = Destination ();

		transform.Rotate (0, x, 0);
		transform.Translate (y, 0, 0);
		// transform.position = Vector3.Lerp (transform.position, newLoc, 0.5f * Time.deltaTime);

		if ((Time.realtimeSinceStartup - lastTime) > 1) {
			ReduceTime ();
			lastTime = Time.realtimeSinceStartup;
		}
	}

	public override void OnStartLocalPlayer ()
	{
		print ("Good");
		transform.Rotate (0, 90, 0);
		GameObject camera = GameObject.Find ("Main Camera");
		CameraController cameraController = camera.GetComponent<CameraController> ();
		cameraController.Reset ();
		cameraController.Follow (this.gameObject);

		StatExtensions.PlayerStart (this.gameObject);
	}

	private Vector3 Destination ()
	{
		Vector3 loc = this.gameObject.transform.position;

		float y = loc.y + forwardDistance;

		Vector3 dest = new Vector3 (loc.x, y, loc.z);

		return dest;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("C")) {
			other.gameObject.GetComponent<Collectable> ().Collect (this.gameObject);
		}
	}

	public void Boost (Collectable.Boost boost)
	{
		print (boost);
		print ((int)boost);
		powerups [(int)boost] += boost.GetTime ();
		print ("done1");
		boost.GetStat ().ChangeStat (boost.GetAmount ());
	}

	public void ReduceTime ()
	{
		int num = powerups.GetLength (0);
		for (int i = 0; i < num; i++) {
			if (powerups [i] > 0) {
				powerups [i] = powerups [i] - 1;
			} else if (powerups [i] == 0) {
				UndoPowerup ((Collectable.Boost)i);
				powerups [i] = powerups [i] - 1;
			} else if (powerups [i] < -1) {
				powerups [i] = 0;
			} else {
				// if time on powerup is -1
			}
		}
	}

	public void UndoPowerup (Collectable.Boost boost)
	{
		print ("done");
		Stat s = boost.GetStat ();
		float a = boost.GetAmount ();
		a = a * -1;

		s.ChangeStat (a);
		print ("done2");
	}

	public enum Stat
	{
		none,
		speed
	}

	public Vector3 Interpolate (Vector3 start, Vector3 end)
	{
		Vector3 ret;
		float rate = 1;
		ret = Vector3.Lerp (start, end, Time.deltaTime * rate);
		return Vector3.up;
	}
}

namespace Extension
{
	public static class BoostExtensions
	{
		public static int GetTime (this Collectable.Boost boost)
		{
			int b = (int)boost;
			if (b == 1) {
				return 10;
			} else if (b == 2) {
				return 1;
			} else {
				return -1;
			}
		}

		public static PlayerController.Stat GetStat (this Collectable.Boost boost)
		{
			int b = (int)boost;
			if (b == 1) {
				return PlayerController.Stat.speed;
			} else if (b == 2) {
				return PlayerController.Stat.speed;
			} else {
				return PlayerController.Stat.none;
			}
		}

		public static float GetAmount (this Collectable.Boost boost)
		{
			int b = (int)boost;
			if (b == 1) {
				return 2;
			} else if (b == 2) {
				return 10;
			} else {
				return -1;
			}
		}
	}

	public static class StatExtensions
	{
		public static GameObject g;

		public static void ChangeStat (this PlayerController.Stat stat, float amount)
		{
			if (stat == PlayerController.Stat.speed) {
				PlayerController playerController = g.GetComponent<PlayerController> ();
				playerController.speedBoost = playerController.speedBoost + amount;

				MonoBehaviour.print ("Changed stat");
			} else {
				// error
			}
		}

		public static void PlayerStart (UnityEngine.GameObject player)
		{
			MonoBehaviour.print ("Set");
			g = player;
			MonoBehaviour.print ("Player");
			MonoBehaviour.print (g);
		}
	}
}