using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Image> health;
    [SerializeField] private List<Image> mental;
    private GameObject _force;
    private GameObject _intellect;
    private GameObject _mana;

    [SerializeField] private Text forceAmount;
    [SerializeField] private Text manaAmount;
    [SerializeField] private Text intellectAmount;
    [SerializeField] private Image diepanel;

    [FormerlySerializedAs("stat_fill")] [SerializeField] private Image[] statFill = new Image[3];    //force, intel, poli ������ ���
    
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
        for(int i = 0; i < 5; i++)
        {
            health.Add(healthGo.transform.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < 5; i++)
        {
            mental.Add(mentalGo.transform.GetChild(i).GetComponent<Image>());
        }
        _force = GameObject.Find("Force");
        _intellect = GameObject.Find("Intellect");
        _mana = GameObject.Find("Mana");
        forceAmount = _force.transform.GetChild(2).GetComponent<Text>();
        manaAmount = _mana.transform.GetChild(2).GetComponent<Text>();
        intellectAmount = _intellect.transform.GetChild(2).GetComponent<Text>();
        statFill[0] = _force.transform.GetChild(1).GetComponent<Image>();
        statFill[1] = _intellect.transform.GetChild(1).GetComponent<Image>();
        statFill[2] = _mana.transform.GetChild(1).GetComponent<Image>();
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
    /// <param name="value">health 또는 mental 수치</param>
    public void ImgChange(int state, int value)
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
    /// <summary>
    /// player 상태 이미지 set 함수
    /// </summary>
    /// <param name="health">health 수치</param>
    /// <param name="mental">mental 수치</param>
    public void ImgSet(int health, int mental)
    {
        ImgChange(0, health);
        ImgChange(1, mental);
    }
    /// <summary>
    /// 플레이어 능력치 Change fillamount
    /// </summary>
    /// <param name="ability">능력치 </param>
    /// <param name="value">능력치 수치</param>
    public void changeability_amount(PlayerAbility ability, int value)
    {
        if (ability == PlayerAbility.Force)
        {
            forceAmount.text = value.ToString();
            statFill[0].fillAmount = value / 20.0f;
        }
        else if (ability == PlayerAbility.Intellect)
        {
            intellectAmount.text = value.ToString();
            statFill[1].fillAmount = value / 20.0f;
        }
        else if (ability == PlayerAbility.Mana)
        {
            manaAmount.text = value.ToString();
            statFill[2].fillAmount = value / 20.0f;
        }
    }
    /// <summary>
    /// 죽거나 씬 바뀜
    /// </summary>
    public void DieAndSceneChange()
    {
        //Application.Quit();
        SceneManager.LoadScene(1);
        //EditorApplication.Exit(0);
        Achivement.Acv.change_scene();
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

