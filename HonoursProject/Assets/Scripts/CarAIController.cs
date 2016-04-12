using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityStandardAssets.CrossPlatformInput;

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
        public bool isCoasting = false;

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
            Debug.Log("fixed update called in caraicontroller");
			//this is where the move method will be called on the car - it will be called based upon the bools held to control the car
       
				int steer, accel, handbrake, footbrake;

                if (isAccelerating)
                {
                    accel = 1;
                    footbrake = 0;
                }
                else if (isBraking)
                {
                    accel = 0;
                    footbrake = -1;
                }
                else if (isCoasting)
                {
                    accel = 0;
                    footbrake = 0;
                }
				else
				{
					accel = 0;
                    footbrake = 0;
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

                Debug.Log("Calling move with " + steer + " " + accel + " " + footbrake + " " + handbrake);
				carController.Move (steer, accel, footbrake, handbrake);
		}

		public void toggleCanGo()
		{
			carCanGo = !carCanGo;

            if (carCanGo)
            notifications.CreateNotification("Car is now allowed to move.");
            else
                notifications.CreateNotification("Car is now not allowed to move");
            Debug.Log ("CarCanGo: " + carCanGo);
		}


		public void toggleAccelerating()
		{
			isAccelerating = !isAccelerating;
			isBraking = false;
            isCoasting = false;

            if (isAccelerating)
                notifications.CreateNotification("Car is now accelerating");
            else
                notifications.CreateNotification("Car is no longer accelerating");
            Debug.Log ("isAccelerating: " + isAccelerating);
		}

		public void toggleBraking()
		{
			isBraking = !isBraking;
			isAccelerating = false;
            isCoasting = false;

            if (isBraking)
                notifications.CreateNotification("Car is now braking");
            else
                notifications.CreateNotification("Car is no longer braking");
            Debug.Log ("isBraking: " + isBraking);
		}

		public void toggleTurningLeft()
		{
			isTurningLeft = !isTurningLeft;
			isTurningRight = false;

            if (isTurningLeft)
                notifications.CreateNotification("Car is now turning left");
            else
                notifications.CreateNotification("Car is no longer turning left");
            Debug.Log ("isTurningLeft: " + isTurningLeft);
		}

		public void toggleTurningRight()
		{
			isTurningRight = !isTurningRight;
			isTurningLeft = false;

            if (isTurningRight)
                notifications.CreateNotification("Car is now turning right");
            else
                notifications.CreateNotification("Car is no longer turning right");
            Debug.Log ("isTurningRight: " + isTurningRight);
		}

		public void toggleHandBraking()
		{
			isHandBraking = !isHandBraking;

            if (isHandBraking)
                notifications.CreateNotification("Car has engaged handbrake");
            else
                notifications.CreateNotification("Car has disengaged handbrake");
            Debug.Log ("isHandBraking: " + isHandBraking);
		}

        public void toggleCoasting()
        {
            isCoasting = !isCoasting;
            isAccelerating = false;
            isBraking = false;

            if (isCoasting)
                notifications.CreateNotification("Car has begun coasting");
            else
                notifications.CreateNotification("Car has stopped coasting");
            Debug.Log ("isCoasting: " + isCoasting);
        }

		public void resetAll()
		{
			isAccelerating = false;
			isBraking = false;
			isTurningLeft = false;
			isTurningRight = false;
			isHandBraking = false;
            carCanGo = false;
            isCoasting = false;

            notifications.CreateNotification("All actions have been reset to false.");
        }
	}
}
