using UnityEngine;
using System.Collections;

public class ShipSeek : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 destination;
	public float velocityLimit = 3;
	private Vector2 position;

	public bool followMouse;

	public float mass = 20;
	public float maxSteeringForce = 0.4f;

	// Use this for initialization
	void Start () {
		position = new Vector2 ();
		this.velocity = new Vector2 (0,0);
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

		Vector2 steering = Seek (this.destination);
		steering = this.TruncateVector2 (steering, maxSteeringForce);
		steering /= mass;

		velocity = this.TruncateVector2 (this.velocity + steering, this.velocityLimit);

		position += this.velocity;

		Vector3 start = this.transform.position;
		this.transform.localPosition = Vector3.Lerp(start, this.position, Time.deltaTime);
	}

	Vector2 Seek(Vector2 destination){
		//		Vector2 desiredVelocity = this.destination - position;
		//		Vector2 steering = desiredVelocity - this.velocity;

		Vector2 desired = destination - this.position;
		desired.Normalize();
		desired = desired * velocityLimit;
//		desired = this.TruncateVector2 (desired, velocityLimit);

		return desired - this.velocity;
	}

	Vector2 TruncateVector2(Vector2 force,float forceLimit){
		float truncate = forceLimit / force.magnitude;
		truncate = truncate < 1.0f ? 1.0f : truncate;
				
		return force.normalized * forceLimit;
	}
}
