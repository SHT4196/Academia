using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AddChoice : MonoBehaviour
{

    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private GameObject choicePrefab_activefalse;
    private GameObject Panel;
    private GameObject choiceBox;
    private TextMeshProUGUI choiceText;
    private Choice choice;
    
    void Start()
    {
        MakeButton();
    }

    public void MakeButton()
    {
        int i = NextContainer.Instance.nextChoice.Count -1;
        foreach (string c in NextContainer.Instance.nextChoice)
        {
            int choiceCount = 0;
            bool need_complete = true;
            choice = MakeDialog.Instance.FindChoice(c);
            string tmpText = "";
            if (choice.need != null)
            {
                choiceCount = choice.need.Count;
            }
            for(int j = 0; j < choiceCount; j++)
            {
                bool AbilityAvailableCheck = false;
                string[] _val = choice.need[j].Split('%');
                if(_val[0] == "¹«")
                {
                    int value = 0;
                    for(int k = 0; k < _val[1].ToIntArray().Length; k++)
                    {
                        value += (_val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, _val[1].ToIntArray().Length - k - 1);
                    }
                    AbilityAvailableCheck = Player.Instance.AbilityAvailable(player_ability.force, value);
                }
                else if (_val[0] == "Áö")
                {
                    int value = 0;
                    
                    for (int k = 0; k < _val[1].ToIntArray().Length; k++)
                    {
                        value += (_val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, _val[1].ToIntArray().Length - k - 1);
                        
                    }
                    AbilityAvailableCheck = Player.Instance.AbilityAvailable(player_ability.intellect, value);
                }
                else if (_val[0] == "¸¶")
                {
                    int value = 0;
                    for (int k = 0; k < _val[1].ToIntArray().Length; k++)
                    {
                        value += (_val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, _val[1].ToIntArray().Length - k - 1);
                    }
                    AbilityAvailableCheck = Player.Instance.AbilityAvailable(player_ability.mana, value);
                }
                if (AbilityAvailableCheck == false)
                {
                    tmpText += "<color=#8B0000>";
                    tmpText += choice.need[j];
                    tmpText += "</color>";
                    need_complete = false;
                }
                else
                {
                    tmpText += "<color=#008000>";
                    tmpText += choice.need[j];
                    tmpText += "</color>";
                }
                if (j != choiceCount - 1)
                    tmpText += "  ";
            }
            tmpText += " " + choice.text;
            Panel = GameObject.Find("Panel");
            if (need_complete)
            {
                choiceBox = Instantiate(choicePrefab, Panel.transform.position, Panel.transform.rotation) as GameObject;
                choiceBox.transform.SetParent(Panel.transform, false);
                choiceBox.transform.position += new Vector3(-1300, -150 * i, 0);
            }
            else
            {
                choiceBox = Instantiate(choicePrefab_activefalse, Panel.transform.position, Panel.transform.rotation) as GameObject;
                choiceBox.transform.SetParent(Panel.transform, false);
                choiceBox.transform.position += new Vector3(-1300, -150 * i, 0);
            }

            choiceText = choiceBox.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = tmpText;
            choiceBox.name = choice.next;

            i--;
            
        }
    }

    public void DestroyButton()
    {
        Panel = GameObject.Find("Panel");
        for (int i = 0; i < Panel.transform.childCount; i++)
        {
            Destroy(Panel.transform.GetChild(i).gameObject);
        }
    }

}
