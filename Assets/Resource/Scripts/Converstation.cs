using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Sprite cg;
    // public Image name;
    public Sprite speakerShape;//ȭ�ڸ��� �ٸ� ������ ȭ��ǥ,�׵θ�,�̸�
    [TextArea]
    public string dialogue;
    
    //public Sprite leftcg;
    //��ũ��Ʈ���� �������� �ٲ�°���(��ȭUI Sprite�̹���),
    //Inspector���� �Է��� ������ ���������� �ٲ�°�������
    public bool isDynamicCg = false;
}
public class Converstation : MonoBehaviour
{
    [SerializeField] Image sprite_CG;//ĳ���� ��ȭâ ����sprite�̹���(LoveGameManager���� ����)
    [SerializeField] Image sprite_Box;
    [SerializeField] Image img_Speaker;//ȭ�ڹڽ�(�׵θ�,ȭ��ǥ,�̸�)
    [SerializeField] TMP_Text txt_Dialogue;
    //[SerializeField] GameObject nameBox;
    [SerializeField] Image thinkCloud;//���(�̴ϰ���)������ ���Ǵ� ��ǳ���̹���
    [SerializeField] bool isNext = false;
    [SerializeField] int dialogueCnt = 0;
    [SerializeField] Dialogue[] dialogue;
    [SerializeField] public Player3d_Planet playercameracontroll;//��ȭ�� �����߿��� ī�޶�(�÷��̾�)��Ʈ�� ���� ����
    public GameObject GameOverPanel;
    public GameObject goodEndingPanel;

    //��ǳ��(�����׸�ã���� ��ǳ��)�� ���̱� ������ Conversation index��
    [SerializeField] private int ViewThinkCloud_startIndex;
    [SerializeField] private bool isCompleted = false;

    //Ŭ��Ȱ��ȭ ��ų ������Ʈ(���̾Ʒα� �����ÿ� Ư�� Ȱ��ȭ��ų)
    [SerializeField] public ClickEvent targetactive_ClickEvent;

    [SerializeField] public bool isCompleted_NextDialog = false;
    [SerializeField] GameObject Target_Dialogue;//���̾Ʒα׿��� -> ���� ���̾Ʒα�

    [SerializeField] public bool isAction_TargetTutorial = false;
    [SerializeField] public TutorialControllerVer2 TargetTutorial;//��ȭ �������ó� or Ư����Ȳ�� ������ Ÿ�� Ʃ�丮��
    [SerializeField] public GameObject OXQuizPanel;

    [SerializeField] public bool isLastDialog = false;//������(�� ����,result)�� �������� ���� ������ ��ȭ ���̾Ʒα� ���� ����

    [SerializeField] public bool isPlayerSpeak=true;//�÷��̾ Inspector���� ������ Sprite(��������)���� ���ϰ��ִ°��,���� �� �ٸ�ĳ���Ͱ� ���Ҷ� Sprite��������
    [SerializeField] public string NowSpeakername = "";//�� ��ü���� ���� ���ϰ� �ִ� ���(ȭ��)

    public int ConversationIndex = 0;//�ش� ��ü�� �δ콺��(�� ��ü ��ü�� index ��) ���� Conversation��ü���� ��ȣ ���� ����>

    public LoveGameManager lovegameManager;

    [SerializeField] private bool isDynamicScene = false;
    //[SerializeField] private string HappyEndingSceneName;
    //[SerializeField] private string SadEndingSceneName;//��������(������ �������) �������� ����

    public Maintain_GameDatamanager maintain_gameDatamanager;
    public GameObject MarryMeSuggestUI;

