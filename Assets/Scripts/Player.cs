using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum player_ability
{
    force,
    intellect,
    mana
}

public class Player
{
    // Player instance
    private static Player instance;
    
    public static Player Instance
    {
        get
        {
            if (null == instance)
                instance = new Player();
            return instance;
        }
    }

    private int health; // 0 ~ 5
    private int mental; // 0 ~ 5
    private int force;
    private int intellect;
    private int mana;
    public PlayerManager gmr;

    private static List<Pocket> entirePocket = new List<Pocket>();
    private static List<Ability> abilityPocket = new List<Ability>();
    private static List<Item> itemPocket = new List<Item>();
    private static List<State> statePocket = new List<State>();

    private Pocket[] entirePocketSort;
    private Ability[] abilityPocketSort;
    private Item[] itemPocketSort;
    private State[] statePocketSort;
    public bool sortbytime = true;
    public bool playerReset = false;

    public Player()
    {
        gmr = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        health = PlayerPrefs.GetInt("Health") == 0 ? 5 : PlayerPrefs.GetInt("Health");
        mental = PlayerPrefs.GetInt("Mental") == 0 ? 5 : PlayerPrefs.GetInt("Mental");
        gmr.imgSet(health, mental);
        
        force = PlayerPrefs.GetInt("Force") == 0 ? 2 : PlayerPrefs.GetInt("Force");
        intellect = PlayerPrefs.GetInt("Intellect") == 0 ? 2 : PlayerPrefs.GetInt("Intellect");
        mana = PlayerPrefs.GetInt("Mana") == 0 ? 2 : PlayerPrefs.GetInt("Mana");
        gmr.changeability_amount(player_ability.force, force);
        gmr.changeability_amount(player_ability.intellect, intellect);
        gmr.changeability_amount(player_ability.mana, mana);
    }
    public void ResetPlayer()
    {
        gmr = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        Debug.Log("HI");
        health = 5;
        mental = 5;
        gmr.imgSet(health, mental);
        force = 2;
        intellect = 2;
        mana = 2;
        gmr.changeability_amount(player_ability.force, force);
        gmr.changeability_amount(player_ability.intellect, intellect);
        gmr.changeability_amount(player_ability.mana, mana);
    }

    public void HealthChange(int value)
    {
        if (health > 0)
        {
            this.health += value;
            if (this.health >= 5)
                this.health = 5;
            gmr.imgChange(0, this.health);
            PlayerPrefs.SetInt("Health", this.health);
        }
        if (health <= 0)
            this.Die();
    }
    public void MentalChange(int value)
    {
        if (mental > 0)
        {
            this.mental += value;
            if (this.mental >= 5)
                this.mental = 5;
            gmr.imgChange(1, this.mental);
            PlayerPrefs.SetInt("Mental", this.mental);
        }
        if (mental <= 0)
            this.Die();
    }

    public void Changeability(player_ability ability, int value) //value: Amount of ability Change
    {
        if (ability == player_ability.force)
        {
            this.force += value;
            gmr.changeability_amount(ability, this.force);
            PlayerPrefs.SetInt("Force", this.force);
        }
        else if (ability == player_ability.intellect)
        {
            this.intellect += value;
            gmr.changeability_amount(ability, this.intellect);
            PlayerPrefs.SetInt("Intellect", this.intellect);
        }
        else if (ability == player_ability.mana)
        {
            this.mana += value;
            gmr.changeability_amount(ability, this.mana);
            PlayerPrefs.SetInt("Mana", this.mana);
        }
    }

    public void Die()
    {
        PlayerPrefs.DeleteAll();
        gmr.DiepanelActive();
        if(health == 0)
            Debug.Log("Die");
        else if(mental == 0)
            Debug.Log("Die");
    }

