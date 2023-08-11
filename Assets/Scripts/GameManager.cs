using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;

    public static GameManager _instance;
    private Vector3 orignVec;
    public GameObject lose;
    public GameObject win;
    public GameObject[] stars;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0) {
            orignVec = birds[0].transform.position;
            Debug.Log(orignVec);
        }
    }

    private void Start()
    {
        InitializeState();
       
    }

    private void InitializeState() {
        for (int i = 0; i < birds.Count; i++) {
            Bird bird = birds[i];
            if (i == 0)
            {
                bird.transform.position = orignVec;
                bird.enabled = true;
                bird.sp.enabled = true;
            }
            else {
                bird.enabled = false;
                bird.sp.enabled = false;
            }
        }

    }

    public void NextBird() {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                InitializeState();
            }
            else {
                // 输了
                lose.SetActive(true);
                Debug.Log("zhy -----输了");
            }
        }
        else {
            // win
            Debug.Log("zhy ------win");
            win.SetActive(true);
        }
    }

    public void ShowStars() {
        Debug.Log("zhy ----- show stars" + birds.Count);
        StartCoroutine("Show");
    }

    IEnumerator Show() {
        for (int i = 0; i < birds.Count + 1; i++) {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true);
        }
    }

    public void Replay() {
        Debug.Log("zhy ------replay");
        SceneManager.LoadScene(2);
    }

    public void Home() {
        Debug.Log("zhy ------home");
        SceneManager.LoadScene(1);

    }
}
