using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScript : MonoBehaviour {

    [SerializeField] GameObject Window1;
    [SerializeField] GameObject Window2;
    [SerializeField] GameObject WindowGroup;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleWindow1()
    {
        Window1.SetActive(!Window1.activeSelf);
    }
    public void ToggleWindow2()
    {
        Window2.SetActive(!Window2.activeSelf);
    }
    public void ToggleWindowGroup()
    {
        WindowGroup.SetActive(!WindowGroup.activeSelf);
    }
}
