using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARGameSceneManager : MonoBehaviour {

    public abstract void playerTapped(GameObject player);
    public abstract void droidTapped(GameObject droid);
}
