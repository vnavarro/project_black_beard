using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private float ENEMY_HIT_DAMAGE = -5;

	public float speed = 5;
	public GameObject ammo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Shoot ();
	}

	void Move(){
		float horiz = Input.GetAxis("Horizontal");
		this.transform.Translate(new Vector3(horiz,0,0) * this.speed * Time.deltaTime);

		float vertic = Input.GetAxis("Vertical");
		this.transform.Translate(new Vector3(0,vertic,0) * this.speed *  Time.deltaTime);
	}

	void Shoot(){
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate(ammo, new Vector3(this.transform.position.x, this.transform.position.y, -1) , Quaternion.identity);
		}
	}

	public void Damage(float damage){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log (collider.tag);
		if (collider.tag != "Enemy") return;
		Damage(ENEMY_HIT_DAMAGE);
	}
}
