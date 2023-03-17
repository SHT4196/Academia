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
        int i = 0;
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
                    if(val[0] == "무") 
                    {
                        int value = 0;
                        for(int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Force, value);
                        
                    }
                    else if (val[0] == "지")
                    {
                        int value = 0;
                    
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Intellect, value);
                    }
                    else if (val[0] == "마")
                    {
                        int value = 0;
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Mana, value);
                    }
                }
                string[] rList = _choice.need[j].Split('%'); //replace list
                string replacedTxt = "";
                if (rList[0] == "지")
                {
                    replacedTxt += rList[0];
                    replacedTxt += "력 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }
                else if (rList[0] == "고")
                {
                    replacedTxt += rList[0];
                    replacedTxt += "려 호감도 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }
                else if (rList[0] == "연")
                {
                    replacedTxt += rList[0];
                    replacedTxt += "세 호감도 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }
                else if (rList[0] == "서")
                {
                    replacedTxt += rList[0];
                    replacedTxt += "울 호감도 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }
                else if (rList[0] == "강")
                {
                    replacedTxt += "서";
                    replacedTxt += rList[0];
                    replacedTxt += " 호감도 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }
                else
                {
                    replacedTxt += rList[0];
                    replacedTxt += "력 ";
                    replacedTxt += rList[1];
                    replacedTxt += "필요";
                    _choice.need[j] = replacedTxt;
                }

                if (abilityAvailableCheck == false) // �������� ������ �� ���� ��
                {
                    tmpText += "<color=#791414>";
                    tmpText += _choice.need[j];
                    tmpText += "</color>";
                    needComplete = false;
                }
                else // ������ ���� ������ ��
                {
                    tmpText += "<color=#14782E>";
                    tmpText += _choice.need[j];
                    tmpText += "</color>";
                }
            }
            tmpText += " " + _choice.text;
            _panel = GameObject.Find("Panel");
            
            // ������ ���� ���� ���ο� ���� Prefab Instantiation
            if (needComplete)
            {
                _choiceBox = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                _choiceBox.transform.SetParent(_panel.transform, false);
                //_panel.transform.position.y + 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height
                Debug.Log(_choiceBox.GetComponent<RectTransform>().rect.height);
                _choiceBox.GetComponent<RectTransform>().anchoredPosition = 
                new Vector2(0, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height);
                if (i != 0)
                {
                    _choiceBox.GetComponent<RectTransform>().anchoredPosition += 
                        new Vector2(0, _choiceBox.GetComponent<RectTransform>().rect.height * 0f * i);
                }
            }
            else
            {
                _choiceBox = Instantiate(choicePrefabActivefalse, Vector3.zero, Quaternion.identity) as GameObject;
                _choiceBox.transform.SetParent(_panel.transform, false);
                _choiceBox.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(0, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height);
                if (i != 0)
                {
                    _choiceBox.GetComponent<RectTransform>().anchoredPosition += 
                        new Vector2(0, _choiceBox.GetComponent<RectTransform>().rect.height * 0f * i);
                }
            }

            _choiceText = _choiceBox.GetComponentInChildren<TextMeshProUGUI>();
            _choiceText.text = tmpText;
            if (_choice.next.Count == 1) //Ȯ�� ���� - choicenext �Ѱ�
            {
                _choiceBox.name = _choice.next[0];
            }
            else
            {
                int randomValue = Random.Range(1, 11);
                Debug.Log(randomValue);
                int num = 0;
                foreach (var choiceNext in _choice.next)
                {
                    num += choiceNext[choiceNext.IndexOf('(') + 1] - '0';
                    if (randomValue <= num)
                    {
                        _choiceBox.name = choiceNext.Split('(')[0];
                        break;
                    }
                }   
            }

            i++;
            
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