    [SerializeField] private int mother_planetIndex;
    // Start is called before the first frame update
    void Start()
    {
        dialogueCnt = 0;
        playercameracontroll = FindObjectOfType<Player3d_Planet>();
        maintain_gameDatamanager = FindObjectOfType<Maintain_GameDatamanager>();
        lovegameManager = FindObjectOfType<LoveGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext) //Ȱ��ȭ�� �Ǿ��� ���� ��簡 ����ǵ���
        {
            if (dialogueCnt >= ViewThinkCloud_startIndex)
            {
                if (thinkCloud != null && isAction_TargetTutorial == true)
                {
                    thinkCloud.gameObject.SetActive(true);//����� ��ǳ���� ���̰� ������ ��ȭindex��
                }
            }
            if (dialogueCnt > dialogue.Length) // ������ ����
            {
                ONOFF(false); // ��簡 ����
                return; // �� �̻� �������� �ʵ��� ��ȯ
            }
        }
    }

    public void ShowDialogue() //����ƼUI��ȭâ Ŭ���ÿ� ���� ��ȭ ����ǵ���
    {
        ONOFF(true); //��ȭ�� ���۵�
        //dialogueCnt = 0;
        NextDialogue(); //ȣ����ڸ��� ��簡 ����� �� �ֵ��� 
    }
    private void ONOFF(bool _flag)
    {
        sprite_Box.gameObject.SetActive(_flag);//��ȭâ�ڽ�Ȱ��ȭ
        sprite_CG.gameObject.SetActive(_flag);//��ȭâ����SpriteȰ��ȭ
        txt_Dialogue.gameObject.SetActive(_flag);//��ȭ�ؽ�ƮȰ��ȭ
        //nameBox.gameObject.SetActive(_flag);//ȭ���̸��ڽ�Ȱ��ȭ

        if (thinkCloud != null && isAction_TargetTutorial == true)
        {
            thinkCloud.gameObject.SetActive(false);//��ǳ�� �⺻�� �׻� ����
        }

        if (isCompleted)
        {
            //�� Dialogue �б��� �������� ��������� false�� ���ߴ� ����
            if (thinkCloud != null && isAction_TargetTutorial == true)
            {
                thinkCloud.gameObject.SetActive(_flag);//��ǳ�� ��ȭ �������� ��������� ���߱�
            }
        }

        isNext = _flag;
    }

    private void NextDialogue()
    {
        if (dialogueCnt < dialogue.Length) // ��ȭ �ε����� ������ ����� �ʵ��� üũ
        {
            //Debug.Log("[[Conversation]] [[NextDialogue]] dialogueIndex>>" + dialogueCnt);
            // ù��° ���� ù��° cg���� ��� ���� cg�� ����Ǹ鼭 ȭ�鿡 ���̰� �ȴ�.
            txt_Dialogue.text = dialogue[dialogueCnt].dialogue;//��ȭ�ؽ�Ʈ���
            //txt_Name.text = dialogue[dialogueCnt].name;//ȭ���̸����
            if (dialogue[dialogueCnt].cg != null)
            {
                if (dialogue[dialogueCnt].isDynamicCg == false)
                {
                    isPlayerSpeak = true;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //Inspector Dialogue �Է� ������Ʈ(����)�� ���� �� ��ȭ�϶��� ȭ���� CgSprite��(����� ������) ����
                    //Debug.Log("[[Conversation]] sprite_CG name>" + sprite_CG.transform.name);
                    sprite_CG.sprite = dialogue[dialogueCnt].cg;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //Debug.Log("[[Conversation]] SpriteCG>" + dialogue[dialogueCnt].cg);
                }
                else if (dialogue[dialogueCnt].isDynamicCg == true)
                {
                    //�÷��̾ �ƴ� ����(������,����,Ÿ�뽺)���� ���ϴ� ���(������ ǥ���� �������� �ٲ�� ��ȹ��������)
                    isPlayerSpeak = false;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //�̿Ͱ��� ��쿡�� ���������� �ƴ� �ܺ� Ŭ����(��ũ��Ʈ)��� �����Ͽ� ���� ���� Swap
                }
                sprite_CG.gameObject.SetActive(true); // ��������Ʈ�� ������ Ȱ��ȭ
            }
            else
            {
                if (dialogue[dialogueCnt].isDynamicCg == false)
                {
                    isPlayerSpeak = true;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //Inspector Dialogue �Է� ������Ʈ(����)�� ���� �� ��ȭ�϶�
                    //ȭ�� CgSprite���� ���ٸ� �ش� ��ȭ���� �̹��� �����ϰڴ�(����)
                    sprite_CG.gameObject.SetActive(false); // ��������Ʈ�� ������ ��Ȱ��ȭ             
                }
                else if (dialogue[dialogueCnt].isDynamicCg == true)
                {
                    //�÷��̾ �ƴ� ����(������,����,Ÿ�뽺)���� ���ϴ� ���(������ ǥ���� �������� �ٲ�� ��ȹ��������)
                    isPlayerSpeak = false;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //�ش� ��ȭ �������� ���� DynamicCg�ν� �ڵ����� �ܺο��� SpriteCg�� �����Ͽ� �̹���Swap�ϴ�
                    //���̽���� �̹����� Ȱ��ȭ �Ǿ��־�߸� �Ѵ�.
                    sprite_CG.gameObject.SetActive(true);
                }
            }

            //��ȭâ Ŭ������(����� �⺻�� ��ǳ���� �׻� �����)->���� dialogueCnt >= ViewThinkCloud_startIndex ���� Update��׶��� ��ǳ��Ȱ��ȭ
            if (thinkCloud != null && isAction_TargetTutorial == true)
            {
                thinkCloud.gameObject.SetActive(false);
            }

            dialogueCnt++; // ���� ���� cg�� ��������
            playercameracontroll.IsMoved = false;//��ȭ���߿� �÷��̾� ī�޶����� X
        }
        else if (dialogueCnt >= dialogue.Length)
        {
            ONOFF(false); // ��簡 �����ٸ� UI�� ��Ȱ��ȭ
            playercameracontroll.IsMoved = true;//��ȭ�� �������� ī�޶����� O
            isCompleted = true;

            isPlayerSpeak = false;
            NowSpeakername = "";

            //��ȭ �� ���� Ư��Ȱ��ȭ�Ͽ� �����ų ������Ʈ�� �ִٸ�
            if (targetactive_ClickEvent != null)
            {
                targetactive_ClickEvent.CanClick = true;
            }
            //���̾Ʒα�->���̾Ʒα� ���̽�
            if (isCompleted_NextDialog)
            {
                if (Target_Dialogue != null)
                {
                    Target_Dialogue.SetActive(true);
                    //Debug.Log("[[Conversation]] ���� ��ȭ ���̾Ʒα׷� �Ѿ activeConversationIndex����>>");
                    lovegameManager.activeConversationIndex++;
                }
            }
            //���̾Ʒα�->Ʃ�丮�� ������� ���̽�
            if (isAction_TargetTutorial)
            {
                if (TargetTutorial != null)
                {
                    //Ÿ�뽺��,������ �̴ϰ��� Ȱ��ȭ
                    //Debug.Log("[[Conversation]] ���� �׼� Ʃ�丮��� �Ѿ activeConversationIndex����>>");
                    TargetTutorial.SetNextTutorial();
                }
                if (OXQuizPanel != null)
                {
                    //���� �̴ϰ���(OX) Ȱ��ȭ
                    OXQuizPanel.SetActive(true);
                }
            }

            //������ ��ȭ ���̾Ʒα�(Conversation)���� ����� ���� ����,���� �ص� ���� �б� �̵�>>
            if (isLastDialog)
            {
                if (isDynamicScene)
                {
                    if (playercameracontroll.LoveScore >= 10)
                    {
                        //Debug.Log("[[Conversation]]���ͱ�� �ḻ ��� �ḻ ������ �̵� ����!!!");//DEBUG
                        //SceneManager.LoadScene(HappyEndingSceneName);
                        MarryMeSuggestUI.SetActive(true);
                        maintain_gameDatamanager.visit_planetList[mother_planetIndex] = true;
                    }

                    if(playercameracontroll.HeartCount_ <= 0)
                    {
                        GameOverPanel.SetActive(true);
                        maintain_gameDatamanager.visit_planetList[mother_planetIndex] = true;
                    }
                }
            }
        }
    }

    public void LoveAccept_Action()
    {
        if (isLastDialog)
        {
            //Debug.Log("��ȥ �³� ���Ǿص� �� �̵�>>");
            //SceneManager.LoadScene(HappyEndingSceneName);
            goodEndingPanel.SetActive(true);
            MarryMeSuggestUI.SetActive(false);
            playercameracontroll.HealthSlider.gameObject.SetActive(false);

        }
    }
    public void LoveRefuse_Action()
    {
        if (isLastDialog)
        {
            //Debug.Log("��ȥ ���� �༺ �� �� �̵�,���� Ƚ�� +1 ����");
            maintain_gameDatamanager.SetRefuseCount(1);
            SceneManager.LoadScene("RoadViewBase_SpaceTour");
        }
    }
}
