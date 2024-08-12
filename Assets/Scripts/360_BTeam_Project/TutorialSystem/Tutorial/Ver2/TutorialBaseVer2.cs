using UnityEngine;

public abstract class TutorialBaseVer2 : MonoBehaviour
{
    // �ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public abstract void Enter();

    // �ش� Ʃ�丮�� ������ �����ϴ� ���� �� ������ ȣ��
    public abstract void Execute(TutorialControllerVer2 controller);

    // �ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public abstract void Exit();
}


