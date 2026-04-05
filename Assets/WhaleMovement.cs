using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class WhaleMovement : MonoBehaviour
{
	public float speed = 0.1f;
	public float directionChangeInterval = 10;
	public float maxHeadingChange = 20;

	CharacterController controller;
	float heading;
	Vector3 targetRotation;

	void Awake ()
	{
		controller = GetComponent<CharacterController>();
        // OG version: 
		// Set random initial rotation
		// heading = Random.Range(0, 360);
        // New set initial heading to 180 or 0
        heading = Random.value < 0.5f ? 0f : 180f;
        transform.eulerAngles = new Vector3(0, heading, 0);

		StartCoroutine(NewHeading());
	}

	void Update ()
	{
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.right);
        controller.SimpleMove(forward * speed);

        float moveX = transform.TransformDirection(Vector3.right).x;

        // Flip on Z axis based on direction
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 180f); // flipped
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0f);   // normal
        }
	}

	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);

		heading = Random.Range(floor, ceil);
        
		targetRotation = new Vector3(0, heading, 0);
	}
}