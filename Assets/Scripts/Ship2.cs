using UnityEngine;
using System.Collections;

public class Ship2 : MonoBehaviour {

	public Vector2 velocity;
	public Vector2 destination;
	public Vector2 position;

//	http://answers.unity3d.com/questions/9985/limiting-rigidbody-velocity.html
// This MonoBehaviour uses hard clamping to limit the velocity of a rigidbody.

// The maximum allowed velocity. The velocity will be clamped to keep 
// it from exceeding this value.
	public float maxVelocity;

// The cached rigidbody reference.
	private Rigidbody2D rb;
// A cached copy of the squared max velocity. Used in FixedUpdate.
	private float sqrMaxVelocity;

// Awake is a built-in unity function that is called called only once during the lifetime of the script instance.
// It is called after all objects are initialized.
// For more info, see:
// http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.Awake.html
	void Awake() {
		rb = this.rigidbody2D;
		SetMaxVelocity(maxVelocity);
		position = new Vector2 ();
	}

// Sets the max velocity and calculates the squared max velocity for use in FixedUpdate.
// Outside callers who wish to modify the max velocity should use this function. Otherwise,
// the cached squared velocity will not be recalculated.
	public void SetMaxVelocity(float maxVelocity){
		this.maxVelocity = maxVelocity;
		sqrMaxVelocity = maxVelocity * maxVelocity;
	}

// FixedUpdate is a built-in unity function that is called every fixed framerate frame.
// We use FixedUpdate instead of Update here because the docs recommend doing so when
// dealing with rigidbodies.
// For more info, see:
// http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.FixedUpdate.html 
	void FixedUpdate() {
		position.x = this.transform.localPosition.x;
		position.y = this.transform.localPosition.y;
		this.velocity = this.destination - position;
		rb.velocity = this.velocity;

		var v = rb.velocity;
		// Clamp the velocity, if necessary
		// Use sqrMagnitude instead of magnitude for performance reasons.
		if(v.sqrMagnitude > sqrMaxVelocity){ // Equivalent to: rigidbody.velocity.magnitude > maxVelocity, but faster.
			// Vector3.normalized returns this vector with a magnitude 
			// of 1. This ensures that we're not messing with the 
			// direction of the vector, only its magnitude.
			rb.velocity = v.normalized * maxVelocity;
		}   
	}

// Require a Rigidbody component to be attached to the same GameObject.
}

