using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    private static EndingManager _instance;

    public Canvas EndingCanvasK;
    public Canvas EndingCanvasY;
    public Canvas EndingCanvasB1;
    public Canvas EndingCanvasB2;
    public static EndingManager instance
    {
        get
        {
            if (null == _instance)
                return null;
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (null == instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // endingCanvas = GameObject.Find()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
