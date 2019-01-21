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

    private List<Droid> liveDroids = new List<Droid>();
    private Droid selectedDroid;

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

    void Start()
    {
        for (int i = 0; i < startingDroids; i++)
        {
            InstantiateDroid();
        }

        StartCoroutine(GenerateDroids());
    }

    public void DroidWasSelected(Droid d)
    {
        selectedDroid = d;
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

}
