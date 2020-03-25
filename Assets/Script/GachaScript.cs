using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaScript : MonoBehaviour
{
    public List<SpriteRenderer> light;
    public List<GameObject> playerQueue;

    private bool isInvoke;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            isInvoke = true;
        }
        if (Input.GetKeyDown("s"))
        {
            isInvoke = false;
        }
        if (!isInvoke)
        {
            StartCoroutine(PlayRandomLight());
        }

    }

    IEnumerator PlayRandomLight() {
        int i;
        while (!isInvoke)
        {
            for (int j = 0; j < light.Count; j++)
            {
                light[j].color = Color.black;
            }
            i = Random.Range(0, 6);

            light[i].color = Color.white;

            yield return null;
        }
    }

}
