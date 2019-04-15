using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DroidFactory : Singleton<DroidFactory> {

    [SerializeField] private Droid[] availableDroids;
    [SerializeField] private Player player;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int startingDroids = 5;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;
    private bool gameStarted = false;

    private List<Droid> liveDroids = new List<Droid>();
    private Droid selectedDroid;
    private int selectedDroidIndex;

    public List<Droid> LiveDroids
    {
        get { return liveDroids; }
    }
    public Droid SelectedDroid
    {
        get { return selectedDroid;  }
    }
    private void Awake () {
        Assert.IsNotNull(availableDroids);
        Assert.IsNotNull(player);
    }

    public void SelectDroid(Droid d)
    {
        
        for (int i = 0; i < liveDroids.Count; i++)
        {
            if (liveDroids[i] == d)
            {
                selectedDroidIndex = i;
                selectedDroid = d;
                break;
            }
        }
    }

    private IEnumerator GenerateDroids()
    {
        while (true)
        {
            InstantiateDroid();
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void InstantiateDroid()
    {
        int index = Random.Range(0,availableDroids.Length);
        float x = player.transform.position.x + GenerateRange();
        float z = player.transform.position.z + GenerateRange();
        float y = player.transform.position.y+2;

        liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity));
        liveDroids[liveDroids.Count - 1].SetDroidType(index);
    }

    private float GenerateRange()
    {
        float randomNum = Random.Range(minRange, maxRange);
        int sign=0; 
        if (Random.Range(0, 10) < 5)
        {
            sign = 1;
        }
        else
        {
            sign = -1;
        }
        return randomNum*sign;
    }
    
    /*public void SetStartingDroids(int n)
    {
        startingDroids = n;
        liveDroids.Clear();
        for (int k = 0; k < startingDroids; k++)
        {
            if (k%2==0)
                liveDroids.Add(Instantiate(availableDroids[0], Vector3.zero, Quaternion.identity));
            else
                liveDroids.Add(Instantiate(availableDroids[1], Vector3.zero, Quaternion.identity));
        }
    }*/

    public int GetDroidIndex()
    {
        return selectedDroidIndex;
    }

    public void SetGameStarted(int start)
    {
        if (liveDroids.Count <= 0 && !gameStarted)
        {
            gameStarted = true;
            if (start>=0)
            startingDroids = start;
            for (int i = 0; i < startingDroids; i++)
            {
                InstantiateDroid();
            }

            //StartCoroutine(GenerateDroids());
        }
    }

    public void ResetDroids()
    {
        for (int i = 0; i < liveDroids.Count; i++)
        {
            liveDroids[i].Destroy();
        }
        liveDroids.Clear();
        //FindObjectsOfType<Droid>()
        startingDroids = 5;

        for (int k = 0; k < startingDroids; k++)
        {
            InstantiateDroid();
        }

        //StartCoroutine(GenerateDroids());
        
    }

    public bool SetDefeated(int i)
    {
        if (i >= 0 && i < liveDroids.Count) {
            liveDroids[i].Destroy();
            liveDroids.Remove(liveDroids[i]);
            startingDroids = liveDroids.Count;
            return true;
        }
        return false;
    }
}


