using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControllerVer2 : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBaseVer2> tutorials;

    public TutorialBaseVer2 currentTutorial = null;
    public int currentIndex = -1;

    public bool isCompleted = false;

    public Player3d_Planet player;

    //SerializeField] private bool isDynamicScene = false;
    //Ʃ�丮��ü ������ ����Ʈ�� ��� �������ÿ� �� �̵����� ���ҿ���
    //[SerializeField] private bool isSceneMoveByCompleted = false;
    //[SerializeField] private string HappyEndingSceneName;
    //[SerializeField] private string SadEndingSceneName;
    [SerializeField] GameObject Target_Dialogue;

    public LoveGameManager lovegameManager;

    private void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();

        lovegameManager = FindObjectOfType<LoveGameManager>();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    //�� ��ü�� �ܺο��� �̰��� ���������� ȣ���ϴ°��(�ٸ� Ŭ�������� ȣ���ؾ��ϴ°��)
    public void SetNextTutorial()
    {
        Debug.Log("Conversation<->[[TutorialControllerVer2]] SetNextTutorial >> " + currentIndex);
        // ���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        // ������ Ʃ�丮���� �����ߴٸ� CompletedAllTutorials() �޼ҵ� ȣ��
        if (currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        // ���� Ʃ�丮�� ������ currentTutorial�� ���
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        // ���� �ٲ� Ʃ�丮���� Enter() �޼ҵ� ȣ��
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;
        isCompleted = true;
        // �ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        // ����� �� ��ȯ

        Debug.Log("Conversation<->[[TutorialControllerVer2]][CompletedAllTutorials]Complete All >> ���� Ÿ�� �� �̵�>>>");//1ȸ(�� ������ ���޽� ù �ѹ��� ����)   

        /* if (isSceneMoveByCompleted)
         {
             if (isDynamicScene)
             {
                 if (player.LoveScore >= 10)
                 {
                     Debug.Log("[CompletedAllTutorials]���Ǿص������� �̵�,������ �����ص� ��Name(happyScene)�� �̵�" + HappyEndingSceneName);
                     SceneManager.LoadScene(HappyEndingSceneName);
                 }
                 else if (player.LoveScore < -8)
                 {
                     Debug.Log("[CompletedAllTutorials]����ص������� �̵�,������ �����ص� ��Name(SadScene)�� �̵�" + SadEndingSceneName);
                     SceneManager.LoadScene(SadEndingSceneName);
                 }
             }
         }*/

        if (Target_Dialogue)
        {
            //Ver2����,Ver1�� �ٸ��� TutorialController�ȿ��� ���̾Ʒαױ��� ��ü �����ϴ����°� �ƴ�
            //TutorialController����, ���̾Ʒα�(Conversation��ȭ) ���� ���������� �����Ͽ� �̿Ͱ��� ��ġ
            Debug.Log("Conversation<->[[TutorialControllerVer2]]Ver2Scene���� ���� Ʃ�丮��Controller �� �Ѱ� (�̴ϰ���)�� �������ø���, " +
                "����� ���̾Ʒα�(Conversation) ����"+Target_Dialogue.transform.name);
            Target_Dialogue.SetActive(true);
            Debug.Log("�׼� Ʃ�丮����Wrapper ��� ������ Ÿ�� ���� ���̾Ʒα� �Ѿ activeConversationIndex����>>");
            lovegameManager.activeConversationIndex++;
            //Ʃ�丮��->���̾Ʒα� ���̽�
        }
    }
}


