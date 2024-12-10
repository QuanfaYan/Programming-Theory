using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public GameObject[] linesProgress;
    public LineManage[] lines;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    private TextMeshProUGUI[] linesProgressText;
    private Slider[] linesProgressBar;
    public static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        winText.enabled = false;
        SetWinText(false);
        SetLoseText(false);

        linesProgressText = new TextMeshProUGUI[linesProgress.Length];
        linesProgressBar = new Slider[linesProgress.Length];

        for (int i = 0; i < linesProgress.Length; i++)
        {
            linesProgressText[i] = linesProgress[i].GetComponentInChildren<TextMeshProUGUI>();
            linesProgressBar[i] = linesProgress[i].GetComponentInChildren<Slider>();
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foodText.SetText("Food:" + GameManager.instance.food);
        for(int i = 0; i < linesProgress.Length; i++)
        {
            int val = lines[i].GetBuildProgress();
            if(val != linesProgressBar[i].value)
            {
                linesProgressBar[i].value = val;
                linesProgressText[i].SetText("Lines" + (i + 1) + ": " + val + "%");
            }
        }
    }

    public void SetWinText(bool val)
    {
        winText.enabled = val;
    }

    public void SetLoseText(bool val)
    {
        loseText.enabled = val;
    }
}
