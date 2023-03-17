using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [FormerlySerializedAs("EndingText")] [SerializeField] private TextMeshProUGUI endingText;

    [FormerlySerializedAs("MainMenuBtn")] [SerializeField] private Button mainMenuBtn;
    private Script _endingScript;

    private bool _isReadyToType = false;

    private int _textLength;

    private int _textNowIndex = 0;
    private string _endingTempText = "";

    private bool _isTypeDone = false;

    public AudioSource bgmSource;
    public AudioSource endingSource;
    public AudioClip endingClip;
    // Start is called before the first frame update
    void Start()
    {
        bgmSource.Stop();
        Debug.Log("started ending");
        endingSource.PlayOneShot(endingClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReadyToType)
        {
            StartCoroutine(nameof(Type));
            _isReadyToType = false;
            // SoundManger.stopAudio();
        }

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            if (_isTypeDone)
            {
                mainMenuBtn.gameObject.SetActive(true);
            }
            else
            {
                StopCoroutine(nameof(Type));
                endingText.text = _endingScript.text;
                _isTypeDone = true;
            }
        }
    }

    public void ToMainMenu()
    {
        Player.instance.ResetPlayer();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Tutorial", 1);
        endingSource.Stop();
        GameObject.Find("Content").GetComponent<AddText>().DestroySpace();
        GameObject.Find("Content").GetComponent<AddText>().DestroyPicture();
        Player.instance.IsPlayerReset = true;
        SceneManager.LoadScene(3);
    }

    public void SetEndingScript(Script endingScript)
    {
        _endingScript = endingScript;
        _textLength = _endingScript.text.Length;
        _isReadyToType = true;
    }

    IEnumerator Type()
    {
        while (_textNowIndex < _textLength)
        {
            _endingTempText += _endingScript.text[_textNowIndex];
            endingText.text = _endingTempText;
            _textNowIndex++;
            yield return new WaitForSeconds(0.1f);
        }

        _isTypeDone = true;
    }
}
