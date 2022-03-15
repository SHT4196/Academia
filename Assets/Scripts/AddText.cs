using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class AddText : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI textPrefab;
    private static List<TextMeshProUGUI> textBox = new List<TextMeshProUGUI>();
    private static string currentID;
    private Script script;
    private List<Choice> choice;

    private static List<Image> imgarr = new List<Image>();

    private static List<bool> isimage = new List<bool>();

    private static TextMeshProUGUI empty;

	public static float delay = 0.01f;
    private static Coroutine coroutine;

    private static string fulltext;
    private string currentText;
    private static TextMeshProUGUI tb;      //current text box

    private static bool text_exit;
    private static bool text_full;
    private static bool text_cut;
    private static bool trigger = false;     //trigger coroutine

    private float _emptyTextSize = 0f;

    private float widthRatio = 2.5f;
    private float heightRatio = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        AddScript();
    }

    public void AddScript()
    {
        script = MakeDialog.Instance.FindScript(NextContainer.Instance.nextText);
        DestroyEmpty();
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
            textBox[textBox.Count - 1].rectTransform.localScale = new Vector3(widthRatio, heightRatio);
            PlayerPrefs.SetString("ScriptID", script.id);

            Get_Typing(script.text, textBox[textBox.Count-1]);

            if(isimage[isimage.Count-2])
            {
                // ScrollAmount += 480f;
            }

            Scroll.Instance.pos += textBox[textBox.Count - 2].GetComponent<RectTransform>().rect.height * textBox[textBox.Count - 2].rectTransform.localScale.y + 200f;
            AddEmpty();
            Scroll.Instance.IsScroll = true;
        }
        else
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
            textBox[textBox.Count - 1].rectTransform.localScale = new Vector3(widthRatio, heightRatio);
            PlayerPrefs.SetString("ScriptID", script.id);

            Get_Typing(script.text, textBox[0]);
            
            Scroll.Instance.pos = 0f;
            AddEmpty();
            Scroll.Instance.ScrollReset();
        }
        NextContainer.Instance.nextChoice = script.next;
        currentID = script.id;
    }

    public void DestroyScript()
    {
        for(int i = 0; i < textBox.Count(); i++){
            if(textBox[i] != null)
                Destroy(textBox[i].gameObject);
        }
        textBox.Clear();
    }
    public void AddPicture(Sprite picture)          //n is changed by sprite size
    {
        Image img = Instantiate(Resources.Load<Image>("Prefab/Image"));
        imgarr.Add(img);
        img.sprite = picture;
        img.rectTransform.localScale =
            new Vector3(img.rectTransform.localScale.x - 0.4f, img.rectTransform.localScale.y - 0.4f);
        img.transform.SetParent(GameObject.Find("Content").transform, true);
        img.SetNativeSize();
        Scroll.Instance.pos += img.rectTransform.rect.height * img.rectTransform.localScale.y + 200f;
    }

    public void DestroyPicture()
    {
        for(int i = 0; i < imgarr.Count();i++){
            Destroy(imgarr[i].gameObject);
        }
        imgarr.Clear();
    }

    public void AddEmpty()
    {
        empty = Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/EmptyText"));
        empty.transform.SetParent(GameObject.Find("Content").transform, true);
         if (empty.rectTransform.sizeDelta.y + textBox[textBox.Count - 1].rectTransform.sizeDelta.y*textBox[textBox.Count - 1].rectTransform.localScale.y + 200 <=
            empty.transform.parent.parent.GetComponent<RectTransform>().rect.height)
        {
            empty.rectTransform.sizeDelta = new Vector2(textBox[textBox.Count - 1].rectTransform.sizeDelta.x,
                Mathf.Abs(empty.transform.parent.parent.GetComponent<RectTransform>().rect.height - 200 -
                          textBox[textBox.Count - 1].rectTransform.sizeDelta.y * textBox[textBox.Count - 1].rectTransform.localScale.y));
            
        }
        empty.rectTransform.localScale = Vector3.one;
    }
    public void DestroyEmpty()
    {
        if(empty != null)
        {
            Destroy(empty.gameObject);
            empty = null;
        }
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

    public void Get_Typing(string _fullText, TextMeshProUGUI textbox)
    {
        text_exit = false;
        text_full = false;
        text_cut = false;

        fulltext = _fullText;
        tb = textbox;

        trigger = true;
    }

    public void End_Typing()
    {
        if (!text_full)
        {
            text_cut = true;
        }
    }

    private IEnumerator ShowText(string _fullText, TextMeshProUGUI _textBox)
    {
        currentText = "";
        for (int i = 0; i < _fullText.Length; i++)
        {
            if (text_cut == true)
            {
                break;
            }
            currentText = _fullText.Substring(0, i + 1);
            _textBox.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        _textBox.text = _fullText;
        text_exit = true;
        StopCoroutine(coroutine);

        text_full = true;
    }
    void Update() 
    {
        if (trigger)
        {
            coroutine = StartCoroutine(ShowText(fulltext, tb));
            trigger = false;
        }
        if (text_exit)
        {
            int n = GameObject.Find("Panel").transform.childCount;
            for (int i = 0; i < n; i++)
            {
                GameObject.Find("Panel").transform.GetChild(i).gameObject.SetActive(true);
            }
            DestroyEmpty();
            AddEmpty();

            text_exit = false;
        }
    }
}
