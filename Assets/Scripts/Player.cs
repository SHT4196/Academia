using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Player 스탯 Enum class
/// </summary>
public enum PlayerAbility
{
    Force,
    Intellect,
    Mana
}

public enum PlayerDepartment
{
    Knight,
    Wizard,
    Politics
}

/// <summary>
/// Player Singleton class
/// </summary>
public class Player
{
    // Player instance
    private static Player _instance;
    


    public static Player instance
    {
        get
        {
            if (null == _instance)
                _instance = new Player();
            return _instance;
        }
    }

    /// <summary>
    /// player health 수치: 0 ~ 5
    /// </summary>
    private int _health;
    /// <summary>
    /// player mental 수치: 0 ~ 5
    /// </summary>
    private int _mental; 
    /// <summary>
    /// player 무력 수치
    /// </summary>
    private int _force;
    /// <summary>
    /// player 지력 수치
    /// </summary>
    private int _intellect;
    /// <summary>
    /// player 마력 수치
    /// </summary>
    private int _mana;

    /// <summary>
    /// player 학부
    /// </summary>
    private PlayerDepartment _department;
    
    /// <summary>
    /// player 이름
    /// </summary>
    private string _playerName;


    public Dictionary<string, int> _likeableDic = new Dictionary<string, int>()
    {
        {"서", 0},
        {"연", 0},
        {"고", 0},
        {"강", 0},
        {"성", 0},
        {"중", 0},
        {"한", 0}

    };
    public Dictionary<string, string> fullName = new Dictionary<string, string>()
    {
        {"서", "서울"},
        {"연", "연세"},
        {"고", "고려"},
        {"강", "서강"},
        {"성", "성균관"},
        {"중", "중앙"},
        {"한", "한양"}

    };


   
    private PlayerManager _gmr;

    public bool isAdmin { get; set; }
    
    private static readonly List<Pocket> EntirePocket = new List<Pocket>();
    private static readonly List<Ability> AbilityPocket = new List<Ability>();
    private static readonly List<Item> ItemPocket = new List<Item>();
    private static readonly List<State> StatePocket = new List<State>();

    private Pocket[] _entirePocketSort;
    private Ability[] _abilityPocketSort;
    private Item[] _itemPocketSort;
    private State[] _statePocketSort;
    public bool Sortbytime = true;
    
    /// <summary>
    /// player 재시작 여부
    /// </summary>
    public bool IsPlayerReset = false;

    private Player()
    {
        if (isAdmin)
        {
            return;
        }
        // gmr 및 player 기본 health, mental 수치 초기화 or 저장 값 불러오기
        _health = PlayerPrefs.GetInt("Health") == 0 ? 5 : PlayerPrefs.GetInt("Health");
        _mental = PlayerPrefs.GetInt("Mental") == 0 ? 5 : PlayerPrefs.GetInt("Mental");
        
        // player force, intellect, mana 수치 초기화 or 저장 값 불러옴
        _force = PlayerPrefs.GetInt("Force") == 0 ? 2 : PlayerPrefs.GetInt("Force");
        _intellect = PlayerPrefs.GetInt("Intellect") == 0 ? 2 : PlayerPrefs.GetInt("Intellect");
        _mana = PlayerPrefs.GetInt("Mana") == 0 ? 2 : PlayerPrefs.GetInt("Mana");
        
        //player 이름 설정
        _playerName = PlayerPrefs.GetString("PlayerName");

        if (PlayerPrefs.GetString("Department") == "Knight")
            _department = PlayerDepartment.Knight;
        else if (PlayerPrefs.GetString("Department") == "Wizard")
            _department = PlayerDepartment.Wizard;
        else if (PlayerPrefs.GetString("Department") == "Politics")
            _department = PlayerDepartment.Politics;
    }

    /// <summary>
    /// Set _gmr variable
    /// </summary>
    public void SetGameManager()
    {
        _gmr = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        _gmr.ImgSet(_health, _mental);
        _gmr.changeability_amount(PlayerAbility.Force, _force);
        _gmr.changeability_amount(PlayerAbility.Intellect, _intellect);
        _gmr.changeability_amount(PlayerAbility.Mana, _mana);
        _gmr.likeable_amount();

        
    }
    
