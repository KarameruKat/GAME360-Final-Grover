using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript : MonoBehaviour
{
    public GameObject npcTalk;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip talking;


    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (npcTalk.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                npcTalk.SetActive(true);
                StartCoroutine(Typing());
                
            }
            
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
        
    }
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        npcTalk.SetActive(false);
        audioSource.Stop();
        
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            audioSource.PlayOneShot(talking);
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            animator.Play("VelvetTalk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            animator.Play("VelvetIdle");
        }
    }
}
