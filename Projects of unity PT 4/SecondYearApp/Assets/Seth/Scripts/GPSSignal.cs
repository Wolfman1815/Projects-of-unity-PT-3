using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSSignal : MonoBehaviour {

    public static GPSSignal Instance { set; get; }

    public float longitude;
    public float latitude;



    private void start()
    {

        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Start());


    }



	IEnumerator Start()
    {

        //We're checking to see if the user on the phone
        //has location services enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        //Start the service before we query the location
        Input.location.Start();

        //We're waiting until the service initializes
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {

            yield return new WaitForSeconds(1);
            maxWait--;

        }

        //Checking to see if the service didnt intialize in 20 seconds
        if(maxWait < 1)
        {
            print ("They Have Timed Out");
            yield break;

        }


        //Checking to see if the connection has failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {

            print("Unable to determine the device's location");
            yield break;

        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        yield break;

        /*else
        {

            //Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

        }

        //Stop Service if there is no need to query location updates continuously
        Input.location.Stop();*/


    }



}
