using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {
	
	public float Top;
	public float Bottom;
	public float Left;
	public float Right;
	public float maximumSpeed = 1;
	public float minimumSpeed = 0.1f;

	
	float dest_speedX;
	float dest_speedZ;
	float speedX;
	float speedZ;
	

	
	void UpdateInput()
	{
		dest_speedX = Input.GetAxis ("Horizontal");
		dest_speedZ = Input.GetAxis ("Vertical");
		
		speedX = Mathf.Lerp(speedX,dest_speedX,minimumSpeed);
		speedZ = Mathf.Lerp(speedZ,dest_speedZ,minimumSpeed);
		Mathf.Clamp (speedX,-maximumSpeed,maximumSpeed);
		Mathf.Clamp (speedZ,-maximumSpeed,maximumSpeed);
	}
	
	
	void UpdatePosition()
	{
		Vector3 tmpPosition;
			tmpPosition = this.transform.position;
			
	         {
				tmpPosition.x += speedX;
				if(tmpPosition.x < Left) tmpPosition.x = Left;
				if(tmpPosition.x > Right) tmpPosition.x = Right;
			}
		 {
				tmpPosition.z += speedZ;
				if(tmpPosition.z < Top) tmpPosition.z = Top;
				if(tmpPosition.z > Bottom) tmpPosition.z = Bottom;
			}
			
		
		
		this.transform.position = tmpPosition;
	
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		UpdateInput();
		UpdatePosition();
	}
}
