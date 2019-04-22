using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPuzzle : MonoBehaviour {

    [SerializeField] Text timerText;
    [SerializeField] Toggle ARToggle;
    [SerializeField] GameObject mazeGameObject;
    [SerializeField] GameObject plane;
    [SerializeField] GameObject topPlane;
    private PuzzleManager puzzleManager;

    // Use this for initialization
    void Start () {
        puzzleManager = PuzzleManager.Instance;

    }
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Timer: " + puzzleManager.GetTime().ToString();
    }

    public void ToggleAumentedReality()
    {
        if (ARToggle.isOn)
        {
            mazeGameObject.transform.position = new Vector3(0, 0, 0);
            mazeGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            plane.GetComponent<MeshRenderer>().enabled = false;
            topPlane.transform.rotation = Quaternion.Euler(0, 0, 0);

            int size = mazeGameObject.GetComponentsInChildren<MeshRenderer>().Length;
            Debug.Log("on: " + size);
            for (int i = 0; i <= size; i++)
            {
                mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = false;
                if (i != size)
                    mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = false;
            }
        }
        else
        {
            mazeGameObject.transform.position = new Vector3(0, 0, 15.7f);
            mazeGameObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
            topPlane.transform.rotation = Quaternion.Euler(-90f, 0, 0);
            plane.GetComponent<MeshRenderer>().enabled = true;

            int size = mazeGameObject.GetComponentsInChildren<MeshRenderer>().Length;
            Debug.Log("off: " + size);
            for (int i = 0; i <= size; i++)
            {
                mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = true;
                if (i!=size)
                mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
            }

        }

    }
}
