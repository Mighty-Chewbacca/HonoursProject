using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{

	public class CarAIController : MonoBehaviour 
	{
		public CarController carController;
        public NotificationsScript notifications;
		public Text currentSpeedText;

		public bool carCanGo = false;

		public bool isBraking = false;
		public bool isAccelerating = false;
		public bool isTurningLeft = false;
		public bool isTurningRight = false;
		public bool isHandBraking = false;
		public bool isReversing = false;

		// Use this for initialization
		void Start () 
		{
	
		}
	
		// Update is called once per frame
		void Update () 
		{
			currentSpeedText.text = Mathf.RoundToInt (carController.CurrentSpeed).ToString ();
		}

		private void FixedUpdate()
		{ 
			//this is where the move method will be called on the car - it will be called based upon the bools held to control the car

			if (carCanGo) //first check if the car is actually allowed to go if not - it doesnt matter what it should be doing because it cant move
			{
				int steer, accel, handbrake;

				if (isAccelerating)
				{
					accel = 1;
				}
				else if (isReversing)
				{
					accel = -1;
				}
				else if (isBraking)
				{
					accel = 0;
				}
				else
				{
					accel = 0;
				}

				if (isHandBraking)
				{
					handbrake = 1;
				}
				else
				{
					handbrake = 0;
				}

				if (isTurningLeft)
				{
					steer = -1;
				}
				else if (isTurningRight)
				{
					steer = 1;
				}
				else
				{
					steer = 0;
				}

				//move function takes (left or right, accelerating, braking, handbraking) - all values are between -1 and 1 but will be clamped to 0 and 1 in the locomotion script
				Debug.Log ("calling the move function");

				carController.Move (steer, accel, accel, handbrake);
			} 
			else 
			{
				carController.Move (0, 0, 0, 0); //car should not be allowed to move at all

			}
		}

		public void toggleCanGo()
		{
			carCanGo = !carCanGo;
            notifications.CreateNotification("Car has been allowed to move.");
			Debug.Log ("CarCanGo: " + carCanGo);
		}


		public void toggleAccelerating()
		{
			isAccelerating = !isAccelerating;
			isBraking = false;
			isReversing = false;
			Debug.Log ("isAccelerating: " + isAccelerating);
		}

		public void toggleBraking()
		{
			isBraking = !isBraking;
			isAccelerating = false;
			isReversing = false;
			Debug.Log ("isBraking: " + isBraking);
		}

		public void toggleTurningLeft()
		{
			isTurningLeft = !isTurningLeft;
			isTurningRight = false;
			Debug.Log ("isTurningLeft: " + isTurningLeft);
		}

		public void toggleTurningRight()
		{
			isTurningRight = !isTurningRight;
			isTurningLeft = false;
			Debug.Log ("isTurningRight: " + isTurningRight);
		}

		public void toggleHandBraking()
		{
			isHandBraking = !isHandBraking;
			Debug.Log ("isHandBraking: " + isHandBraking);
		}

		public void toggleReversing()
		{
			isReversing = !isReversing;
			isAccelerating = false;
			isBraking = false;
			Debug.Log ("isReversing: " + isReversing);
		}

		public void resetAll()
		{
			isAccelerating = false;
			isBraking = false;
			isReversing = false;
			isTurningLeft = false;
			isTurningRight = false;
			isHandBraking = false;
		}
	}
}
