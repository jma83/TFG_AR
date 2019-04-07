using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureRange : MonoBehaviour {
    private bool dir = true;
    private const float factor = 0.004f;
    private float range=0;
    private Player player;
    private Vector3 auxVec;
    // Use this for initialization
    void Start () {
        player = GameManager.Instance.CurrentPlayer;
        auxVec = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
        //float aux = 0;

        if (player.CaptureRange != range)
            range = player.CaptureRange;

        if (dir)
            auxVec.Set(-range, transform.localScale.y, -range); //aux = transform.localScale.x - factor;   
        else
            auxVec.Set(range, transform.localScale.y, range); //aux = transform.localScale.x + factor;


        


        if (transform.localScale.x <= -(range-0.05f))
            dir = false;
        if (transform.localScale.x >= (range-0.05f))
            dir = true;

        transform.localScale = Vector3.Lerp(transform.localScale, auxVec, Time.deltaTime);
        //Debug.Log("dir: " + dir + " localscale.x: " +transform.localScale.x + " range: "+ range);
        //transform.localScale = new Vector3(aux, transform.localScale.y, aux);
        //Wait();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    //collision manager
    public void OnTriggerEnter(Collider col)
    {
        //print(droids.Length);
        if (col.gameObject.tag == "droid")
        {
            Droid[] droids = FindObjectsOfType<Droid>();

            foreach (Droid d in droids)
            {
                if (d.gameObject == col.gameObject)
                {
                    d.SetCaptureRange(true);
                }
            }
        }
        else if (col.gameObject.tag == "item")
        {
            ItemPickup[] item = FindObjectsOfType<ItemPickup>();

            foreach (ItemPickup i in item)
            {
                if (i.gameObject == col.gameObject)
                {
                    i.SetCaptureRange(true);
                }
            }
        }
    }

    public void SetEntitiesCaptureRange(bool b)
    {
        MapEntity[] mapEntities = FindObjectsOfType<MapEntity>();

        foreach (MapEntity m in mapEntities)
        {
            m.SetFixedCaptureRange(b);        
        }
    }
}
