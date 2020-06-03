using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BorderScript : MonoBehaviour
{

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        SceneManager.LoadScene("startMenu");
    }
}
