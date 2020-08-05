using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetScript : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.ResetGame();
    }
}
