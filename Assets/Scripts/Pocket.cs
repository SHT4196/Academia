using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket
{
    protected string __name;
    protected int num; // Ability: level, Item & State: count
    protected int type; // 0: Ability, 1: Item, 2: State

    public virtual string getNameStr()
    {
        string nameWithNum = __name + "x" + num.ToString();
        return nameWithNum;
    }
    public int getType() { return type; }
    public void increseNum() { num++; }
    public void decreseNum() { num--; }
    public int getNum() { return num; }
    public string getName() { return __name; }

    public int CompareTo(Pocket a)
    {
        // A null value means that this object is greater.
        if (a == null)
            return 1;

        else
            return this.__name.CompareTo(a.__name);
    }
}

public class Ability : Pocket
{
    public Ability(string _name, int _level)
    {
        this.__name = _name;
        this.num = _level;
        this.type = 0;
    }
    public override string getNameStr()
    {
        string nameWithNum = __name + " Lv." + num.ToString();
        return nameWithNum;
    }
}

public class Item : Pocket
{
    public Item(string _name, int _count)
    {
        this.__name = _name;
        this.num = _count;
        this.type = 1;
    }
}

public class State : Pocket
{
    public State(string _name, int _count)
    {
        this.__name = _name;
        this.num = _count;
        this.type = 2;
    }
}