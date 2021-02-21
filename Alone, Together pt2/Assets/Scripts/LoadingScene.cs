using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Text fadeText;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(fade());
        StartCoroutine(LoadASyncOperation());
    }

    IEnumerator LoadASyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        //Doesnt work for shit
        // for (float i = 0; i <= 5; i += Time.deltaTime) //1 second fade
        // {
        //     fadeText.color = new Color(1,1,1,i);
        //     yield return null;
        // }
        //loading scene in background        
        // //LOADING BAR
        // _progress.fillAmount = gameLevel.progress;
        // yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
    }

}

