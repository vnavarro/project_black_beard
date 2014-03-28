using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathSeek : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 destination;
	public float velocityLimit = 3;
	private Vector2 position;

	public float mass = 20;
	public float maxSteeringForce = 0.4f;

	public Path path;
	public int currentNode;

	public float pathRadius = 1;
	private int pathDirection = 1;

	public int completed = 0;
	public bool shouldSeek = true;

	// Use this for initialization
	void Start () {
		position = new Vector2 ();
		this.velocity = new Vector2 (0,0);

		this.path = new Path ();
//		this.path.AddNode (new Vector2(2,2));
//		this.path.AddNode (new Vector2(0,4));
		//		this.path.AddNode (new Vector2(-2,-4));
	}

	// Update is called once per frame
	void Update () {
		UpdateMovement ();
	}
	void UpdateMovement(){
		if (!shouldSeek){
			return;
		}

		position.x = this.transform.localPosition.x;
		position.y = this.transform.localPosition.y;

		Vector2 steering = this.PathFollowing();

		steering = this.TruncateVector2 (steering, maxSteeringForce);
		steering /= mass;

		velocity = this.TruncateVector2 (this.velocity + steering, this.velocityLimit);

		position += this.velocity;

		Vector3 start = this.transform.position;
		this.transform.localPosition = Vector3.Lerp(start, this.position, Time.deltaTime);
	}

	Vector2 Seek(Vector2 destination){

		Vector2 desired = destination - this.position;
		desired.Normalize();
		desired = desired * velocityLimit;

		return desired - this.velocity;
	}

	Vector2 TruncateVector2(Vector2 force,float forceLimit){
		float truncate = forceLimit / force.magnitude;
		truncate = truncate < 1.0f ? 1.0f : truncate;

		return force.normalized * forceLimit;
	}

	//Path following
	Vector2 PathFollowing(){
		Vector2 destination = Vector2.zero;
		if (path != null) {
			List<Vector2> nodes = this.path.GetNodes ();

			destination = nodes[currentNode];

			if (Vector2.Distance(this.position, destination) <= pathRadius) {
				this.currentNode += pathDirection;

				if (this.currentNode >= nodes.Count || currentNode < 0) {
					completed++;
					pathDirection *= -1;
					this.currentNode += pathDirection;
				}
			}
		}

		return destination != Vector2.zero ? this.Seek(destination) : Vector2.zero;
	}
}
