using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SpawnBalls : MonoBehaviour
{
    GameObject bottom;
    GameObject left;
    GameObject right;
    public List<GameObject> ball;
    public static Dictionary<string,GameObject> ballSpn = new Dictionary<string, GameObject>();
    float nextBallTime = 0.0f;
    SpriteRenderer rendererr;
    GameObject border;
    public Sprite sprite;
    float speed = 1.0f;
    long timer = 10;

    void Awake()
    {
        bottom = new GameObject("Bottom");
        left = new GameObject("Left");
        right = new GameObject("Right");

        border = new GameObject("Border");
        rendererr = border.AddComponent<SpriteRenderer>();
    }


    void Start()
    {
        if (ballSpn.Count == 0)
        {
            for (int i = 0; i < ball.Count - 1; i++)
            {
                ballSpn.Add(ball[i].tag, ball[i + 1]);
            }
        }
        CreateScreenColliders();
        BallScript.score = 0;
    }

    void Update()
    {
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        if (Time.time - nextBallTime > speed)
        {
            Instantiate(ball[Random.Range(0,3)], new Vector3(Random.Range(-topRightScreenPoint.x/1.2f, topRightScreenPoint.x/1.2f), topRightScreenPoint.y/10f), Quaternion.identity);
            nextBallTime = Time.time;
        }
        if (Time.time - timer > 200 && speed > 0.9f)
        {
            timer = (long)Time.time; speed -= 0.02f;
        }
    }
        


    void CreateScreenColliders()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        // Create bottom collider
        BoxCollider2D collider = bottom.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.3f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        //** Bottom needs to account for collider size
        bottom.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - collider.size.y, 0f);


        // Create left collider
        collider = left.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.3f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y)/1.3f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        //** Left needs to account for collider size
        left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);


        // Create right collider
        collider = right.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.3f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y)/1.3f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);

        rendererr.sprite = sprite;
        rendererr.sortingLayerName = "menu";
        border.transform.localScale = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.3f, 0f);
        border.transform.position = new Vector3(0f, topRightScreenPoint.y / 3.0f, 0.0f);
        collider = border.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(1.0f, 1.0f, 0f);
        collider.isTrigger = true;
        border.AddComponent<BorderScript>();
        border.tag = "border";
    }
}
 