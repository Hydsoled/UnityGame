using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 ballPos;
    private bool isBeingHeld = false;
    private static bool isEqual = false;
    private static Vector3 pos;
    private static string tagg;
    public static int score = 0;

    void Update()
    {
        if (isBeingHeld)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            if (mousePos.x > bottomLeftScreenPoint.x && mousePos.x < topRightScreenPoint.x && mousePos.y > bottomLeftScreenPoint.y)
                gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
            else
            {
                isBeingHeld = false; gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        if (isEqual)
        {
            if (tagg != "2048")
            Instantiate(SpawnBalls.ballSpn[tagg], pos, Quaternion.identity);
            isEqual = false;
            if (tagg == "2048") score += 2048;
            if (tagg == "1024") score += 1024;
            if (tagg == "512") score += 512;
            if (tagg == "256") score += 256;
            if (tagg == "128") score += 128;
            if (tagg == "64") score += 64;
            if (tagg == "32") score += 32;
            if (tagg == "16") score += 16;
            if (tagg == "8") score += 8;
            if (tagg == "4") score += 4;
            if (tagg == "2") score += 2;
            if (PlayerPrefs.GetInt("Highscore") < score)
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
        if (gameObject.transform.localPosition.y < Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y-0.1f,0.0f);
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ballPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ballPos.z = 0;
            isBeingHeld = true;
        }
    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0);
        isBeingHeld = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == gameObject.tag)
        {
            Destroy(other.gameObject);
            isEqual = true;
            tagg = gameObject.tag;
            pos = gameObject.transform.position;
            Destroy(gameObject);
        }
        if (other.tag == "border")
        {
            SceneManager.LoadScene("startMenu");
        }
    }
}
