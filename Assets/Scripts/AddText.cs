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
    private static List<TextMeshProUGUI> textBox = new List<TextMeshProUGUI>();
    private static string currentID;
    private Script script;     //store script information for current id (information read from Excel file)
    private List<Choice> choice;

    private static List<Image> imgarr = new List<Image>();    //store image information and representation on one screen

    private static List<bool> isimage = new List<bool>();     //store is image exist

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
        script = MakeDialog.instance.FindScript(NextContainer.instance.NextText);
        DestroyEmpty(); //Destroy empty text prefab 
        //float ScrollAmount = 0;
        if (currentID[0] == script.id[0] && currentID[1] == script.id[1]) //Check if the screen is switched

        {
            if(script.sprite != "null")  //Check if there's a picture that needs to be added
            {
                Sprite pic = Resources.Load<Sprite>("Images/" + script.sprite);
                AddPicture(pic);
                isimage.Add(true);
            }
            else
            {
                isimage.Add(false);
            }
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));  //add text prefab
            textBox[textBox.Count-1].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[textBox.Count - 1].rectTransform.localScale = new Vector3(widthRatio, heightRatio);
            PlayerPrefs.SetString("ScriptID", script.id); //save id


            Get_Typing(script.text, textBox[textBox.Count-1]);  //typing animation start


            if(isimage[isimage.Count-2])  //If image exist, increase the amount of scrolling.

            {
                // ScrollAmount += 480f;
            }

            //Increase the amount of scrolling by the size of the text box
            Scroll.instance.pos += textBox[textBox.Count - 2].GetComponent<RectTransform>().rect.height * textBox[textBox.Count - 2].rectTransform.localScale.y + 200f;
            // ScrollAmount += textBox[textBox.Count-2].GetComponent<RectTransform>().rect.height * 0.555f;
            // Scroll.Instance.pos += 200f;
            // ScrollAmount += 200f;
            AddEmpty();//add empty text
            // Scroll.Instance.scrollAmount = ScrollAmount;

            Scroll.instance.isScroll = true;
        }
        else
        {
            DestroyScript();  //destroy previous text
            DestroyPicture();  //destroy previous picture
            if(script.sprite != "null")  //Check if there's a picture that needs to be added
            {
                Sprite pic = Resources.Load<Sprite>("Images/" + script.sprite);
                AddPicture(pic);
                isimage.Add(true);
            }
            else
            {
                isimage.Add(false);
            }
            textBox.Add(Instantiate(Resources.Load<TextMeshProUGUI>("Prefab/TextPrefab")));  //add text prefab
            textBox[0].transform.SetParent(GameObject.Find("Content").transform , true);
            textBox[textBox.Count - 1].rectTransform.localScale = new Vector3(widthRatio, heightRatio);
            PlayerPrefs.SetString("ScriptID", script.id); //save id


            Get_Typing(script.text, textBox[0]);  //typing animation start
            

            Scroll.instance.pos = 0f; //reset screen's position

            AddEmpty();
            Scroll.instance.ScrollReset();
        }
        NextContainer.instance.NextChoice = script.next;
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
    public void AddPicture(Sprite picture)
    {
        Image img = Instantiate(Resources.Load<Image>("Prefab/Image"));
        imgarr.Add(img);
        img.sprite = picture;
        img.rectTransform.localScale =
            new Vector3(img.rectTransform.localScale.x - 0.4f, img.rectTransform.localScale.y - 0.4f);
        img.transform.SetParent(GameObject.Find("Content").transform, true);
        img.SetNativeSize();
        Scroll.instance.pos += img.rectTransform.rect.height * img.rectTransform.localScale.y + 200f;
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
                        Player.instance.Changeability(PlayerAbility.Intellect, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.instance.Changeability(PlayerAbility.Intellect, Int32.Parse(string_val) * (-1));
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
                        Player.instance.Changeability(PlayerAbility.Force, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.instance.Changeability(PlayerAbility.Force, Int32.Parse(string_val) * (-1));
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
                        Player.instance.Changeability(PlayerAbility.Mana, Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.instance.Changeability(PlayerAbility.Mana, Int32.Parse(string_val) * (-1));
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
                        Player.instance.HealthChange(Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.instance.HealthChange(Int32.Parse(string_val) * (-1));
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
                        Player.instance.MentalChange(Int32.Parse(string_val));
                    }
                    else
                    {
                        for (int j = 2; j < temp_str.Length; j++)
                        {
                            string_val += temp_str[j];
                        }
                        Player.instance.MentalChange(Int32.Parse(string_val) * (-1));
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

        trigger = true;  //if trigger is true, coroutine start at update fuction
    }

    public void End_Typing()  //skip typing effect
    {
        if (!text_full)
        {
            text_cut = true;
        }
    }

    private IEnumerator ShowText(string _fullText, TextMeshProUGUI _textBox)  //typing effect
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
        if (text_exit)  //if typing effect end, next button express.
        {
            int n = GameObject.Find("Panel").transform.childCount;
            for (int i = 0; i < n; i++)
            {
                GameObject.Find("Panel").transform.GetChild(i).gameObject.SetActive(true);
            }

            //empty prefab bug fix
            DestroyEmpty();
            AddEmpty();

            text_exit = false;
        }
    }
}
