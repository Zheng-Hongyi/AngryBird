using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    
    public void Show() {
        Debug.Log("zhy ----- to show stars");
        GameManager._instance.ShowStars();
    }
}
