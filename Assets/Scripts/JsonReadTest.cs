using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class JsonReadTest : MonoBehaviour
{
    public List<StopRecode> stopRecodes;

    private void Start()
    {
        List<StopRecode> stopRecodes = new List<StopRecode>();

        var inputString = Resources.LoadAll<TextAsset>("Json/StopRecodes");

        foreach (var item in inputString)
        {
            StopRecode inputJson = JsonConvert.DeserializeObject<StopRecode>(item.text);
            stopRecodes.Add(inputJson);
        }

        GetComponent<Text>().text = stopRecodes[0].stopName + "\n" + stopRecodes[1].stopName + "\n" + stopRecodes[2].stopName; 
    }
}
