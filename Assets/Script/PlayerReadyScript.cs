using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyScript : MonoBehaviour
{
    public TextMesh t1;
    public TextMesh t2;
    public TextMesh t3;
    public TextMesh t4;

    private bool r1;
    private bool r2;
    private bool r3;
    private bool r4;
    private bool allready;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            t1.text = "READY!";
            r1 = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            t2.text = "READY!";
            r2 = true;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            t3.text = "READY!";
            r3 = true;
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            t4.text = "READY!";
            r4 = true;
        }

        if (r1 && r2 && r3 && r4) {
            r1 = false;
            GameManager.Instance.BeginGame();
        }

    }
}
