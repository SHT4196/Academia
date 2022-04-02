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
    /// <summary>
    /// 씬 안에서 스크립트가 표시되는 모든 textbox
    /// </summary>
    private static List<TextMeshProUGUI> textBox = new List<TextMeshProUGUI>();
    /// <summary>
    /// 현재 ID값
    /// </summary>
    private static string currentID;
    /// <summary>
    /// 엑셀파일에서 읽어온 현재 id에 대한 스크립트의 정보
    /// </summary>
    private Script script;
    private List<Choice> choice;
    /// <summary>
    /// 씬 안에서 표시되는 모든 image
    /// </summary>
    private static List<Image> imgarr = new List<Image>();
    private static List<bool> isimage = new List<bool>();
    /// <summary>
    /// 공백의 textbox
    /// </summary>
    private static TextMeshProUGUI empty;
    /// <summary>
    /// typing 효과를 주는 coroutine 함수에게 주는 delay값
    /// </summary>
	public static float delay = 0.01f;
    private static Coroutine coroutine;

    private static string fulltext;
    private string currentText;
    private static TextMeshProUGUI tb;

    private static bool text_exit;
    private static bool text_full;
    private static bool text_cut;
    /// <summary>
    /// typing 효과를 주는 coroutine을 시작하는 trigger
    /// </summary>
    private static bool trigger = false;

    private float _emptyTextSize = 0f;

    private float widthRatio = 2.5f;
    private float heightRatio = 2.5f;
    /// <summary>
    /// 스크롤이 밀리는 현상을 없애기 위해 보정값 추가할지 여부
    /// </summary>
    private static bool correction = true;

    // Start is called before the first frame update
    void Start()
    {
        currentID = "M0_0";
        correction = true;
        AddScript();
    }

    /// <summary>
    /// 스크립트를 텍스트박스에 추가 및 스크롤과 그림에 대한 값 제어
    /// </summary>
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
                Scroll.instance.pos += imgarr[imgarr.Count-1].rectTransform.rect.height * imgarr[imgarr.Count-1].rectTransform.localScale.y + 200f;
                // ScrollAmount += 480f;
            }

            //Increase the amount of scrolling by the size of the text box
            Scroll.instance.pos += textBox[textBox.Count - 2].GetComponent<RectTransform>().rect.height * textBox[textBox.Count - 2].rectTransform.localScale.y + 200f;
            // ScrollAmount += textBox[textBox.Count-2].GetComponent<RectTransform>().rect.height * 0.555f;
            // Scroll.Instance.pos += 200f;
            // ScrollAmount += 200f;
            AddEmpty();//add empty text
            // Scroll.Instance.scrollAmount = ScrollAmount;
            if (correction)    //보정값 추가
            {
                Debug.Log("보정값 넣기");
                Scroll.instance.pos -= 20;
                correction = false;
            }
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
            correction = true; //이제 보정값을 추가해야 함.

            AddEmpty();
            Scroll.instance.ScrollReset();
        }
        NextContainer.instance.NextChoice = script.next;
        currentID = script.id;
    }

    /// <summary>
    /// 아이디가 바뀔 때 씬의 텍스트 박스 모두 제거하여 씬 초기화
    /// </summary>
    public void DestroyScript()
    {
        for(int i = 0; i < textBox.Count(); i++){
            if(textBox[i] != null)
                Destroy(textBox[i].gameObject);
        }
        textBox.Clear();
    }

    /// <summary>
    /// 그림 추가
    /// </summary>
    public void AddPicture(Sprite picture)
    {
        int screen_width = Screen.width;
        int screen_height = Screen.height;

        Image img = Instantiate(Resources.Load<Image>("Prefab/Image"));
        img.sprite = picture;
        //img.rectTransform.localScale =
        //    new Vector3(img.rectTransform.localScale.x - 0.4f, img.rectTransform.localScale.y - 0.4f);
        img.transform.SetParent(GameObject.Find("Content").transform, true);
        img.SetNativeSize();

        float width = img.rectTransform.rect.width;
        float height = img.rectTransform.rect.height;

        if ((width >= screen_width && height >= screen_height && width-screen_width >= height-screen_height)||
            (width >= screen_width && height < screen_height)||
            (width < screen_width && height < screen_height && screen_width-width <= screen_height-height))
        {
            img.rectTransform.sizeDelta = new Vector2(screen_width, height*screen_width/width);
        }
        else if ((width >= screen_width && height >= screen_height && width-screen_width < height-screen_height)||
                (width < screen_width && height >= screen_height)||
                (width < screen_width && height < screen_height && screen_width-width > screen_height-height))
        {
            img.rectTransform.sizeDelta = new Vector2(width*screen_height/height, screen_height);
        }
        else
        {
            img.rectTransform.sizeDelta = new Vector2(screen_width, height*screen_width/width);
            Debug.Log("Unknown Case");
        }
        imgarr.Add(img);
        //Scroll.instance.pos += img.rectTransform.rect.height * img.rectTransform.localScale.y + 200f;
    }

    /// <summary>
    /// 아이디가 바뀔 때 씬의 그림 모두 제거하여 씬 초기화
    /// </summary>
    public void DestroyPicture()
    {
        for(int i = 0; i < imgarr.Count();i++){
            Destroy(imgarr[i].gameObject);
        }
        imgarr.Clear();
    }

    /// <summary>
    /// empty text 추가하여 텍스트가 화면 상단에 오게 content 크기 조절
    /// </summary>
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

    /// <summary>
    /// 이미 사용한 empty text 제거
    /// </summary>
    public void DestroyEmpty()
    {
        if(empty != null)
        {
            Destroy(empty.gameObject);
            empty = null;
        }
    }
    
    /// <summary>
    /// 선택지를 선택했을 때 stat update 동작 (excel 파일에서 받아옴)
    /// </summary>
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

    /// <summary>
    /// 타이핑 시작
    /// </summary>
    /// <param name="_fullText">타이핑 효과를 줄 텍스트의 문자열</param>
    /// <param name="textbox">텍스트를 넣을 textbox</param>
    public void Get_Typing(string _fullText, TextMeshProUGUI textbox)
    {
        text_exit = false;
        text_full = false;
        text_cut = false;

        fulltext = _fullText;
        tb = textbox;

        trigger = true;  //if trigger is true, coroutine start at update fuction
    }

    /// <summary>
    /// 타이핑 효과 즉시 종료
    /// </summary>
    public void End_Typing()
    {
        if (!text_full)
        {
            text_cut = true;
        }
    }
    /// <summary>
    /// 타이핑 효과 coroutine
    /// </summary>
    /// <param name="_fullText">타이핑 효과를 줄 텍스트의 문자열</param>
    /// <param name="textbox">텍스트를 넣을 textbox</param>
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

    /// <summary>
    /// 타이핑 효과 coroutine 제어 및 타이핑 효과 스킵시 동작
    /// </summary>
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
