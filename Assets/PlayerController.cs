using UnityEngine;

public class PlayerController : MonoBehaviour
{
	void Update ()
	{
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 100.0f;
		var y = Input.GetAxis ("Vertical") * Time.deltaTime * -5.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (y, 0, 0);
	}
}