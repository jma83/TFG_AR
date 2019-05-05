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
    [SerializeField] GameObject parent2;
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject light1;
    [SerializeField] Text textdebug;
    Vector3 dir;

    private PuzzleManager puzzleManager;

    // Use this for initialization
    void Start () {
        puzzleManager = PuzzleManager.Instance;
        plane.GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Timer: " + puzzleManager.GetTime().ToString();
        //textdebug.text = "x:" + GameObject.FindGameObjectWithTag("Player").transform.position.x + "\n y:" + GameObject.FindGameObjectWithTag("Player").transform.position.y + "\n z:" + GameObject.FindGameObjectWithTag("Player").transform.position.z;
        if (puzzleManager.GetTime()<=10)
            timerText.color = new Color32(242, 0, 0, 100);
        if (!ARToggle.isOn && puzzleManager.GetTime() > 0)
        {
            dir = new Vector3(0, -0.5f, 0);

            /*float angle = Input.acceleration.y * 180 / Mathf.PI;
            textdebug.text = "x:" + Input.acceleration.x + "\n y:" + Input.acceleration.y + "\n z:" + Input.acceleration.z; //Input.gyro
            parent2.transform.rotation = Quaternion.Euler(0, 0, angle);
            //plane.transform.Rotate(0, 0, angle);
            topPlane.transform.rotation = Quaternion.Euler(0, 0, angle);
            camera1.transform.rotation = Quaternion.Euler(0, 0, angle);
            light1.transform.rotation = Quaternion.Euler(0, 0, angle);*/

            /*Vector3 tilt = Input.acceleration;

            tilt = Quaternion.Euler(90, 0, 0) * tilt;

            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().AddForce(tilt*2);*/
            //textdebug.text = "x:" + Input.acceleration.x + "\n y:" + Input.acceleration.y + "\n z:" + Input.acceleration.z; //Input.gyro

            dir.x = Input.acceleration.x ;
            dir.z = Input.acceleration.y;

            // clamp acceleration vector to unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            // Make it move 10 meters per second instead of 10 meters per frame...
            dir *= Time.deltaTime;

            // Move object
            if (dir!=Vector3.zero)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = (dir * 1000f);

        }

    }

    public void ToggleAumentedReality()
    {
        GameObject auxGO;

        if (ARToggle.isOn)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().useGravity = true;

            camera1.transform.position = new Vector3(0, 0, 0);
            camera1.transform.rotation = Quaternion.Euler(0, 0, 0);

            plane.GetComponent<MeshRenderer>().enabled = false;

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("puzzleObject").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("puzzleObject")[i];
                auxGO.transform.SetParent(mazeGameObject.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = false;
                if (auxGO.GetComponent<Collider>() != null)
                    auxGO.GetComponent<Collider>().enabled = false;
                if (auxGO.GetComponent<SkinnedMeshRenderer>() != null)
                    auxGO.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Finish").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("Finish")[i];
                auxGO.transform.SetParent(mazeGameObject.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = false;
                if (auxGO.GetComponent<Collider>() != null)
                    auxGO.GetComponent<Collider>().enabled = false;
            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("Player")[i];
                auxGO.transform.SetParent(mazeGameObject.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = false;
                if (auxGO.GetComponent<Collider>() != null)
                    auxGO.GetComponent<Collider>().enabled = false;
            }
        }
        else{

            puzzleManager.SetTime(puzzleManager.GetTime() / 2);
            timerText.color = new Color32(242, 0, 0, 100);

            camera1.transform.position = new Vector3(0, 20, 0);
            camera1.transform.rotation = Quaternion.Euler(90, 0, 0);
            plane.GetComponent<MeshRenderer>().enabled = true;
            

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("puzzleObject").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("puzzleObject")[i];
                auxGO.transform.SetParent(parent2.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = true;
                if (auxGO.GetComponent<Collider>() != null)
                    auxGO.GetComponent<Collider>().enabled = true;
                if (auxGO.GetComponent<SkinnedMeshRenderer>() != null)
                    auxGO.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Finish").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("Finish")[i];
                auxGO.transform.SetParent(parent2.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = true;
                if (auxGO.GetComponent<Collider>()!=null)
                    auxGO.GetComponent<Collider>().enabled = true;
            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
            {
                auxGO = GameObject.FindGameObjectsWithTag("Player")[i];
                auxGO.transform.SetParent(parent2.transform);
                if (auxGO.GetComponent<MeshRenderer>() != null)
                    auxGO.GetComponent<MeshRenderer>().enabled = true;
                if (auxGO.GetComponent<Collider>() != null)
                    auxGO.GetComponent<Collider>().enabled = true;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<SlidingSphere>().ResetPos();

        }
        PuzzleManager.Instance.SetAR(ARToggle.isOn);
    }

}
