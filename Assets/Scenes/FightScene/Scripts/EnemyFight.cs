using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{

    private int numEnemies;

    // Use this for initialization
    void Start()
    {
        numEnemies = GameObject.FindGameObjectsWithTag("Player").Length;
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    }

    public int getNumEnemies()
    {
        return numEnemies; 
    }

    public void setNumEnemies(int i)
    {
        numEnemies = i;
    }

    IEnumerator Move()
    {


        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
    }
}