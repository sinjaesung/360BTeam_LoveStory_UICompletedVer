using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    [SerializeField]
    private string nextSceneName = "";

    private TutorialBase currentTutorial = null;
    public int currentIndex = -1;

    public bool isCompleted = false;

    public Player3d_Planet player;

    [SerializeField] private bool isDynamicScene = false;
    [SerializeField] private string HappyEndingSceneName;
    [SerializeField] private string SadEndingSceneName;

    public Maintain_GameDatamanager maintain_gameDatamanager;
    public GameObject MarryMeSuggestUI;

    private void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();
        maintain_gameDatamanager = FindObjectOfType<Maintain_GameDatamanager>();
        SetNextTutorial();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        Debug.Log("SetNextTutorial >> " + currentIndex);
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

        Debug.Log("[CompletedAllTutorials]Complete All >> ���� Ÿ�� �� �̵�>>>");//1ȸ(�� ������ ���޽� ù �ѹ��� ����)

        if (!nextSceneName.Equals(""))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (isDynamicScene)
        {
            if(player.LoveScore >= 10)
            {
                Debug.Log("[CompletedAllTutorials]���� ��ȥ�������� UI ����" + HappyEndingSceneName);
                //SceneManager.LoadScene(HappyEndingSceneName); ��ȥ���� UI (�³�,����)�� ����, �³��ÿ� ���Ǿص���ȥ�ص��� �̵�,�����ÿ� 
                //�༺ ������ ��������,����Ƚ�� +1
                MarryMeSuggestUI.SetActive(true);
            }
            else if(player.LoveScore < -8)
            {
                Debug.Log("[CompletedAllTutorials]����ص������� �̵�,������ �����ص� ��Name(SadScene)�� �̵�" + SadEndingSceneName);
                SceneManager.LoadScene(SadEndingSceneName);
            }     
        }
    }

    public void LoveAccept_Action()
    {
        Debug.Log("��ȥ �³� ���Ǿص� �� �̵�>>");
        SceneManager.LoadScene(HappyEndingSceneName);
    }
    public void LoveRefuse_Action()
    {
        Debug.Log("��ȥ ���� �༺ �� �� �̵�,���� Ƚ�� +1 ����");
        maintain_gameDatamanager.SetRefuseCount(1);
        SceneManager.LoadScene("RoadViewBase_SpaceTour");
    }
}


