using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


public class AddChoice : MonoBehaviour
{
    [SerializeField] private GameObject choicePrefab;
    [FormerlySerializedAs("choicePrefab_activefalse")] [SerializeField] private GameObject choicePrefabActivefalse;
    private GameObject _panel;
    private GameObject _choiceBox;
    private TextMeshProUGUI _choiceText;
    private Choice _choice;
    
    void Start()
    {
        MakeButton();
    }

    /// <summary>
    /// ������ Button ���� ����
    /// </summary>
    public void MakeButton()
    {
        int i = NextContainer.instance.NextChoice.Count -1;
        foreach (string c in NextContainer.instance.NextChoice) // �� choice ���� ������ ����
        {
            int choiceCount = 0;
            bool needComplete = true;
            _choice = MakeDialog.instance.FindChoice(c);
            string tmpText = "";
            if (_choice.need != null)
            {
                choiceCount = _choice.need.Count;
            }
            for(int j = 0; j < choiceCount; j++) // choice available ���� Ȯ��
            {
                bool abilityAvailableCheck = false;
                if (_choice.need != null)
                {
                    string[] val = _choice.need[j].Split('%');
                    if(val[0] == "��") 
                    {
                        int value = 0;
                        for(int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Force, value);
                    }
                    else if (val[0] == "��")
                    {
                        int value = 0;
                    
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Intellect, value);
                    }
                    else if (val[0] == "��")
                    {
                        int value = 0;
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Mana, value);
                    }
                }

                if (abilityAvailableCheck == false) // �������� ������ �� ���� ��
                {
                    tmpText += "<color=#8B0000>";
                    tmpText += _choice.need[j];
                    tmpText += "</color>";
                    needComplete = false;
                }
                else // ������ ���� ������ ��
                {
                    tmpText += "<color=#008000>";
                    tmpText += _choice.need[j];
                    tmpText += "</color>";
                }
                if (j != choiceCount - 1)
                    tmpText += "  ";
            }
            tmpText += " " + _choice.text;
            _panel = GameObject.Find("Panel");
            
            // ������ ���� ���� ���ο� ���� Prefab Instantiation
            if (needComplete)
            {
                _choiceBox = Instantiate(choicePrefab, _panel.transform.position, _panel.transform.rotation) as GameObject;
                _choiceBox.transform.position += new Vector3(0, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height, 0);
                _choiceBox.transform.SetParent(_panel.transform, false);
            }
            else
            {
                _choiceBox = Instantiate(choicePrefabActivefalse, _panel.transform.position, _panel.transform.rotation) as GameObject;
                _choiceBox.transform.position += new Vector3(0, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height, 0);
                _choiceBox.transform.SetParent(_panel.transform, false);
            }

            _choiceText = _choiceBox.GetComponentInChildren<TextMeshProUGUI>();
            _choiceText.text = tmpText;
            _choiceBox.name = _choice.next;

            i--;
            
        }
    }

    /// <summary>
    /// ������ ��ư ����
    /// </summary>
    public void DestroyButton()
    {
        _panel = GameObject.Find("Panel");
        for (int i = 0; i < _panel.transform.childCount; i++)
        {
            Destroy(_panel.transform.GetChild(i).gameObject);
        }
    }

}
