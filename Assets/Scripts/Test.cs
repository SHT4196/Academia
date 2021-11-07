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
    public void moneyup()
    {
        Player.Instance.gainMoney(1);
    }
    public void moneydown()
    {
        Player.Instance.loseMoney(1);
    }
}
/*
[TO DO] 
1. 업적
2. 능력, 아이템, 상태 얻었을 떄 뜨는 메세지 구현
3. 모험 재시작 구현
4. pocket 창 옆 슬라이드로 넘어가게 & 카테고리 bold
5. pocket 창 안 text 한줄에 세개 
6. Die() 구현
 */