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
            InstantiateDroid(-1);
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void InstantiateDroid(int j)
    {
        int index = j;
        /*Debug.Log("(availableDroids.Length * 4): " + (availableDroids.Length * 4));
        Debug.Log("(availableDroids.Length * 4) - 6: " + (availableDroids.Length * 4 - 6));
        Debug.Log("(availableDroids.Length * 4) - 11: " + (availableDroids.Length * 4 - 11));*/
        if (index == -1)
        {

            int fixedsize= availableDroids.Length * 4;
            index = Random.Range(0, availableDroids.Length*4);
            if (index < (fixedsize) && index >= (fixedsize - (fixedsize/2)))
            {
                index = 0;
            }else if (index < (fixedsize - (fixedsize / 2)) && index >= (fixedsize - (fixedsize-1)))
            {
                index = 1;
            }
            else if (index == 0)
            {
                index = 2;
            }
        }

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

    public void SetGameStarted(int start,int[] types)
    {
        if (liveDroids.Count <= 0 && !gameStarted)
        {
            gameStarted = true;
            if (start>=0)
            startingDroids = start;
            for (int i = 0; i < startingDroids; i++)
            {
                if (start!=-1)
                InstantiateDroid(types[i]);
                else
                InstantiateDroid(-1);
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
            InstantiateDroid(-1);
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


