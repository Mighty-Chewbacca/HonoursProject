using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Vehicles.Car
{

    public class ActionSelectionScript : MonoBehaviour
    {
        //this script will contain the finite state machine that will control the choices of the ai
        //this layer is the action selection layer - it does not take anything in except environmental stuff and it will pass out a decision on a behaviour down to the steering layer

        public enum Accelerationstates {stopped, accelerating, braking, coasting};
        public Accelerationstates currentState;
        public float currentMaxSpeed, currentMinSpeed;

        public SteeringBehavioursScript mySteeringBehaviourScript;
        public NotificationsScript notifications;
        public CarController carController;

        public int currentSpeedLimit;

        // Use this for initialization
        void Start()
        {
            currentSpeedLimit = (int)carController.MaxSpeed;
            currentState = Accelerationstates.stopped;
        }
	
        // Update is called once per frame
        void FixedUpdate()
        {
            //query speed and compare to the speed limit to decide on the needed action
            float currentSpeed = carController.CurrentSpeed;
            currentMaxSpeed = currentSpeedLimit / 10;
            currentMaxSpeed = currentMaxSpeed * 9;

            currentMinSpeed = currentSpeedLimit / 10;
            currentMinSpeed = currentMinSpeed * 8;

            Debug.Log("cars current speed: " + currentSpeed);

            if (currentState == Accelerationstates.braking)
            {
                if (currentSpeed < currentMaxSpeed && currentSpeed > currentMinSpeed)
                {
                    //go to coasting
                    setCoasting();
                }

                if (currentSpeed < currentMinSpeed)
                {
                    //go to accelerating
                    setAccelerating();
                }
            }
            else if (currentState == Accelerationstates.accelerating)
            {
                if (currentSpeed > currentSpeedLimit)
                {
                    //go to braking
                    setBraking();
                }

                if (currentSpeed > currentMaxSpeed)
                {
                    //switch to coasting
                    setCoasting();
                }
            }
            else if (currentState == Accelerationstates.coasting)
            {
                if (currentSpeed < currentMinSpeed)
                {
                    //go to accelerating
                    setAccelerating();
                }
                if (currentSpeed > currentMaxSpeed || currentSpeed > currentSpeedLimit)
                {
                    //go to accelerating
                    setBraking();
                }
            }
            else if (currentState == Accelerationstates.stopped)
            {
                //setAccelerating();
                StopMoving();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            SpeedLimitScript script = other.GetComponent<SpeedLimitScript>();

            if (script != null)
            {
                //the trigger does contain a speed limit script

                currentSpeedLimit = script.mySpeedLimit;
            }
        }

        void setCoasting()
        {
            currentState = Accelerationstates.coasting;
            mySteeringBehaviourScript.InitiateCoasting();
        }

        void setAccelerating()
        {
            currentState = Accelerationstates.accelerating;
            mySteeringBehaviourScript.InitiateAcceleration();
        }

        void setBraking()
        {
            currentState = Accelerationstates.braking;
            mySteeringBehaviourScript.InitiateBrakes();
        }

        public void AllowToGo()
        {
            setAccelerating();
            notifications.CreateNotification("Car has been allowed to go");
        }

        public void StopMoving()
        {
            currentState = Accelerationstates.stopped;
            mySteeringBehaviourScript.InitiateBrakes();
            notifications.CreateNotification("Car has been stopped");
        }
    }
}