    public Pocket FindPocketElement(string name)
    {
        Pocket result = entirePocket.Find( // finding element
            delegate (Pocket tmp)
            {
                return tmp.getName() == name;
            });
        if (result != null)
            return result;
        else
            return null;
    }
    public bool AbilityAvailable(player_ability ability, int value)
    {
        if(ability == player_ability.force)
        {
            if (this.force >= value)
                return true;
            else
                return false;
        }
        else if (ability == player_ability.intellect)
        {
            if (this.intellect >= value)
                return true;
            else
                return false;
        }
        else
        {
            if (this.mana >= value)
                return true;
            else
                return false;
        }
    }
    public void SetAgain() //Play Again
    {
        this.health = 5;
        this.mental = 5;
        gmr.imgChange(0, health);
        gmr.imgChange(1, mental);
    }
    public void putPocketElements(Pocket element)
    {
        Pocket result = entirePocket.Find( // finding element
            delegate (Pocket tmp)
            {
                return tmp.getName() == element.getName();
            });

        if(result != null) // element found
        {
            result.increseNum();
        }
        else // element not found
        {
            //Put element into entirePocket list
            entirePocket.Add(element);

            if (element.getType() == 0)
            {
                //Put element in to ability list
                abilityPocket.Add(element as Ability);
            }
            else if (element.getType() == 1)
            {
                //Put element in to item list
                itemPocket.Add(element as Item);
            }
            else if (element.getType() == 2)
            {
                statePocket.Add(element as State);
            }
        }
    }
    public void removePocketElement(Pocket element)
    {
        Pocket result = entirePocket.Find( // finding element
            delegate (Pocket tmp)
            {
                return tmp.getName() == element.getName();
            });
        if(result != null)
        {
            //element found
            if(result.getNum() >= 2) // more than one element -> just decrease num
            {
                result.decreseNum();
            }
            else // just one element -> 1. remove from lists, 2. destory object
            {
                //Remove From Lists
                entirePocket.Remove(result);
                abilityPocket.Remove(result as Ability);
                itemPocket.Remove(result as Item);
                statePocket.Remove(result as State);

                //Destroy Obj
                result = null;
                GC.Collect();
            }
        }
        else
        {
            // element not found
            // do nothing.. maybe?
        }
    }
    public void test() // TEST
    {
        putPocketElements(new Ability("¿ä¸®½Ç·Â", 1));
        putPocketElements(new Item("ÃÊÄÝ¸´", 1));
        putPocketElements(new State("°¨¿°", 1));
        putPocketElements(new State("°¨¿°", 1));
        putPocketElements(new State("°¨¿°", 1));
        removePocketElement(new Item("ÃÊÄÝ¸´", 1));
        removePocketElement(new State("°¨¿°", 1));
        putPocketElements(new State("°¨¿°", 1));
        putPocketElements(new State("°¨¿°", 1));
        putPocketElements(new Ability("±Ù·Â", 1));
        putPocketElements(new Item("»ìÃæÁ¦", 1));
        //Changeability(player_ability.force, 2);
        //Changeability(player_ability.intellect, 2);
        //Changeability(player_ability.mana, 2);

    }

    public string setStr(int type)
    {
        string pocket_str = "";
        if (sortbytime)
        {
            if (type == 0) // Entire
            {
                for (int i = 0; i < entirePocket.Count; i++)
                {
                    pocket_str += entirePocket[i].getNameStr() + "\n";
                }
            }
            else if (type == 1) // Abilbity 
            {
                for (int i = 0; i < abilityPocket.Count; i++)
                {
                    pocket_str += abilityPocket[i].getNameStr() + "\n";
                }
            }
            else if (type == 2) // Item
            {
                for (int i = 0; i < itemPocket.Count; i++)
                {
                    pocket_str += itemPocket[i].getNameStr() + "\n";
                }
            }
            else if (type == 3) // State
            {
                for (int i = 0; i < statePocket.Count; i++)
                {
                    pocket_str += statePocket[i].getNameStr() + "\n";
                }
            }
        }
        else //sort by char
        {
            sortPocket();
            if (type == 0) // Entire
            {
                for (int i = 0; i < entirePocketSort.Length; i++)
                {
                    pocket_str += entirePocketSort[i].getNameStr() + "\n";
                }
            }
            else if (type == 1) // Abilbity 
            {
                for (int i = 0; i < abilityPocketSort.Length; i++)
                {
                    pocket_str += abilityPocketSort[i].getNameStr() + "\n";
                }
            }
            else if (type == 2) // Item
            {
                for (int i = 0; i < itemPocketSort.Length; i++)
                {
                    pocket_str += itemPocketSort[i].getNameStr() + "\n";
                }
            }
            else if (type == 3) // State
            {
                for (int i = 0; i < statePocketSort.Length; i++)
                {
                    pocket_str += statePocketSort[i].getNameStr() + "\n";
                }
            }
        }
        return pocket_str;
    }
    public List<string> setPreviewStr()
    {
        List<string> preStrList = new List<string>();
        if(entirePocket.Count <= 2)
        {
            for(int i =0; i < entirePocket.Count; i++)
            {
                preStrList.Add(entirePocket[i].getNameStr());
            }
        }
        else
        {
            for(int i =0; i < 3; i++)
            {
                preStrList.Add(entirePocket[i].getNameStr());
            }
        }
        return preStrList;
    }

    public void sortPocket()
    {
        entirePocketSort = new Pocket[entirePocket.Count];
        abilityPocketSort = new Ability[abilityPocket.Count];
        itemPocketSort = new Item[itemPocket.Count];
        statePocketSort = new State[statePocket.Count];


        IComparer<Pocket> comparer = new PocketComparer();
        if (entirePocket.Count != 0)
        {
            entirePocket.CopyTo(entirePocketSort);
            if (entirePocket.Count >= 2)
                Array.Sort(entirePocketSort, comparer);
        }
        if(abilityPocket.Count != 0)
        {
            abilityPocket.CopyTo(abilityPocketSort);
            if(abilityPocket.Count >= 2)
                Array.Sort(abilityPocketSort, comparer);
        }
        if(itemPocket.Count != 0)
        {
            itemPocket.CopyTo(itemPocketSort);
            if (itemPocket.Count >= 2)
                Array.Sort(itemPocketSort, comparer);
        }
        if(statePocket.Count != 0)
        {
            statePocket.CopyTo(statePocketSort);
            if (statePocket.Count >= 2)
                Array.Sort(statePocketSort, comparer);
        }
    }

}

public class PocketComparer : IComparer<Pocket> //Pocket Comparer
{
    public int Compare(Pocket x, Pocket y)
    {
        return (x.CompareTo(y));
    }
}