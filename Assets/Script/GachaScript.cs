using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaScript : MonoBehaviour
{
    public List<SpriteRenderer> light;
    public List<Animator> buffAnimation;
    public List<Animator> chest;
    public Text txt;
    public Text keyTxt;

    private List<int> noRepeat = new List<int>();
    [System.Serializable]
    struct ChestLoot {
        public int chestNum;
        public int effect;
        public string effectType;
        public float effectValue;
    }

    private ChestLoot[] loot = new ChestLoot[6]; 

    private bool isInvoke;
    private int i;
    private int repeatable = 0;

    private void Start()
    {
        noRepeat = new List<int>() {-1,-1,-1,-1,-1,-1};

        for (i = 0; i<chest.Count; i++) {
            chest[i].SetBool("isOpen", false);
            loot[i].chestNum = i;
            int temRand = Random.Range(1,5);
            loot[i].effect= temRand;
            GenerateChestLoot(temRand, i);
        }
        //txt.text = GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].charName;
       // keyTxt.text = "Press " + GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].tapKey.ToString();
    }

    private void Update()
    {
        if (!isInvoke)
        {
            StartCoroutine(PlayRandomLight());
        }
        txt.text = GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].charName;
        keyTxt.text = "Press " + GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].tapKey.ToString();
    }

    IEnumerator DelayedAnimation()
    {
        yield return new WaitForSeconds(1);
        buffAnimation[i].SetInteger("status", loot[i].effect);
    }

    void GenerateChestLoot(int lootNumber, int chestIndex)
    {
       if (lootNumber%2 ==0) //buff
        {

            switch (lootNumber) {
                case 2:
                    loot[chestIndex].effectType = "speed";
                    break;
                case 4:
                    loot[chestIndex].effectType = "damage";
                    break;
            }
            loot[chestIndex].effectValue = (float)System.Math.Round(Random.Range(1.0f, 1.5f), 2);
        }
        else { //debuff
            if (lootNumber == 1 || lootNumber == 3) {
                loot[chestIndex].effectType = "speed";
            }
            loot[chestIndex].effectValue = Random.Range(0.7f, 0.9f);
        }
    }

    IEnumerator PlayRandomLight() {
   
        while (!isInvoke && GameManager.Instance.countFinish != 4)
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
           
            if (Input.GetKeyDown(GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].tapKey))
            {
                chest[i].SetBool("isOpen", true);
                noRepeat[repeatable] = i;
                isInvoke = true;
                StartCoroutine(DelayedAnimation());
                yield return new WaitForSeconds(4);
                isInvoke = false;
                GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].bufftype = loot[i].effectType;
                GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].buffValue = loot[i].effectValue;
                GameManager.Instance.countFinish++;
                repeatable++;
               // txt.text = GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].charName;
               // keyTxt.text = "Press " + GameManager.Instance.playerRank[GameManager.Instance.playerFinish[repeatable % 4] - 1].tapKey.ToString();
            }

            yield return null;
        }
    }

}
