using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AddText : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI textPrefab;
    private static List<TextMeshProUGUI> textBox = new List<TextMeshProUGUI>();
    private static string currentID;
    private Script script;
    private List<Choice> choice;

    public Image imgprefab;
    private static List<Image> imgarr = new List<Image>();

    private static List<bool> isimage = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        AddScript();
    }

    public void AddScript()
    {
        script = MakeDialog.Instance.FindScript(NextContainer.Instance.nextText);
        if (currentID[0] == script.id[0] && currentID[1] == script.id[1]) // ���� ���� ���丮
        {
            if(script.sprite != "null")
            {
                Sprite pic = Resources.Load<Sprite>("Images/" + script.sprite);
                AddPicture(pic);
                isimage.Add(true);
            }
            else
            {
                isimage.Add(false);
            }
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));
            textBox[textBox.Count-1].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[textBox.Count-1].text = script.text;
            if(isimage[isimage.Count-2])
            {
                GameObject.Find("Content").GetComponent<Scroll>().pos += 480;
            }
            GameObject.Find("Content").GetComponent<Scroll>().pos += textBox[textBox.Count-2].GetComponent<RectTransform>().rect.height * 0.555f;
            GameObject.Find("Content").GetComponent<Scroll>().pos += 200;
            GameObject.Find("Content").GetComponent<Scroll>().IsScroll = true;
        }
        else //���ο� ���丮
        {
            DestroyScript();
            DestroyPicture();
            if(script.sprite != "null")
            {
                Sprite pic = Resources.Load<Sprite>("Images/" + script.sprite);
                AddPicture(pic);
                isimage.Add(true);
            }
            else
            {
                isimage.Add(false);
            }
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));
            textBox[0].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[0].text = script.text;
            GameObject.Find("Content").GetComponent<Scroll>().pos = 0;
            GameObject.Find("Content").GetComponent<Scroll>().IsScroll = true;
        }
        NextContainer.Instance.nextChoice = script.next;
        currentID = script.id;
        Debug.Log(textBox[0].GetComponent<RectTransform>().rect.y);
    }

    public void DestroyScript(){
        for(int i = 0; i < textBox.Count(); i++){
            Destroy(textBox[i].gameObject);
        }
        textBox.Clear();
    }
    public void AddPicture(Sprite picture)          //n is changed by sprite size
    {
        Image img = Instantiate(Resources.Load<Image>("Prefab/Image"));
        imgarr.Add(img);
        img.sprite = picture;
        img.transform.SetParent(GameObject.Find("Content").transform, true);
        //img.transform.position = textBox[textBox.Count-1].transform.position;
    }

    public void DestroyPicture(){
        for(int i = 0; i < imgarr.Count();i++){
            Destroy(imgarr[i]);
        }
        imgarr.Clear();
    }
    public void Stat()
    {
        for(int i = 0; i < script.result.Count; i++)
        {
            string temp_str = script.result[i];
            string string_val = "";
            if (temp_str != "-")
            {
                if (temp_str[0] == '지')
                {
                    if (temp_str[1] == '+')
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.intellect, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.intellect, Int32.Parse(string_val) * (-1));
                    }
                }
                if (temp_str[0] == '무')
                {
                    if (temp_str[1] == '+')
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.force, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.force, Int32.Parse(string_val) * (-1));
                    }
                }
                if (temp_str[0] == '마')
                {
                    if (temp_str[1] == '+')
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.mana, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.Changeability(player_ability.mana, Int32.Parse(string_val) * (-1));
                    }
                }
                if (temp_str[0] == '체')
                {
                    if (temp_str[1] == '+')
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.HealthChange(Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.HealthChange(Int32.Parse(string_val) * (-1));
                    }
                }
                if (temp_str[0] == '정')
                {
                    if (temp_str[1] == '+')
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.MentalChange(Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.Instance.MentalChange(Int32.Parse(string_val) * (-1));
                    }
                }
            }
        }
    }
}
