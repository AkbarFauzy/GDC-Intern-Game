using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaScript : MonoBehaviour
{
    [SerializeField] private PlayersScript[] _players;

    public List<SpriteRenderer> light;
    public Text txt;
    public Text keyTxt;

    private List<int> noRepeat = new List<int>();
    [System.Serializable]
    public struct ChestLoot {
        public Animator BuffAnimator;
        public Animator ChestAnimator;
        public int EffectNumber;
        public Buff Effect;
    }

    public ChestLoot[] loot = new ChestLoot[6]; 

    private bool isInvoke;
    private int repeatable = 0;

    private void Start()
    {
        noRepeat = new List<int>() {-1,-1,-1,-1,-1,-1};

        _players = _players.OrderBy(w => w.Position).ToArray();

        for (int i = 0; i< loot.Length; i++) {
            loot[i].ChestAnimator.SetBool("isOpen", false);
            int tempRamp = Random.Range(1, 101);
            loot[i].EffectNumber= tempRamp;
            GenerateChestLoot(tempRamp, i);
        }
        txt.text = "Player " + _players[repeatable].playerNumber.ToString();
        keyTxt.text = "Press " + _players[repeatable].tapKey.ToString();
    }

    private void Update()
    {
        if (!isInvoke && repeatable < 4)
        {
            for (int j = 0; j < light.Count; j++)
            {
                light[j].color = Color.black;
            }
            int selectedIndex = Random.Range(0, 6);
            while (noRepeat.Contains(selectedIndex))
            {
                selectedIndex = Random.Range(0, 6);
            }
            light[selectedIndex].color = Color.white;

            if (Input.GetKeyDown(_players[repeatable].tapKey))
            {
                loot[selectedIndex].ChestAnimator.SetBool("isOpen", true);
                AudioManager.Instance.PlaySound("Gacha_selected");
                noRepeat[repeatable] = selectedIndex;
                isInvoke = true;
                StartCoroutine(OnGetBuff(selectedIndex));
            }
        }
    }

    IEnumerator OnGetBuff(int index)
    {
        yield return new WaitForSeconds(1);
        loot[index].BuffAnimator.SetInteger("status", loot[index].EffectNumber);
        GameManager.Instance.SavePlayerBuff(_players[repeatable], loot[index].Effect.BuffType, loot[index].Effect.BuffValue);
        _players[repeatable].BuffHolder.SetBuff(loot[index].Effect);
        repeatable++;

        if (repeatable < 4)
        {
            txt.text ="Player " + _players[repeatable].playerNumber.ToString();
            keyTxt.text = "Press " + _players[repeatable].tapKey.ToString();
        }
        else
        {
            txt.text = "Procedd To";
            keyTxt.text = "The Next Level";
        }
        yield return new WaitForSeconds(2);
        isInvoke = false;
        GameManager.Instance.countFinish++;
    }

    void GenerateChestLoot(int lootNumber, int chestIndex)
    {
        if (lootNumber <= 70) //buff 70%
        {
            if (lootNumber <= 10) // 14.2% from 70% chance to get speed (10/100)
            {
                loot[chestIndex].Effect = new Buff(BuffType.BuffSpeed, 1.1f);
            }
            else if (lootNumber <= 15) // 7.1% from 70% chance to get damage (5/100)
            {
                loot[chestIndex].Effect = new Buff(BuffType.BuffDamage, (float)System.Math.Round(Random.Range(1.1f, 1.3f), 1));
            }
            else if(lootNumber <= 55)   // 57.1% from 70% chance to get health (40/100)
            {
                loot[chestIndex].Effect = new Buff(BuffType.BuffHealth, 100 * (int)Random.Range(1, 4));
            }
            else  //21.4% from 70% chance to get life (15/100)
            { 
                loot[chestIndex].Effect = new Buff(BuffType.Life, 1);
            }
        }
        else
        { //debuff 30%
            if (lootNumber <= 80) // 33.3% from 30% chance to get speed (10/100)
            {
                loot[chestIndex].Effect = new Buff(BuffType.DebuffSpeed, 0.9f);
            }
            else if (lootNumber <= 85) // 16.6% from 30% chance to get damage (5/100)
            {
                loot[chestIndex].Effect = new Buff(BuffType.DebuffDamage, (float)System.Math.Round(Random.Range(0.8f, 0.9f), 1));
            }
            else //50% from 30% chance to get health
            {
                loot[chestIndex].Effect = new Buff(BuffType.DebuffHealth, 100 * (int)Random.Range(1, 4));
            }
        }
    }
}
