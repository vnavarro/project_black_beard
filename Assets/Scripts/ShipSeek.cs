using UnityEngine;
using System.Collections;

public class ShipSeek : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 destination;
	public float velocityLimit;
	private Vector2 position;

	public bool followMouse;

	// Use this for initialization
	void Start () {
		position = new Vector2 ();
		this.velocity = new Vector2 ();
	}

	// Update is called once per frame
	void Update () {
		UpdateMovement ();
	}
	void UpdateMovement(){

		if (this.followMouse) {
			this.destination = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
		}

		position.x = this.transform.localPosition.x;
		position.y = this.transform.localPosition.y;


		//This this.velocity = normalize(target - position) * max_velocity
		// is the same as the lines bellow 
		this.velocity = this.destination - position;

//		if (this.velocity.magnitude > this.velocityLimit) {

		this.velocity = this.velocity.normalized * this.velocityLimit;

//		}

		position = position + this.velocity;

		Vector3 start = this.transform.position;
		this.transform.localPosition = Vector3.Lerp(start, this.position, Time.deltaTime);
	}
}
