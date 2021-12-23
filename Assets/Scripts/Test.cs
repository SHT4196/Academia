using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour //Just For Test
{
    public void healthup()
    {
        Player.Instance.gainHealth(1);
    }
    public void healthdown()
    {
        Player.Instance.loseHealth(1);
    }
    public void mentalup()
    {
        Player.Instance.gainMental(1);
    }
    public void mentaldown()
    {
        Player.Instance.loseMental(1);
    }

    public void forceup50()
    {
        Player.Instance.Changeability(player_ability.force, 50);
    }
    public void forcedown50()
    {
        Player.Instance.Changeability(player_ability.force, -50);
    }
    //public void moneyup()
    //{
    //    Player.Instance.gainMoney(1);
    //}
    //public void moneydown()
    //{
    //    Player.Instance.loseMoney(1);
    //}
}