    /// <summary>
    /// Player Name 설정
    /// </summary>
    /// <param name="name">설정할 Player 이름</param>
    public void SetPlayerName(string name)
    {
        _playerName = name;
        DatabaseManager.Instance.SetPlayerName_DB(_playerName, SystemInfo.deviceUniqueIdentifier);
        PlayerPrefs.SetString("PlayerName", name);
    }

    /// <summary>
    /// Get Player Name
    /// </summary>
    /// <returns></returns>
    public string GetPlayerName()
    {
        return _playerName;
    }

    public void SetAdminVersion(int health, int mental, int force, int intellect, int mana, string playerName)
    {
        _health = health;
        _mental = mental;
        _force = force;
        _intellect = intellect;
        _mana = mana;
        _playerName = playerName;
    }
    /// <summary>
    /// Player 능력치, 스탯 reset (게임 오버 후 실행)
    /// </summary>
    public void ResetPlayer()
    {
        _gmr = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        
        _health = 5;
        _mental = 5;
        _gmr.ImgSet(_health, _mental);
        
        _force = 2;
        _intellect = 2;
        _mana = 2;

        _likeableDic["서"] = 0;
        _likeableDic["연"] = 0;
        _likeableDic["고"] = 0;
        _likeableDic["강"] = 0;
        _likeableDic["성"] = 0;
        _likeableDic["중"] = 0;
        _likeableDic["한"] = 0;


        _gmr.changeability_amount(PlayerAbility.Force, _force);
        _gmr.changeability_amount(PlayerAbility.Intellect, _intellect);
        _gmr.changeability_amount(PlayerAbility.Mana, _mana);
        _gmr.likeable_amount();
    }

    /// <summary>
    /// player의 health 수치 변경
    /// </summary>
    /// <param name="value">변경할 값</param>
    public void HealthChange(int value)
    {
        if (_health > 0)
        {
            this._health += value;
            _gmr.ImgChange(0, value, this._health);
            if (this._health >= 5)
                this._health = 5;
            if (isAdmin)
            {
                return;
            }
            PlayerPrefs.SetInt("Health", this._health); //변경된 health값 저장
        }
        if (_health <= 0)
            this.Die();
    }

    /// <summary>
    /// likeableDictionary에서 해당 key값이 valuable한지 확인
    /// </summary>
    /// <param name="key">확인하고자하는 key값</param>
    /// <returns></returns>
    public bool CheckKeyAvailable(string key)
    {
        return _likeableDic.ContainsKey(key);
    }

    /// <summary>
    /// 호감도 수치 변경 함수
    /// </summary>
    /// <param name="key">변경하고자 하는 호감도 key</param>
    /// <param name="value">변경할 값</param>
    public void ChangeLikeable(string key, int value)
    {
        if (!CheckKeyAvailable(key))
            return;
        int nowValue;
        _likeableDic.TryGetValue(key, out nowValue);
        _likeableDic[key] = nowValue + value;
        Debug.Log($"서: {_likeableDic["서"]}, 연: {_likeableDic["연"]}, 고: {_likeableDic["고"]}");
        _gmr.likeable_amount();

    }
    /// <summary>
    /// player의 mental 수치 변경
    /// </summary>
    /// <param name="value">변경할 값</param>
    public void MentalChange(int value)
    {
        if (_mental > 0)
        {
            this._mental += value;
            _gmr.ImgChange(1, value, this._mental);
            if (this._mental >= 5)
                this._mental = 5;
            if (!isAdmin)
            {
                PlayerPrefs.SetInt("Mental", this._mental); //변경된 mental 값 저장
            }
        }
        if (_mental <= 0)
            this.Die();
    }

    /// <summary>
    /// player 능력치 수치 변경
    /// </summary>
    /// <param name="ability">변경할 능력</param>
    /// <param name="value">변경 값</param>
    public void Changeability(PlayerAbility ability, int value) //value: Amount of ability Change
    {
        if (ability == PlayerAbility.Force)
        {
            this._force += value;
            _gmr.changeability_amount(ability, this._force);
            if (isAdmin)
            {
                return;
            }
            PlayerPrefs.SetInt("Force", this._force);
        }
        else if (ability == PlayerAbility.Intellect)
        {
            this._intellect += value;
            _gmr.changeability_amount(ability, this._intellect);
            if (isAdmin)
            {
                return;
            }
            PlayerPrefs.SetInt("Intellect", this._intellect);
        }
        else if (ability == PlayerAbility.Mana)
        {
            this._mana += value;
            _gmr.changeability_amount(ability, this._mana);
            if (isAdmin)
            {
                return;
            }
            PlayerPrefs.SetInt("Mana", this._mana);
        }
    }

