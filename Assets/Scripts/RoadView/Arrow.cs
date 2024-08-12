using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.SceneView;

/*
 * ���� �̵��ϴ� �뵵�� Arrow
   Ŭ���ÿ� ���� ����� ������ ���̵�ƿ��Ǹ� �̵��Ѵ�.
 */

public class Arrow : MonoBehaviour
{
    [SerializeField] private RectTransform objMessage;
    [SerializeField] private TMP_Text TextMessage;

    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);

    [SerializeField] private new string name = string.Empty;

    private bool isHover = false;
    //public bool[] isClear = new bool[3];

    //[SerializeField] private int loadSceneIndex = 5;
    //Arrow�� Ŭ���� �ش� ��������(�ش� Arrow�� �ٹ���ġ�� ī�޶� �̵���Ű�¿��� ����)
    //Arrow�� �̵���Ű�� ���⺤�� x,z���� ���͸� �����صΰ� Ŭ���� �� �������� �̵��ϰԲ�(ī�޶�)
    [SerializeField] private Vector3 move_direction = new Vector3(0, 0, 0);

    [SerializeField] private Camera playerCamera;

    //[SerializeField] private int moveSceneIndex = 0;
    [SerializeField] private string moveSceneName;
    [SerializeField] public int plantIndex = 0;
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] CameraMoveTest cameraMove;

    [SerializeField] private PlanetUI planetui;

    public Maintain_GameDatamanager maintain_gameDatamanager;

    private void Start()
    {
        planetui = FindObjectOfType<PlanetUI>();
        fadeinout = FindObjectOfType<FadeInOut>();
        playerCamera = FindObjectOfType<Camera>();
        cameraMove = playerCamera.GetComponent<CameraMoveTest>();
        maintain_gameDatamanager = FindObjectOfType<Maintain_GameDatamanager>();
    }
    private void OnMouseEnter()
    {
        isHover = true;
    }
    private void OnMouseExit()
    {
        isHover = false;
    }

    private void OnMouseUp()
    {
       //Debug.Log("Arrow��� Ŭ���� ���!");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�浹 �ε��� ���ü>>" + other.transform.name);
        // objMessage.SetActive(true);
        if (!maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            objMessage.anchoredPosition = new Vector2(0, 0);
            TextMessage.text = $"{name} �༺���� ���� �Ͻǰſ���?";
            planetui.activePlanetIndex = plantIndex;
        }

        if (maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            objMessage.anchoredPosition = new Vector2(0, 0);
            TextMessage.text = $"�༺�� ������ �� �����ϴ�.";
            planetui.activePlanetIndex = plantIndex;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // objMessage.SetActive(false);
        objMessage.anchoredPosition = new Vector2(0, 1200);
    }
    /*public void UI_MoveScene_Close()
    {
        objMessage.SetActive(false);
    }*/
    public void SceneMove()
    {
        //Debug.Log("[[Planet Scene Move]]�༺ ���̵�>>" + moveSceneName);
        //SceneManager.LoadScene(moveSceneName);
        if (!maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            Debug.Log("�༺ �湮�������� ��� �༺ ������ �湮>");
            StartCoroutine(SceneLoad());
        }
        else
        {
            Debug.Log("�༺ �湮�����ִ� ��� �༺ ��������");
        }
    }
    IEnumerator SceneLoad()
    {
        cameraMove.CanControl = false;
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("fadeinoutȿ���� ��� ���� waitTime�Ŀ� �� ��ȯ" + waitTime);
        //Debug.Log($"�ش� {transform.name}�༺ ���� ������ �̵� [[Planet Scene Move]]>>" + moveSceneName);
        SceneManager.LoadScene(moveSceneName);
    }

    /*private void Update()
    {
        if(isHover == true)
        {
            //Arrow������ǥ�� ȭ�� ��ũ����ǥ�� �ٲ��ش�.
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);

            //���� ��ġ�� ��ũ����ǥ�� ������ �� message�� ��ġ�� ����
            objMessage.transform.position = screenPos;
        }
    }*/
}
