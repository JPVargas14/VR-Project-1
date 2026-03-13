using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class WeatherAPI : MonoBehaviour
{
    public GameObject weatherTextObject;

    // add your personal API key after APPID= and before &units=
    // current lat and lon are for Vienna, Austria
    string url = "https://api.openweathermap.org/data/2.5/weather?lat=48.2082&lon=16.3738&APPID=204b1b0b206dadcc466f765d1d386fdf&units=metric";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello");

        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void GetDataFromWeb()
    {

        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                // this code will NOT fail gracefully, so make sure you have
                // your API key before running or you will get an error

                // grab the current temperature and simplify it if needed
                int startTemp = webRequest.downloadHandler.text.IndexOf("temp", 0);
                int endTemp = webRequest.downloadHandler.text.IndexOf(",", startTemp);
                double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp + 6, (endTemp - startTemp - 6)));
                int easyTempF = Mathf.RoundToInt((float)tempF);
                Debug.Log("integer temperature is " + easyTempF.ToString());
                int startConditions = webRequest.downloadHandler.text.IndexOf("main", 0);
                int endConditions = webRequest.downloadHandler.text.IndexOf(",", startConditions);
                string conditions = webRequest.downloadHandler.text.Substring(startConditions + 7, (endConditions - startConditions - 8));
                Debug.Log(conditions);

                weatherTextObject.GetComponent<TextMeshPro>().text = "" + easyTempF.ToString() + "°C\n" + conditions;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
