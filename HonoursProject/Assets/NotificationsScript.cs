using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotificationsScript : MonoBehaviour 
{

    public GameObject notificationPrefab;
    public GameObject content;
    private int temp = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void CreateNotification(string notificationtext)
    {
        GameObject notification = Instantiate(notificationPrefab);
        notification.gameObject.transform.SetParent(content.gameObject.transform);
        notification.gameObject.GetComponent<Text>().text = notificationtext += temp.ToString();
        notification.gameObject.transform.SetAsFirstSibling();
        temp++;
    }
}
