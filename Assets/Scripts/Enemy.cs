using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float life = 25;

	#region shipseek

	private PathSeek pathSeeker;

	public List<Vector2> waypoints = new List<Vector2>();

	#endregion

	private float stopTime = 0.5f;
	public GameObject ammo;
	public bool fired = false;

	private bool setup = false;

	public enum MoveStyle
	{
		Straight,
		MidMoonRight,
		StraightSRight,
	}

	public MoveStyle iaMovement;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		SetupPathSeeker();
		Aim ();
		UpdateMove();

	}

	void UpdateMove(){
		if (iaMovement == MoveStyle.MidMoonRight || 
			iaMovement == MoveStyle.Straight ||
			iaMovement == MoveStyle.StraightSRight) {
			if (this.pathSeeker.completed >= 1) {
				Kill ();
			}
		}
	}

	private void SetupPathSeeker(){
		if (setup) return;

		pathSeeker = GetComponent<PathSeek> ();

		if (iaMovement == MoveStyle.Straight) {
			LoadStraightMovement();
		}
		else if (iaMovement == MoveStyle.MidMoonRight){
			LoadMidMoonRightMovement();
		}
		else if(iaMovement == MoveStyle.StraightSRight){
			LoadStraightSRightMovement();
		}

		pathSeeker.path.AddNodes (this.waypoints);

		setup = true;
	}

	private void LoadStraightMovement(){
		Vector3 position = this.transform.position;
		position.x = Random.Range(-6,6);
		position.y = 10;
		this.transform.position = position;

		float x = position.x;
		float y = position.y;	

		for (int i = 0; i < 5; i++) {
			waypoints.Add (new Vector2(x, y));
			y-=5; 
		}

		pathSeeker.velocityLimit = 4;
		pathSeeker.mass = 5;
		pathSeeker.maxSteeringForce = 1;
	}

	private void LoadMidMoonRightMovement(){
		Vector3 position = this.transform.position;
		position.x = Random.Range(-8,10);
		this.transform.position = position;

		float x = position.x;
		float y = position.y;	


		for (int i = 0; i < 7; i++) {
			waypoints.Add (new Vector2(x, y));
			if (i == 2) {
				x = 0;
			}
			else if (i == 3) {
				x = 4;
			}
			else {
				x += 2;
			}

			if (i > 3) {
				y += 2;
			}
			else if (i < 2) {
				y -= 2;
			}
		}

		pathSeeker.velocityLimit = 4;
		pathSeeker.mass = 6;
		pathSeeker.maxSteeringForce = 10;
		pathSeeker.pathRadius = 2;
	}

	private void LoadStraightSRightMovement(){
		Vector3 position = this.transform.position;
		position.x = -8;
		position.y = 6;
		this.transform.position = position;

		float x = position.x;
		float y = position.y;

		waypoints.Add (new Vector2(x, y));

		x+=13;
		waypoints.Add (new Vector2(x, y));

		y-=2;
		waypoints.Add (new Vector2(x, y));

		x-=10;
		waypoints.Add (new Vector2(x, y));

		y-=2;
		waypoints.Add (new Vector2(x, y));

		x+=10;
		waypoints.Add (new Vector2(x, y));

		y-=12;
		waypoints.Add (new Vector2(x, y));

		pathSeeker.velocityLimit = 4;
		pathSeeker.mass = 6;
		pathSeeker.maxSteeringForce = 10;
		pathSeeker.pathRadius = 1;
	}

	public void Kill(){
		Destroy(this.gameObject);
	}

	public void Damage(float dmgQuantity){
		life -= dmgQuantity;
		if (life <= 0){
			Kill ();
		}
	}


	void Shoot(){
		if (this.fired) return;
		GameObject player = GameObject.FindWithTag ("Player");
		Instantiate(ammo, new Vector3(this.transform.position.x, this.transform.position.y, -1) , Quaternion.identity);
		this.fired = true;
	}

	void Aim(){

		if (iaMovement == MoveStyle.Straight) {
			if (pathSeeker.currentNode == 2) {
				Shoot ();
				if (this.stopTime > 0) {
					this.stopTime -= Time.deltaTime;
					pathSeeker.shouldSeek = false;
					return;
				}
				pathSeeker.shouldSeek = true;
			}
		} else if (iaMovement == MoveStyle.MidMoonRight) {
			if (this.transform.position.x > -0.1 && this.transform.position.x < 0.1) {
				Shoot ();
			}
		}
		else if (iaMovement == MoveStyle.StraightSRight) {
			if (this.transform.position.x > -0.1 && this.transform.position.x < 0.1) {
				Shoot ();
			} else if (this.transform.position.x > -2.1 && this.transform.position.x < -1.1) {
				Shoot ();
			} else if (this.transform.position.x > 1.1 && this.transform.position.x < 3.1) {
				Shoot ();
			} else {
				this.fired = false;
			}
		}
	}
}