using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI text;
    Color hoverColor = Color.gray;
    Color normalColor = Color.white;
    AudioSource audioSource;
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        // ��� �������� ������ � ����������� ����
        Application.Quit();

        // ��� ��������� Unity (�������� ������ � ������ Play)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
        audioSource.PlayOneShot(clip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}
