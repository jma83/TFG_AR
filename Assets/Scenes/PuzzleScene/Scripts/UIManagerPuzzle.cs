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
            int size2 = mazeGameObject.GetComponentsInChildren<Collider>().Length;
            int aux;
            if (size2 > size)
            {
                aux = size;
                size = size2;
                size2 = aux;
                for (int i = 0; i < size; i++)
                {
                    mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = true;
                    if (i < size2)
                        mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (i < size2)
                        mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = true;
                    mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                }
            }
        }
        else{
            mazeGameObject.transform.position = new Vector3(0, 0, 20f);
            mazeGameObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
            topPlane.transform.rotation = Quaternion.Euler(-90f, 0, 0);
            plane.GetComponent<MeshRenderer>().enabled = true;

            int size = mazeGameObject.GetComponentsInChildren<MeshRenderer>().Length;
            int size2 = mazeGameObject.GetComponentsInChildren<Collider>().Length;
            int aux;
            if (size2 > size)
            {
                aux = size;
                size = size2;
                size2 = aux;
                for (int i = 0; i < size; i++)
                {
                    mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = true;
                    if (i < size2)
                    mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (i < size2)
                        mazeGameObject.GetComponentsInChildren<Collider>()[i].enabled = true;
                    mazeGameObject.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                }
            }
            Debug.Log("off: " + size);
            

        }
        PuzzleManager.Instance.SetAR(ARToggle.isOn);
    }

}
