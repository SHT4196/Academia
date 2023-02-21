using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuLikeable : MonoBehaviour
{

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
   
  
}
