using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<Image> health;
    public List<Image> mental;
    public Text forceAmount;
    public Text intellectAmount;
    public Text politicalAmount;

    // Start is called before the first frame update
    void Start()
    {
        imgSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void imgChange(int state, int value) // Change Images by state & value
    {
        //state 0: health, 1: mental
        if (state == 0) {
            for (int i = 0; i < value; i++)
                health[i].gameObject.SetActive(true);
            for (int i = 4; i >= value; i--)
                health[i].gameObject.SetActive(false);
        }
        else if(state == 1)
        {
            for (int i = 0; i < value; i++)
                mental[i].gameObject.SetActive(true);
            for (int i = 4; i >= value; i--)
                mental[i].gameObject.SetActive(false);
        }
    }
    public void imgSet()
    {
        imgChange(0, 5);
        imgChange(1, 5);
    }
    public void changeability_amount(player_ability ability, int value)
    {
        if (ability == player_ability.force)
            forceAmount.text = value.ToString();
        else if (ability == player_ability.intellect)
            intellectAmount.text = value.ToString();
        else if (ability == player_ability.political_power)
            politicalAmount.text = value.ToString();
    }
}

