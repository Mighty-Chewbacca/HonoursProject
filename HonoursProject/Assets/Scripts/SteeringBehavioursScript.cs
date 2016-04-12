using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Vehicles.Car
{

    public class SteeringBehavioursScript : MonoBehaviour 
    {
        //this will be the script that contains the steering behaviours
        //it will take in a decision from the action selection layer and pass out a direct command to the car's locomotion layer (i.e - accelerate now, turn left etc etc)

        public CarAIController myController;
        public NotificationsScript notifications;

	    // Use this for initialization
	    void Start () 
        {
	
	    }
	
	    // Update is called once per frame
	    void Update () 
        {
	
	    }

        public void InitiateBrakes()
        {
            myController.toggleBraking();
        }

        public void InitiateAcceleration()
        {
            myController.toggleAccelerating();
        }

        public void InitiateCoasting()
        {
            myController.toggleCoasting();
        }
    }
}
