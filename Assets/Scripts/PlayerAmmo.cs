using UnityEngine;
using System.Collections;

public class PlayerAmmo : MonoBehaviour {

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
		if (transform.position.y > screenYLimit) {
			Destroy (this.gameObject);
		}
	}

	private void Move(){
		if (transform.position.y < 100) {
			Vector3 destination = this.transform.localPosition + new Vector3 (speed.x, speed.y, 0);
			this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, destination, Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log (collider.tag);
		if (collider.tag != "Enemy") return;
		collider.GetComponent<Enemy>().Damage(this.power);
		Destroy (this.gameObject);
	}
}
