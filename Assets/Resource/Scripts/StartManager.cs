using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Image backgroundPannel;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource clickSfx;
    private Color originalColor;
    void Start()
    {
        originalColor = backgroundPannel.color;
        StartCoroutine(CoFadeOut());
        bgm.Play();
    }

    public void StartButton()
    {
        StartCoroutine(CoFadeIn());
        clickSfx.Play();        
    }
    IEnumerator CoFadeOut()
    {
        float fadeOutTime = 1f; // ���̵� �ƿ� �ð� (1��)
        float elapsedTime = 0f;
        
        

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);
            
            backgroundPannel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            
            yield return null;
        }

        // ���̵� �ƿ��� ���� ��, ������ �����ϰ� ����
        backgroundPannel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        backgroundPannel.gameObject.SetActive(false);
        
    }

    IEnumerator CoFadeIn()
    {
        backgroundPannel.gameObject.SetActive(true);
        float fadeInTime = 2f; // ���̵� �ƿ� �ð� (1��)
        float elapsedTime = 0f;
        float initialVolume = bgm.volume;

        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);
            float volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeInTime);
            backgroundPannel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            bgm.volume = volume;
            yield return null;
        }
        bgm.Stop();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SpaceShip_MainScene");
    }

}
