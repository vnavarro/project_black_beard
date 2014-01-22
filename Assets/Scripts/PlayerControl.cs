using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		float horiz = Input.GetAxis("Horizontal");
		this.transform.Translate(new Vector3(horiz,0,0) * this.speed * Time.deltaTime);

		float vertic = Input.GetAxis("Vertical");
		this.transform.Translate(new Vector3(0,vertic,0) * this.speed *  Time.deltaTime);
	}
}
