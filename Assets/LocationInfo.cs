using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfo : MonoBehaviour {

    [SerializeField] Text txt;
    private string str;
    private StartPuzzle startPuzzle;

    void Update () {
        if (txt.text != null && txt.text != "" && str == null && gameObject.activeSelf == true && Time.realtimeSinceStartup > 2)
        {
            str = txt.text;
            StartPuzzle[] sp = FindObjectsOfType<StartPuzzle>();
            for (int i = 0; i < sp.Length; i++)
            {
                if (sp[i].gameObject.transform.localPosition == gameObject.transform.localPosition)
                {
                    sp[i].SetLocationInfo(str);
                    startPuzzle = sp[i];
                    break;
                }
            }
            
        }

    }

    public StartPuzzle GetPuzzle()
    {
        return startPuzzle;
    }

    public string GetLocationInfo()
    {
        return str;
    }
}
