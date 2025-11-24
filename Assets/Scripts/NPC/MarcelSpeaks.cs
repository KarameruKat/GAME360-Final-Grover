using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MarcelSpeaks
{
    public class MarcelSpeaksText : MonoBehaviour
    {
        public GameObject marceltalktext;
        public TMP_Text dialogueMarcel;
        public string[] dialogue2;
        private int index;
        //public Animator animator;
        private AudioSource audioSource;
        public AudioClip littleSpeak;
        [SerializeField] private bool stopAudioSource;

        public GameObject contButton2;
        public float wordSpeed;
        public bool playerIsClose;


        void Start()
        {
            dialogueMarcel.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            audioSource = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                if (marceltalktext.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    marceltalktext.SetActive(true);
                    StartCoroutine(Typing());
                    
                }
                //audioSource.PlayOneShot(littleSpeak);
            }
            if (dialogueMarcel.text == dialogue2[index])
            {
                contButton2.SetActive(true);
            }
        }

        public void zeroText()
        {
            dialogueMarcel.text = "";
            index = 0;
            marceltalktext.SetActive(false);
            audioSource.Stop();
        }

        IEnumerator Typing()
        {
            foreach (char letter in dialogue2[index].ToCharArray())
            {
                dialogueMarcel.text += letter;
                if (stopAudioSource) {
                    audioSource.Stop();
                }
                audioSource.PlayOneShot(littleSpeak);
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            contButton2.SetActive(false);
            if (index < dialogue2.Length - 1)
            {
                index++;
                dialogueMarcel.text = "";
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
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;
                zeroText();
            }
        }
    }
}