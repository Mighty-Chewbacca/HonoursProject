using UnityEngine;
using System.Collections;

public class SpeedLimitScript : MonoBehaviour 
{
    //this script simply holds the value for this colliders speed limit
    public int mySpeedLimit;
    public NotificationsScript notifications;
	// Use this for initialization
	void Start ()
    {
        notifications = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotificationsScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "ColliderBottom")
        {
            notifications.CreateNotification("Speed Limit Zone Entered - " + mySpeedLimit);
        }
    }
}
