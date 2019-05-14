using UnityEngine;
using System.Collections;

public class BulletCollisionManager : MonoBehaviour
{
    private int damage;
    private string owner;
    private GameObject explosion;
    

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
        if (damage == 0)
            damage = 5;
        if (col.gameObject.tag != "bullet")
        {
            
            if ((col.gameObject.tag == "Player" && col.gameObject.tag!= owner) || (col.gameObject.tag == "enemy" && col.gameObject.tag != owner))
            {

                //Debug.Log("HIT: " + col.gameObject.tag);
                //Debug.Log("OWNER: " +owner);

                FightEntity fe = col.gameObject.GetComponent<FightEntity>();
                fe.DealDamage(damage);
                if (col.gameObject.tag == "Player")
                {
                    if (fe.GetHP() <= 0)
                    {
                        EnemyFightManager.Instance.GameOver();
                    }
                }

                if (col.gameObject.tag == "enemy")
                {
                    if (fe.GetHP() <= 0)
                    {
                        Destroy(col.gameObject,0.5f);
                        EnemyFight ef = (EnemyFight)fe;
                        EnemyFightManager.Instance.AddTotalXP(ef.GetXP());
                        EnemyFightManager.Instance.CheckWin();
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