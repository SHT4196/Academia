using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public List<Image> health;
    public List<Image> mental;
    public Text forceAmount;
    public Text manaAmount;
    public Text intellectAmount;
    public Image diepanel;

    public Image[] stat_fill = new Image[3];    //force, intel, poli 순서로 사용

    // Start is called before the first frame update
    void Start()
    {
        //imgSet();
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
    public void imgSet(int health, int mental)
    {
        imgChange(0, health);
        imgChange(1, mental);
    }
    public void changeability_amount(player_ability ability, int value)
    {
        if (ability == player_ability.force)
        {
            forceAmount.text = value.ToString();
            stat_fill[0].fillAmount = value / 20.0f;
        }
        else if (ability == player_ability.intellect)
        {
            intellectAmount.text = value.ToString();
            stat_fill[1].fillAmount = value / 20.0f;
        }
        else if (ability == player_ability.mana)
        {
            manaAmount.text = value.ToString();
            stat_fill[2].fillAmount = value / 20.0f;
        }
    }
    public void DieAndSceneChange()
    {
        SceneManager.LoadScene(1);
    }
    public void Die()
    {
        Player.Instance.Die();
    }
}

