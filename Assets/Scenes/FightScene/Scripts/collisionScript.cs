using UnityEngine;
using System.Collections;

public class collisionScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    //for this to work both need colliders, one must have rigid body (spaceship) the other must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "bullet")
        {
            GameObject explosion = Instantiate(Resources.Load("FightScene/FlareMobile", typeof(GameObject))) as GameObject;
            explosion.transform.position = transform.position;
            Destroy(col.gameObject);
            Destroy(explosion, 2);
            Destroy(gameObject);
        }


        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            FightSceneManager.Instance.ChangeScene(null);
            /*GameObject enemy = Instantiate(Resources.Load("FightScene/enemy1", typeof(GameObject))) as GameObject;
            GameObject enemy1 = Instantiate(Resources.Load("FightScene/enemy2", typeof(GameObject))) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("FightScene/enemy3", typeof(GameObject))) as GameObject;
            GameObject enemy3 = Instantiate(Resources.Load("FightScene/enemy4", typeof(GameObject))) as GameObject;*/

        }
        
    }

}