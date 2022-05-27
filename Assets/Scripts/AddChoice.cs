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
    /// 선택지 Button 동적 생성
    /// </summary>
    public void MakeButton()
    {
        int i = 0;
        foreach (string c in NextContainer.instance.NextChoice) // 각 choice 별로 선택지 생성
        {
            int choiceCount = 0;
            bool needComplete = true;
            _choice = MakeDialog.instance.FindChoice(c);
            string tmpText = "";
            if (_choice.need != null)
            {
                choiceCount = _choice.need.Count;
            }
            for(int j = 0; j < choiceCount; j++) // choice available 여부 확인
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
                    else if (val[0] == "정")
                    {
                        int value = 0;
                    
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Intellect, value);
                    }
                    else if (val[0] == "지")
                    {
                        int value = 0;
                        for (int k = 0; k < val[1].ToIntArray().Length; k++)
                        {
                            value += (val[1].ToIntArray()[k] - 48) * (int)Mathf.Pow(10, val[1].ToIntArray().Length - k - 1);
                        }
                        abilityAvailableCheck = Player.instance.AbilityAvailable(PlayerAbility.Mana, value);
                    }
                }

                if (abilityAvailableCheck == false) // 선택지를 선택할 수 없을 때
                {
                    tmpText += "<color=#8B0000>";
                    tmpText += _choice.need[j];
                    tmpText += "</color>";
                    needComplete = false;
                }
                else // 선택지 선택 가능할 때
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
            
            // 선택지 선택 가능 여부에 따른 Prefab Instantiation
            if (needComplete)
            {
                _choiceBox = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                _choiceBox.transform.SetParent(_panel.transform, false);
                //_panel.transform.position.y + 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height
                _choiceBox.transform.position = new Vector3(_choiceBox.transform.position.x, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height, _choiceBox.transform.position.z);
            }
            else
            {
                _choiceBox = Instantiate(choicePrefabActivefalse, Vector3.zero, Quaternion.identity) as GameObject;
                _choiceBox.transform.SetParent(_panel.transform, false);
                _choiceBox.transform.position = new Vector3(_choiceBox.transform.position.x, 1 * i * _choiceBox.GetComponent<RectTransform>().rect.height, _choiceBox.transform.position.z);
            }

            _choiceText = _choiceBox.GetComponentInChildren<TextMeshProUGUI>();
            _choiceText.text = tmpText;
            if (_choice.next.Count == 1) //확률 없음 - choicenext 한개
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
    /// 선택지 버튼 삭제
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
