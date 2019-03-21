using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager> {

    private AsyncOperation sceneAsync;

	public void GoToScene(string sceneName, List<GameObject> objectsToMove)
    {
        StartCoroutine(LoadScene(sceneName, objectsToMove));
    }

    private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
    {
        SceneManager.LoadSceneAsync(sceneName);

        SceneManager.sceneLoaded += (newScene, mode) => {
            SceneManager.SetActiveScene(newScene);
        };

        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
        if (objectsToMove != null)
        {
            foreach (GameObject obj in objectsToMove)
            {
                if (obj!=null)
                SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
            }
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

        yield return null;
    }

}
