using UnityEngine;
using TMPro;
using System;
using System.Collections;

// The following code is used to update a TMP every second with the current time in
// Vienna, Austria. To figure out how to do this, I used online resources and ChatGPT to
// understand what tools exist to get the time and how to use them

public class ViennaClock : MonoBehaviour
{
    public TMP_Text clockText;

    TimeZoneInfo viennaZone;

    void Start()
    {
        // Windows timezone name
        viennaZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        StartCoroutine(UpdateClock());
    }

    IEnumerator UpdateClock()
    {
        while (true)
        {
            DateTime utcTime = DateTime.UtcNow;
            DateTime viennaTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, viennaZone);

            clockText.text = "Local Time: " + viennaTime.ToString("HH:mm:ss");

            yield return new WaitForSeconds(1f);
        }
    }
}