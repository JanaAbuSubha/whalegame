using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///
/// Creates wandering behaviour for a Whale parent object 
/// By Jana Abu Subha 
/// 4/16/2026
/// 
[RequireComponent(typeof(CharacterController))]
public class WhaleMovement : MonoBehaviour
{
	public float speed = 0.1f;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 20;
	public Transform spriteTransform;
	CharacterController controller;
	float heading;
	Vector3 targetRotation;

	///
	/// Called once at runtime, sets up controller, initial rotation, and calls method which continues movement
	/// 
	void Awake(){
		controller = GetComponent<CharacterController>();
		//transform.rotation = Quaternion.Euler(targetRotation);
		StartCoroutine(NewHeading());

	}
	///
	/// Called every frame, updates rotation of whale using Slerp method which interpolates for smooth movement, moves whale incrementally towards the target, 
	/// and flips the sprite according to the direction of x motion 
	/// 
	void Update(){
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.right);
        controller.Move(forward * speed * Time.deltaTime);
		if (controller.velocity.sqrMagnitude > 0.0001f)
    		spriteTransform.localRotation = Quaternion.Euler(controller.velocity.x < 0f ? 180f : 0f, 180f, 180f);
		
	}

	///
	/// Calculates a new direction to move towardds depending on the current direction of travel. 
	/// Whales which are traveling horizontally change to a diagonal movement, and vice versa. 
	/// 
	IEnumerator NewHeading(){
		while (true) {
			bool isMovingHorizontally = Mathf.Abs(controller.velocity.z) < 0.01f;
			if (isMovingHorizontally){
				float randomValue = Random.Range(10f, 20f);
				yield return new WaitForSeconds(randomValue);
				isMovingHorizontally = Mathf.Abs(controller.velocity.z) < 0.01f;
            		if (isMovingHorizontally){
            		    NewDiagonal();
            		} else {
            	    	NewHorizontal();
            		}
			} else {
				float randomValue2 = Random.Range(3f, 7f);
				yield return new WaitForSeconds(randomValue2);

				isMovingHorizontally = Mathf.Abs(controller.velocity.z) < 0.01f;
            		if (isMovingHorizontally){
            	    NewDiagonal();
            	} else {
            	    NewHorizontal();
            	}
			}
		}
	}

	///
	/// Helper method called by NewHeading() which adds 12 diagonal vector directions to a list and chooses a random one among them to be the target. 
	/// 
	void NewDiagonal(){
		Vector3 samedepth1 = new Vector3(0f, 30f, 0f);
		Vector3 samedepth2 = new Vector3(0f, 330f, 0f);
		Vector3 samedepth3 = new Vector3(0f, 150f, 0f);
		Vector3 samedepth4 = new Vector3(0f, 210f, 0f);

		Vector3 intoscreen1 = new Vector3(3f, 30f, 0f);
		Vector3 intoscreen2 = new Vector3(3f, 330f, 0f);
		Vector3 intoscreen3 = new Vector3(3f, 150f, 0f);
		Vector3 intoscreen4 = new Vector3(3f, 210f, 0f);

		Vector3 outofscreen1 = new Vector3(357f, 30f, 0f);
		Vector3 outofscreen2 = new Vector3(357f, 330f, 0f);
		Vector3 outofscreen3 = new Vector3(357f, 150f, 0f);
		Vector3 outofscreen4 = new Vector3(357f, 210f, 0f);
		List<Vector3> directions = new List<Vector3> { samedepth1, samedepth2, samedepth3, samedepth4, intoscreen1, intoscreen2, intoscreen3, intoscreen4, outofscreen1, outofscreen2, outofscreen3, outofscreen4 };

		targetRotation = directions[Random.Range(0, directions.Count)];

	}
	///
	/// Helper method called by NewHeading() which chooses between two horizontal vector directions and assigns one to be target. 
	/// 
	void NewHorizontal(){
		float leftorright = controller.velocity.x;
		targetRotation = new Vector3(0f, leftorright < 0 ? 180f : 0f, 0f);
	}

}