using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LoveGameManager : MonoBehaviour
{
    private Player3d_Planet player;

    [SerializeField] private Image Character_Conversation_stateImage; //Ver1,2���� ����ϴ�,ĳ���͹��� ��ȭUI Sprite�̹���
    [SerializeField] private Image LoveMonsterState;//Ver1������ ����ϴ� �������UI �����̹���
    [SerializeField] private SpriteRenderer Sprite2DCharacterImage;//Ver1���� �ʻ��� 2D Sprite(Movement)

    [SerializeField] private Sprite[] images;

    [SerializeField] public Converstation activeConversation;//Ȱ��ȭ ��ȭ
    [SerializeField] public int activeConversationIndex = 0;//Ȱ��ȭ��ȭ�δ콺
    [SerializeField] public bool isVer2Env = false;
    [SerializeField] private Converstation[] conversations;
    [SerializeField] private GameObject gameoverCon;
    [SerializeField] private GameObject group1;
    [SerializeField] private GameObject group2;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();
        //conversations = FindObjectsOfType<Converstation>();

        if (isVer2Env)
        {
            Array.Sort(conversations, (obj1, obj2) => obj1.ConversationIndex.CompareTo(obj2.ConversationIndex));
            for (int e = 0; e < conversations.Length; e++)
            {
                Debug.Log(e + "| [[LoveGameManager]] �ʱ�ȭ Conversations��ü�� Index������� ���� indexValue:" + conversations[e].ConversationIndex);
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isVer2Env)
        {
            activeConversation = conversations[activeConversationIndex];
        }

        if (player.LoveScore < -10)
        {
           // Debug.Log("[[LoveGameManager]]����ȭ��");
            if (Sprite2DCharacterImage != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                Sprite2DCharacterImage.sprite = images[0];
            }
            if (LoveMonsterState != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                LoveMonsterState.sprite = images[0];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//����Ver ���� �༺�� ����
                {
                    //�ٸ� ĳ����ȭ�ڿ� �����ϴ� ĳ�����̹���
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //�÷��̾ �ƴ� ĳ����(���͵�)�� ���ϴ°�쿡�� �������� ��ȭui ȭ�� �̹��� Swap
                        Character_Conversation_stateImage.sprite = images[0];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] ���� Conversation name�� �ش� ��ü���� ���� ���ϰ� �ִ� ȭ��" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] ȭ�ڰ� �÷��̾��� ���� ���� �Ҵ� ������ ���� �ʴ� ��ü�� ����̴�");
                    }
                }
                else//�缺Ver ���� �༺�� ����
                {
                    Character_Conversation_stateImage.sprite = images[0];
                }
            }
        }
        else if(player.LoveScore >=-10 && player.LoveScore< 0)
        {
            //Debug.Log("[[LoveGameManager]]���ͳ��");
            if (Sprite2DCharacterImage != null)
            { 
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                Sprite2DCharacterImage.sprite = images[1];
            }
            if (LoveMonsterState != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                LoveMonsterState.sprite = images[1];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//����Ver ���� �༺�� ����
                {
                    //�ٸ� ĳ����ȭ�ڿ� �����ϴ� ĳ�����̹���
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //�÷��̾ ���ϰ� �ִ� ��쿡�� �÷��̾ ������ Sprite�̹�����
                        //���� ���� Swap�̹����� �ٲ��� �ʴ´�.
                        Character_Conversation_stateImage.sprite = images[1];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] ���� Conversation name�� �ش� ��ü���� ���� ���ϰ� �ִ� ȭ��" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] ȭ�ڰ� �÷��̾��� ���� ���� �Ҵ� ������ ���� �ʴ� ��ü�� ����̴�");
                    }
                }
                else//�缺Ver ���� �༺�� ����
                {
                    Character_Conversation_stateImage.sprite = images[1];
                }
            }
        }
        else if(player.LoveScore >=0 && player.LoveScore < 10)
        {
           // Debug.Log("[[LoveGameManager]]��������");
            if (Sprite2DCharacterImage != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                Sprite2DCharacterImage.sprite = images[2];
            }
            if (LoveMonsterState != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                LoveMonsterState.sprite = images[2];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//����Ver ���� �༺�� ����
                {
                    //�ٸ� ĳ����ȭ�ڿ� �����ϴ� ĳ�����̹���
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //�÷��̾ ���ϰ� �ִ� ��쿡�� �÷��̾ ������ Sprite�̹�����
                        //���� ���� Swap�̹����� �ٲ��� �ʴ´�.
                        Character_Conversation_stateImage.sprite = images[2];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] ���� Conversation name�� �ش� ��ü���� ���� ���ϰ� �ִ� ȭ��" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] ȭ�ڰ� �÷��̾��� ���� ���� �Ҵ� ������ ���� �ʴ� ��ü�� ����̴�");
                    }
                }
                else//�缺Ver ���� �༺�� ����
                {
                    Character_Conversation_stateImage.sprite = images[2];
                }
            }
        }
        else if (player.LoveScore >= 10 && player.LoveScore < 26)
        {
           // Debug.Log("[[LoveGameManager]]����������");
            if (Sprite2DCharacterImage != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                Sprite2DCharacterImage.sprite = images[3];
            }
            if (LoveMonsterState != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                LoveMonsterState.sprite = images[3];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//����Ver ���� �༺�� ����
                {
                    //�ٸ� ĳ����ȭ�ڿ� �����ϴ� ĳ�����̹���
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //�÷��̾ ���ϰ� �ִ� ��쿡�� �÷��̾ ������ Sprite�̹�����
                        //���� ���� Swap�̹����� �ٲ��� �ʴ´�.
                        Character_Conversation_stateImage.sprite = images[3];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] ���� Conversation name�� �ش� ��ü���� ���� ���ϰ� �ִ� ȭ��" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] ȭ�ڰ� �÷��̾��� ���� ���� �Ҵ� ������ ���� �ʴ� ��ü�� ����̴�");
                    }
                }
                else//�缺Ver ���� �༺�� ����
                {
                    Character_Conversation_stateImage.sprite = images[3];
                }
            }
        }
        else if (player.LoveScore >= 26)
        {
            //Debug.Log("[[LoveGameManager]]���ͱ��");
            if (Sprite2DCharacterImage != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                Sprite2DCharacterImage.sprite = images[4];
            }
            if (LoveMonsterState != null)
            {
                //������ ���� �̹���(�ٸ�ĳ����ȭ�ڿ� �������� ����)
                LoveMonsterState.sprite = images[4];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//����Ver ���� �༺�� ����
                {
                    //�ٸ� ĳ����ȭ�ڿ� �����ϴ� ĳ�����̹���
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //�÷��̾ ���ϰ� �ִ� ��쿡�� �÷��̾ ������ Sprite�̹�����
                        //���� ���� Swap�̹����� �ٲ��� �ʴ´�.
                        Character_Conversation_stateImage.sprite = images[4];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] ���� Conversation name�� �ش� ��ü���� ���� ���ϰ� �ִ� ȭ��" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] ȭ�ڰ� �÷��̾��� ���� ���� �Ҵ� ������ ���� �ʴ� ��ü�� ����̴�");
                    }
                }
                else//�缺Ver ���� �༺�� ����
                {
                    Character_Conversation_stateImage.sprite = images[4];
                }
            }
        }

      
        if (player.LoveScore >= 10)
        {
            //Debug.Log("���ͱ�� �ḻ ��� �ḻ ������ �̵� ����!!!");//DEBUG
        }
        else if(player.LoveScore < -8)
        {
            //Debug.Log("����ȭ�� �ḻ ȭ�� �ḻ ������ �̵� ����!!!");//DEBUG
        }           
    }

    public void GameOver()
    {
        Debug.Log("���ӿ���>>");      
        gameoverCon.SetActive(true);
        group1.SetActive(false);
        group2.SetActive(false);        
    }
    public void ReloadNowScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GotoStartScene()
    {
        SceneManager.LoadScene("RoadViewBase_SpaceTour");
    }

    public void BacktoMoveScene()
    {
        SceneManager.LoadScene("RoadViewBase_SpaceTour");
    }
}
