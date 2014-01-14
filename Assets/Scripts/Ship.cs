using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 destination;
	public float velocityLimit;

	private Vector2 position;

	//http://answers.unity3d.com/questions/455127/smooth-lerp-movement.html
	bool moving = false;
	public Vector3 start;
	public Vector3 end;

	public bool usePositionVariationOverTime = false;
	//

	public bool useEulerWithVector = false;

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

		this.destination = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));

		position.x = this.transform.localPosition.x;
		position.y = this.transform.localPosition.y;


		//This this.velocity = normalize(target - position) * max_velocity
		// is the same as the lines bellow 
		this.velocity = this.destination - position;
//		if (this.velocity.magnitude > this.velocityLimit) {
		this.velocity = this.velocity.normalized * this.velocityLimit;
//		}

		position = position + this.velocity;
//
//		this.transform.localPosition = new Vector3 (position.x, position.y, 0);

		this.start = this.transform.position;
		if (useEulerWithVector) {
			this.end = this.position;
		} 

		//Last variable is seconds you want Lerp to last
		if (usePositionVariationOverTime) {
			//Get the distance between the objects
			float distance = Vector3.Distance(end, start);

			//Scale the duration down
			float duration = distance * 0.5f;

			//Last variable is seconds you want Lerp to last
			StartCoroutine(MoveFromTo(start, end, duration));
		}
		else{
			StartCoroutine (MoveFromTo (start, end, 5f));
		}
	}


	IEnumerator MoveFromTo(Vector3 start,Vector3 end, float time) {
		if (!moving) {               // Do nothing if already moving
			moving = true;           // Set flag to true
			float t = 0f;
			while (t < 1.0f) {
				t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
				this.transform.position = Vector3.Lerp(start, end, t); // Set position proportional to t
				yield return 0;      // Leave the routine and return here in the next frame
			}
			moving = false;        // Finished moving
		}
	}
}