    /// <summary>
    /// Die
    /// </summary>
    public void Die()
    {

        
        GameObject.Find("Content").GetComponent<AddText>().DestroySpace();
        GameObject.Find("Content").GetComponent<AddText>().DestroySpace();
        PlayerPrefs.DeleteAll(); //저장값 초기화
       // Achivement.Acv.nowupdate(8, 1); //죽었을 때 업적 
        AchievementManager.Instance.Achieve_achievement(1, 1);

        _gmr.DiepanelActive(); // die 창 활성화
        if (isAdmin)
        {
            return;
        }
        PlayerPrefs.DeleteAll(); // 저장값 초기화
        
        // 후에 health와 mental의 다른 대처? 
        // if(_health == 0)
        //     Debug.Log("Die");
        // else if(_mental == 0)
        //     Debug.Log("Die");
    }

    /// <summary>
    /// 선택지 선택 시 Pocket에 해당 element가 있는지 검사
    /// </summary>
    /// <param name="name">검사해야하는 element의 name</param>
    /// <returns>해당 element (없다면 null)</returns>
    public Pocket FindPocketElement(string name)
    {
        Pocket result = EntirePocket.Find( // finding element
            delegate (Pocket tmp)
            {
                return tmp.getName() == name;
            });
        if (result != null)
            return result;
        else
            return null;
    }
    
    /// <summary>
    /// 선택지 선택 시 현재 player가 선택가능한 선택지인지 판별
    /// </summary>
    /// <param name="ability">판별해야 하는 ability</param>
    /// <param name="value">해당 ability의 최소 능력치 수치</param>
    /// <returns>T/F</returns>
    public bool AbilityAvailable(PlayerAbility ability, int value)
    {
        return ability switch
        {
            PlayerAbility.Force => _force >= value,
            PlayerAbility.Intellect => _intellect >= value,
            PlayerAbility.Mana => _mana >= value,
            _ => throw new ArgumentOutOfRangeException(nameof(ability), ability, null)
        };
    }
    
    /// <summary>
    /// player 상태 다시 설정
    /// </summary>
    public void SetAgain() //Play Again
    {
        this._health = 5;
        this._mental = 5;
        _gmr.ImgChange(0, 0, _health);
        _gmr.ImgChange(1, 0, _mental);
    }

    /// <summary>
    /// 플레이어 학부 지정
    /// </summary>
    /// <param name="department">지정할 학부</param>
    public void SetPlayerDepartment(PlayerDepartment department)
    {
        _department = department;
        PlayerPrefs.SetString("Department", $"{_department}");
        Debug.Log($"선택한 학부: {_department}");
    }

    /// <summary>
    /// 플레이어 학부 리턴
    /// </summary>
    /// <returns>학부 </returns>
    public PlayerDepartment GetPlayerDepartment()
    {
        return _department;
    }
    
    /// <summary>
    /// Pocket에 element 넣기 (아이템 획득)
    /// </summary>
    /// <param name="element">넣을 아이템 Pocket</param>
    private static void PutPocketElements(Pocket element)
    {
        var result = EntirePocket.Find( // finding element
            tmp => tmp.getName() == element.getName());

        if(result != null) // element found
        {
            result.increseNum(); // 아이템 수 증가
        }
        else // element not found
        {
            //Put element into entirePocket list
            EntirePocket.Add(element);

            if (element.getType() == 0)
            {
                //Put element in to ability list
                AbilityPocket.Add(element as Ability);
            }
            else if (element.getType() == 1)
            {
                //Put element in to item list
                ItemPocket.Add(element as Item);
            }
            else if (element.getType() == 2)
            {
                StatePocket.Add(element as State);
            }
        }
    }

    /// <summary>
    /// Pocket에서 해당 element remove
    /// </summary>
    /// <param name="element">삭제할 element</param>
    private static void RemovePocketElement(Pocket element)
    {
        var result = EntirePocket.Find( // finding element
            tmp => tmp.getName() == element.getName());
        if(result != null)
        {
            //element found
            if(result.getNum() >= 2) // more than one element -> just decrease num
            {
                result.decreseNum();
            }
            else // just one element -> 1. remove from lists, 2. destroy object
            {
                //Remove From Lists
                EntirePocket.Remove(result);
                AbilityPocket.Remove(result as Ability);
                ItemPocket.Remove(result as Item);
                StatePocket.Remove(result as State);

                //Destroy Obj
                result = null;
                GC.Collect();
            }
        }
        else
        {
            // element not found
            // do nothing.. maybe?
        }
    }
    
