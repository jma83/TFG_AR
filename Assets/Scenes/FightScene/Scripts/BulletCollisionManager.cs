using UnityEngine;
using System.Collections;

public class BulletCollisionManager : MonoBehaviour
{
    private int damage;
    private string owner="Player";
    private GameObject explosion;
    

    // Use this for initialization
    void Start()
    {
        damage = 5;
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
            
            if ((col.gameObject.tag == "Player" && col.gameObject.tag!= owner) || (col.gameObject.tag == "enemy" && col.gameObject.tag != owner))
            {

                Debug.Log("HIT: " + col.gameObject.tag);
                Debug.Log("OWNER: " +owner);

                FightEntity fe = col.gameObject.GetComponent<FightEntity>();
                fe.DealDamage(damage);
                if (col.gameObject.tag == "Player")
                {
                    if (fe.GetHP() <= 0)
                    {
                        GameOver();
                    }
                }

                if (col.gameObject.tag == "enemy")
                {
                    if (fe.GetHP() <= 0)
                    {
                        Destroy(col.gameObject);
                        CheckWin();
                        explosion = Instantiate(Resources.Load("FightScene/FlareMobile", typeof(GameObject))) as GameObject;

                    }
                    else
                    {
                        explosion = Instantiate(Resources.Load("FightScene/ExplosionMobile", typeof(GameObject))) as GameObject;
                    }

                    explosion.transform.position = transform.position;                        
                    Destroy(explosion, 2);
                    

                }

                Destroy(gameObject);

            }
        }


        /*if (col.gameObject.tag == "enemy")
        {

            //FightSceneManager.Instance.ChangeScene(null,0);
            GameObject enemy = Instantiate(Resources.Load("FightScene/enemy1", typeof(GameObject))) as GameObject;
            GameObject enemy1 = Instantiate(Resources.Load("FightScene/enemy2", typeof(GameObject))) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("FightScene/enemy3", typeof(GameObject))) as GameObject;
            GameObject enemy3 = Instantiate(Resources.Load("FightScene/enemy4", typeof(GameObject))) as GameObject;

        }*/

    }

    public bool CheckWin()
    {
        GameObject[] gmObjects = GameObject.FindGameObjectsWithTag("enemy");

        if (gmObjects != null)
            if (gmObjects.Length <= 0)
            {
                //WindowAlert.Instance.SetActiveAlert(); ENEMIGOS DERROTADOS, pulsa OK para volver al mapa
                WindowAlert window = WindowAlert.Instance;
                window.CreateConfirmWindow("BIEN HECHO! ENEMIGOS DERROTADOS, pulsa OK para volver al mapa", false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
                window.SetActiveAlert();
                return true;
            }
        return false;
    }

    public void GameOver()
    {
        WindowAlert window = WindowAlert.Instance;
        window.CreateConfirmWindow("HAS SIDO DERROTADO, pulsa OK para volver al mapa",false, FightSceneManager.Instance.ChangeScene); //HAS SIDO DERROTADO, pulsa OK para volver al mapa
        window.SetActiveAlert();
    }

    public void SetDamage(int i)
    {
        damage = i;
    }

    public void SetOwner(string i)
    {
        owner = i;
    }

    public int GetDamage()
    {
        return damage;
    }

    public string GetOwner()
    {
        return owner;
    }


}