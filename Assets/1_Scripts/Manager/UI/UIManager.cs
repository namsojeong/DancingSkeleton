using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    GameObject setting;
    [SerializeField]
    Text wowText;

    public bool isSetting = false;

    //���� ���� �ݱ�
    public void Setting()
    {
        isSetting = isSetting ? false : true;
        setting.SetActive(isSetting);
    }

    public void OpenUI(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseUI(GameObject obj)
    {
        obj.SetActive(false);
    }

    //����
    public void Quit()
    {
        Application.Quit();
    }

    //Ŭ�� �ؽ�Ʈ ȿ��
    public void Click(string str)
    {
        wowText.DOFade(0.3f, 0f);
        wowText.text = string.Format(str);
        wowText.DOFade(0f, 0.5f);
    }
}
