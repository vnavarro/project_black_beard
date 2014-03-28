using UnityEngine;
using System.Collections;

public class EnemyIA : MonoBehaviour {

	private enum State
	{
		Waiting,
		Moving,
		Shooting,
		Seeking
	}

	private State current_state;

	// Use this for initialization
	void Start () {
		current_state = State.Waiting;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateState ();
	}

	void UpdateState(){
		switch (current_state) {
			case State.Waiting:
			current_state = State.Moving;
			break;
			default:
			break;
		}
	}

	void OnMoving(){
		current_state = State.Moving;
	}

}
