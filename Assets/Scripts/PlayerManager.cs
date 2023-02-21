using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Serialization;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Image> health;
    [SerializeField] private List<Image> mental;
    private GameObject _force;
    private GameObject _intellect;
    private GameObject _mana;

    [SerializeField] private TextMeshProUGUI forceAmount;
    [SerializeField] private TextMeshProUGUI manaAmount;
    [SerializeField] private TextMeshProUGUI intellectAmount;

    [SerializeField] private Image diepanel;

    //[FormerlySerializedAs("stat_fill")] [SerializeField] private Image[] statFill = new Image[3];    //force, intel, poli ������ ���

    [SerializeField] private TextMeshProUGUI koreaAmount;
    [SerializeField] private TextMeshProUGUI seoulAmount;
    [SerializeField] private TextMeshProUGUI yonseiAmount;
    [SerializeField] private TextMeshProUGUI sogangAmount;
    [SerializeField] private TextMeshProUGUI hanyangAmount;
    [SerializeField] private TextMeshProUGUI chungangAmount;
    [SerializeField] private TextMeshProUGUI sungkyunkwanAmount;
    [SerializeField] private TextMeshProUGUI kyungheeAmount;
    [SerializeField] private TextMeshProUGUI uosAmount;
    [SerializeField] private TextMeshProUGUI hufAmount;


    // Start is called before the first frame update
    void Start()
    {
        
        //imgSet();
    }
    private void Awake()
    {
        //PlayerManager 및 Player 상태 초기화
        health = new List<Image>();
        mental = new List<Image>();
        GameObject healthGo = GameObject.FindWithTag("Health");
        GameObject mentalGo = GameObject.FindWithTag("Mental");
        for(int i = 0; i < 3; i++)
        {
            health.Add(healthGo.transform.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < 3; i++)
        {
            mental.Add(mentalGo.transform.GetChild(i).GetComponent<Image>());
        }
        _force = GameObject.Find("Force");
        _intellect = GameObject.Find("Intellect");
        _mana = GameObject.Find("Mana");
        forceAmount = _force.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        manaAmount = _mana.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        intellectAmount = _intellect.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        //statFill[0] = _force.transform.GetChild(1).GetComponent<Image>();
        //statFill[1] = _intellect.transform.GetChild(1).GetComponent<Image>();
        //statFill[2] = _mana.transform.GetChild(1).GetComponent<Image>();
        
        diepanel = GameObject.Find("Canvas").transform.Find("DiePanel").GetComponent<Image>();

        if (Player.instance.IsPlayerReset) // Player 상태 초기화
            Player.instance.ResetPlayer();
        
        Player.instance.SetGameManager();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 플레이어 상태 이미지 change 
    /// </summary>
    /// <param name="state">0: health, 1: mental</param>
    /// <param name="value">변화되는 양</param>
    /// <param name="result">변화된 결과값</param>
    public void ImgChange(int state, int value, int result)
    {
        //state 0: health, 1: mental
        if (state == 0)
        {
            //감소 애니메이션
            if (value < 0)
            {
                for (int i = 0; i < value * (-1); i++)
                {
                    int tempIndex = result - value - i - 1;
                    if (tempIndex >= 0)
                    {
                        health[tempIndex].transform.DOScale(0f, 0.5f).OnComplete(() =>
                        {
                            health[tempIndex].gameObject.SetActive(false);
                        });
                    }
                }
            }
            //증가 애니메이션
            else if (value > 0)
            {
                for (int i = 0; i < value; i++)
                {
                    int tempIndex = result - value + i;
                    if (tempIndex < 3)
                    {
                        health[tempIndex].gameObject.SetActive(true);
                        health[tempIndex].transform.DOScale(1.0f, 0.5f).OnComplete(() =>
                        {
                            health[tempIndex].transform.DOScale(0.5f, 0.25f);
                        });
                    }
                }
            }
            else
            {
                for (int i = 0; i < result; i++)
                    health[i].gameObject.SetActive(true);
                for (int i = 2; i >= result; i--)
                    health[i].gameObject.SetActive(false);
            }
        }
        else if (state == 1)
        {
            //감소 애니메이션
            if (value < 0)
            {
                for (int i = 0; i < value * (-1); i++)
                {
                    int tempIndex = result - value - i - 1;
                    if (tempIndex >= 0)
                    {
                        mental[tempIndex].transform.DOScale(0f, 0.5f).OnComplete(() =>
                        {
                            mental[tempIndex].gameObject.SetActive(false);
                        });
                    }
                }
            }
            //증가 애니메이션
            else if (value > 0)
            {
                for (int i = 0; i < value; i++)
                {
                    int tempIndex = result - value + i;
                    if (tempIndex < 3)
                    {
                        mental[tempIndex].gameObject.SetActive(true);
                        mental[tempIndex].transform.DOScale(1f, 0.25f).OnComplete(() =>
                        {
                            mental[tempIndex].transform.DOScale(0.5f, 0.25f);
                        });   
                    }
                }
            }
            else
            {
                for (int i = 0; i < result; i++)
                    mental[i].gameObject.SetActive(true);
                for (int i = 2; i >= result; i--)
                    mental[i].gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// player 상태 이미지 set 함수
    /// </summary>
    /// <param name="health">health 수치</param>
    /// <param name="mental">mental 수치</param>
    public void ImgSet(int health, int mental)
    {
        ImgChange(0, 0, health);
        ImgChange(1, 0, mental);
    }

    /// <summary>
    /// 플레이어 능력치 표시 함수
    /// </summary>
    /// <param name="ability">능력치 </param>
    /// <param name="value">능력치 수치</param>
    public void changeability_amount(PlayerAbility ability, int value)
    {
        if (ability == PlayerAbility.Force)
        {
            forceAmount.text = value.ToString();
            //statFill[0].fillAmount = value / 20.0f;
        }
        else if (ability == PlayerAbility.Intellect)
        {
            intellectAmount.text = value.ToString();
            //statFill[1].fillAmount = value / 20.0f;
            
        }
        else if (ability == PlayerAbility.Mana)
        {
            manaAmount.text = value.ToString();
            //statFill[2].fillAmount = value / 20.0f;
            
        }
       
    }

    public void likeable_amount()
    {
        int seo_value = Player.instance._likeableDic["서"];
        int yon_value = Player.instance._likeableDic["연"];
        int ko_value = Player.instance._likeableDic["고"];
        int gang_value = Player.instance._likeableDic["강"];
        int sung_value = Player.instance._likeableDic["성"];
        int han_value = Player.instance._likeableDic["한"];
        int chung_value = Player.instance._likeableDic["중"];
        int kyung_value = Player.instance._likeableDic["경"];
        int huf_value = Player.instance._likeableDic["H"];
        int uos_value = Player.instance._likeableDic["U"];

        seoulAmount.text = seo_value.ToString();
        yonseiAmount.text = yon_value.ToString();
        koreaAmount.text = ko_value.ToString();
        sogangAmount.text = gang_value.ToString();
        sungkyunkwanAmount.text = sung_value.ToString();
        hanyangAmount.text = han_value.ToString();
        chungangAmount.text = chung_value.ToString();
        kyungheeAmount.text = kyung_value.ToString();
        hufAmount.text = huf_value.ToString();
        uosAmount.text = uos_value.ToString();

    }




    /// <summary>
    /// 죽거나 씬 바뀜
    /// </summary>
    public void DieAndSceneChange()
    {
        //Application.Quit();
        SceneManager.LoadScene(2);
        //EditorApplication.Exit(0);
      //  Achivement.Acv.change_scene();
    }
    /// <summary>
    /// Player 죽을 때 - UI 상에서 죽는 버튼
    /// </summary>
    public void Die()
    {
       
        Player.instance.Die();
        
    }
    /// <summary>
    /// 죽은 후 Panel Activate
    /// </summary>
    public void DiepanelActive()
    {
        diepanel.gameObject.SetActive(true);
        Player.instance.IsPlayerReset = true;
        GameObject.Find("Content").GetComponent<AddText>().DestroyPicture();
    }
}

