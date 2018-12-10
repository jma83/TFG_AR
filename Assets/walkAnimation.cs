using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour {
    [SerializeField] private Animation walk;
    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        Walk();
    }
    void Walk()
    {
        if (transform.hasChanged)
        {
            walk.Play();
            StartCoroutine(Wait());
        }
        else if (!transform.hasChanged)
        {
            walk.Stop();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        transform.hasChanged = false;
    }

}
