using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour
{
    public static bool paused = false;
    public void isClicked()
    {
        paused = true;
    }
}
