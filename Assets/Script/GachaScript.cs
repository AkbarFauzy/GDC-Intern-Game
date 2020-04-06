using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaScript : MonoBehaviour
{
    public List<SpriteRenderer> light;
    public List<Animator> buffAnimation;
    public List<GameObject> playerQueue;
    public List<Animator> chest;

    private List<int> noRepeat = new List<int>();
    [System.Serializable]
    struct ChestLoot {
        public int chestNum;
        public int buff;
    }

    private ChestLoot[] loot = new ChestLoot[6]; 

    private bool isInvoke;
    private int i;
    private int repeatable = 0;

    private void Start()
    {
            noRepeat = new List<int>() {-1,-1,-1,-1,-1,-1 };

        for (int i = 0; i<chest.Count; i++) {
            chest[i].SetBool("isOpen", false);
            loot[i].chestNum = i;
            int temRand = Random.Range(1,4);
            loot[i].buff = temRand;
            GenerateChestLoot(temRand, i);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown("a"))
        {
            chest[i].SetBool("isOpen", true);
            noRepeat[repeatable] = i;
            repeatable++;
            isInvoke = true;
            StartCoroutine(DelayedAnimation());
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

    IEnumerator DelayedAnimation()
    {
        yield return new WaitForSeconds(1);
        buffAnimation[i].SetInteger("status", loot[i].buff);
    }

    void GenerateChestLoot(int lootNumber, int chestIndex)
    {
        if (lootNumber == 1)
        {
           // buffAnimation[chestIndex].runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("item");
        }
        /*else if () {

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
            while (noRepeat.Contains(i))
            {
                i = Random.Range(0, 6);
            }
            light[i].color = Color.white;

            yield return null;
        }
    }

}
