using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WhatsInHere
{
    public class WhatsInHerecript : MonoBehaviour
    {
        public GameObject caveFlavortext;
        public TMP_Text dialogueTextCave;
        public string[] dialogue1;
        private int index;
        //public Animator animator;
        private AudioSource audioSource;
        public AudioClip sound;


        public GameObject contButton1;
        public float wordSpeed;
        public bool playerIsClose;

        // Update is called once per frame

        private void Start()
        {
            dialogueTextCave.text = "";
        }
        void Update()
        {
            audioSource = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                if (caveFlavortext.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    caveFlavortext.SetActive(true);
                    StartCoroutine(Typing());

                }
                audioSource.PlayOneShot(sound);
            }

            if (dialogueTextCave.text == dialogue1[index])
            {
                contButton1.SetActive(true);
            }

        }
        public void zeroText()
        {
            dialogueTextCave.text = "";
            index = 0;
            caveFlavortext.SetActive(false);
            audioSource.Stop();

        }

        IEnumerator Typing()
        {
            foreach (char letter in dialogue1[index].ToCharArray())
            {
                dialogueTextCave.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            contButton1.SetActive(false);

            if (index < dialogue1.Length - 1)
            {
                index++;
                dialogueTextCave.text = "";
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
                //animator.Play("VelvetTalk");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;
                zeroText();
                //animator.Play("VelvetIdle");
            }
        }
    }
}