    public static void Test() // TEST
    {
        PutPocketElements(new Ability("�丮�Ƿ�", 1));
        PutPocketElements(new Item("���ݸ�", 1));
        PutPocketElements(new State("����", 1));
        PutPocketElements(new State("����", 1));
        PutPocketElements(new State("����", 1));
        RemovePocketElement(new Item("���ݸ�", 1));
        RemovePocketElement(new State("����", 1));
        PutPocketElements(new State("����", 1));
        PutPocketElements(new State("����", 1));
        PutPocketElements(new Ability("�ٷ�", 1));
        PutPocketElements(new Item("������", 1));
        //Changeability(player_ability.force, 2);
        //Changeability(player_ability.intellect, 2);
        //Changeability(player_ability.mana, 2);

    }
    
    /// <summary>
    /// Pocket의 element name string return
    /// </summary>
    /// <param name="type">entire: 0, ability: 1, item: 2, state: 3</param>
    /// <returns></returns>
    public string SetStr(int type)
    {
        var pocketStr = "";
        if (Sortbytime)
        {
            switch (type)
            {
                // Entire
                case 0:
                {
                    foreach (var t in EntirePocket)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // Ability 
                case 1:
                {
                    foreach (var t in AbilityPocket)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // Item
                case 2:
                {
                    foreach (var t in ItemPocket)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // State
                case 3:
                {
                    foreach (var t in StatePocket)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
            }
        }
        else //sort by char
        {
            SortPocket();
            switch (type)
            {
                // Entire
                case 0:
                {
                    foreach (var t in _entirePocketSort)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // Ability 
                case 1:
                {
                    foreach (var t in _abilityPocketSort)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // Item
                case 2:
                {
                    foreach (var t in _itemPocketSort)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
                // State
                case 3:
                {
                    foreach (var t in _statePocketSort)
                    {
                        pocketStr += t.getNameStr() + "\n";
                    }

                    break;
                }
            }
        }
        return pocketStr;
    }
    /// <summary>
    /// Preview Pocket elements
    /// </summary>
    /// <returns></returns>
    public List<string> SetPreviewStr()
    {
        var preStrList = new List<string>();
        if(EntirePocket.Count <= 2)
        {
            foreach (var t in EntirePocket)
            {
                preStrList.Add(t.getNameStr());
            }
        }
        else
        {
            for(var i =0; i < 3; i++)
            {
                preStrList.Add(EntirePocket[i].getNameStr());
            }
        }
        return preStrList;
    }

    /// <summary>
    /// Pocket Sort
    /// </summary>
    private void SortPocket()
    {
        _entirePocketSort = new Pocket[EntirePocket.Count];
        _abilityPocketSort = new Ability[AbilityPocket.Count];
        _itemPocketSort = new Item[ItemPocket.Count];
        _statePocketSort = new State[StatePocket.Count];


        IComparer<Pocket> comparer = new PocketComparer();
        if (EntirePocket.Count != 0)
        {
            EntirePocket.CopyTo(_entirePocketSort);
            if (EntirePocket.Count >= 2)
                Array.Sort(_entirePocketSort, comparer);
        }
        if(AbilityPocket.Count != 0)
        {
            AbilityPocket.CopyTo(_abilityPocketSort);
            if(AbilityPocket.Count >= 2)
                Array.Sort(_abilityPocketSort, comparer);
        }
        if(ItemPocket.Count != 0)
        {
            ItemPocket.CopyTo(_itemPocketSort);
            if (ItemPocket.Count >= 2)
                Array.Sort(_itemPocketSort, comparer);
        }
        if(StatePocket.Count != 0)
        {
            StatePocket.CopyTo(_statePocketSort);
            if (StatePocket.Count >= 2)
                Array.Sort(_statePocketSort, comparer);
        }
    }

}

public class PocketComparer : IComparer<Pocket> //Pocket Comparer
{
    public int Compare(Pocket x, Pocket y)
    {
        return (x.CompareTo(y));
    }
}