using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] GameObject hitOverlay;
    MOVE playerMove;
    AudioSource playerAudioSource;
    AudioClip playerAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerAudioClip = playerAudioSource.clip;
        playerMove = GetComponent<MOVE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void DamagePlayer(float damage)
    {
        playerAudioSource.PlayOneShot(playerAudioClip);
        playerMove.walkSpeed /= 2;
        playerMove.runSpeed /= 2;
        health -= damage;

        hitOverlay.SetActive(true);

        StartCoroutine(RestorePlayerSpeed());


    }

    public IEnumerator RestorePlayerSpeed()
    {
        yield return  new WaitForSeconds(0.5f);

        hitOverlay.SetActive(false);
        playerMove.walkSpeed *= 2;
        playerMove.runSpeed *= 2;
    }
}
