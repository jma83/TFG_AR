using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARGameSceneManager : Singleton<ARGameSceneManager>
{
    public Animator transtionAnim;

    public abstract void playerTapped(GameObject player);
    public abstract void ChangeScene(GameObject droid,int i);}
