using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maintain_GameDatamanager : MonoBehaviour
{
    public static Maintain_GameDatamanager instance = null;
    public GameObject LoveRefuseDataObject;
    private bool isMoved = false;
    private void Awake()
    {
        Debug.Log("[[Maintain_GameDatamanager]]Awake ù Origin������ ���� �����ϰ��ִ� ��ü�����ϰ�, �� ������ ��ġ�ߴٰ� �����ÿ�" +
            "�� ������ �ٽ� ���ƿ;߸� �̰� �ٽ� ����ǰ�, Origin�� ���ķ� �������� �ʰ� ��� ����");
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    //���� ������ �� �̵��� �����ϰ��ִ� ���� ������(����Ƚ��)
    public int RefuseCount = 0;
    public bool[] visit_planetList;

    public void SetRefuseCount(int amount)
    {
        RefuseCount += amount;
    }
    public int GetRefuseCount()
    {
        return RefuseCount;
    }

    private void Update()
    {
        LoveRefuseDataObject.GetComponent<TMP_Text>().text = "������� Ƚ��:" + RefuseCount;

        if(RefuseCount >= 3)
        {
            if (!isMoved)
            {
                Debug.Log("[[Maintain_GameDatamanager]] 3�� �� ���� �Ѱ�� ���þ����� ������>>");
                SceneManager.LoadScene("LonelyEnding");//3�� ��� ������ ��� ������.
            }

            isMoved = true;
        }
    }
}
