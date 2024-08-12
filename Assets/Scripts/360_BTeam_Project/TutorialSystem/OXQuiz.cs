using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quiz
{
    [TextArea]
    public string question = " ";
    [Header("������ O��üũ")]
    public bool correctAnswer; // ������ O���� X���� (O�̸� true, X�̸� false)
}

public class OXQuiz : MonoBehaviour
{
    [SerializeField] Button buttonO;
    [SerializeField] Button buttonX;
    [SerializeField] TMP_Text questionText;
    [SerializeField] Quiz[] quiz;
    [SerializeField] public Player3d_Planet player;
    [SerializeField] GameObject GameOverCon;
    [SerializeField] GameObject NextCon;
    [SerializeField] Image cgImg;  // ���� ����� ���� �̹����� ǥ���ϱ� ���� Image ������Ʈ
    [SerializeField] Sprite Correct_Cg;
    [SerializeField] Sprite Wrong_Cg;
    private int quizCountIndex = 0;

    private void Start()
    {
        quizCountIndex = 0;
        DisplayCurrentQuiz();

        // ��ư Ŭ�� �̺�Ʈ ���
        buttonO.onClick.AddListener(() => OnAnswerSelected(true));
        buttonX.onClick.AddListener(() => OnAnswerSelected(false));
    }

    private void Update()
    {
        if (player.HeartCount_ <= 0)
        {
            GameOver();
        }
    }

    // ���� ��� ǥ��
    private void DisplayCurrentQuiz()
    {
        if (quizCountIndex < quiz.Length)
        {
            questionText.text = quiz[quizCountIndex].question;
        }
        else
        {
            // ��� ������ �� ó��
            this.gameObject.SetActive(false);
            NextCon.SetActive(true);
        }
    }

    // ����ڰ� ���� �������� �� ȣ��Ǵ� �޼���
    private void OnAnswerSelected(bool userAnswer)
    {
        if (quiz[quizCountIndex].correctAnswer == userAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void NextQuiz()
    {
        quizCountIndex++;
        if (quizCountIndex < quiz.Length)
        {
            DisplayCurrentQuiz();  // ���� ��� ǥ��
        }
        else
        {
            // ��� ��� ������ �� ó��
            this.gameObject.SetActive(false);
            NextCon.SetActive(true);
        }
    }

    public void CorrectAnswer()
    {
        cgImg.sprite = Correct_Cg;
        NextQuiz();
    }

    public void WrongAnswer()
    {
        cgImg.sprite = Wrong_Cg;
        player.HeartCount_ -= 1;
        NextQuiz();
    }

    public void GameOver()
    {
        this.gameObject.SetActive(false);
        GameOverCon.SetActive(true);
    }
}