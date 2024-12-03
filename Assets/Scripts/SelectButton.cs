using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public GameObject humanPrefab = null;
    private static SelectButton[] selectButtons;
    private Image buttonImg;
    private int cost = 0;
    // Start is called before the first frame update
    void Start()
    {
        selectButtons = FindObjectsByType<SelectButton>(FindObjectsSortMode.None);
        buttonImg = GetComponent<Image>();
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach(SelectButton sButton in selectButtons)
            {
                sButton.SetDefaultColor();
            }
            SetGreen();
            GameManager.instance.currentSelectPrefab = humanPrefab;
        });

        if(humanPrefab)
        {
            cost = humanPrefab.GetComponent<Human>().cost;
        }
        else
        {
            cost = 0;
        }
        transform.Find("Cost").GetComponent<TextMeshProUGUI>().SetText(cost.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGreen()
    {
        buttonImg.color = new Color(0f, 1f, 0f, 1f);
    }

    public void SetDefaultColor()
    {
        buttonImg.color = new Color(1f, 1f, 1f, 1f);
    }
    

}
