using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaScript : MonoBehaviour
{
    public List<SpriteRenderer> light;
    public List<GameObject> playerQueue;
    public List<Animator> chest;

    [System.Serializable]
    struct ChestLoot {
        public int chestNum;
        public int buff;
    }

    private ChestLoot[] loot = new ChestLoot[6]; 

    private bool isInvoke;
    private int i;

    private void Start()
    {
        for (int i = 0; i<chest.Count; i++) {
            chest[i].SetBool("isOpen", false);
            loot[i].chestNum = i;
            int temRand = Random.Range(0, 10);
            loot[i].buff = temRand;
            GenerateChestLoot();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            chest[i].SetBool("isOpen", true);
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

    void GenerateChestLoot()
    {
       /* if () {

        } else if () {

        } else if () {

        } else if () {

        } else if () { 
        
        } else if () { 
        
        } else if () {

        } else if () {

        } else if () {

        }
        */

    }

    IEnumerator PlayRandomLight() {
   
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
