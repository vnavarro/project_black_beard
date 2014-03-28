using UnityEngine;
using System;
using System.Collections;

public class EnemyAmmo : MonoBehaviour {

	public Vector2 speed = new Vector2(0,10);
	public float screenYLimit = 5;
	public float power = 2;

	// Use this for initialization
	void Start () {
		screenYLimit = 9;
	}

	// Update is called once per frame
	void Update () {
		Move ();
		Remove ();
	}

	private void Remove(){
		if (Math.Abs(transform.localPosition.y) > screenYLimit) {
			Destroy (this.gameObject);
		}
	}

	private void Move(){
//		if (Math.Abs(transform.position.y) < screenYLimit) {
			Vector3 destination = this.transform.localPosition - new Vector3 (speed.x, speed.y, 0);
			this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, destination, Time.deltaTime);
//		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag != "Player") return;
		collider.GetComponent<PlayerControl>().Damage(this.power);
		Destroy (this.gameObject);
	}

}
