using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OllieSpeaking
{
    public class OllieSpeaks : MonoBehaviour
        {
            public GameObject olliesTalking;
            public TMP_Text ollieText;
            public string[] dialogue5;
        private int index;
        //public Animator animator;
        private AudioSource audioSource;
        public AudioClip ollieBabbles;
            [SerializeField] private bool stopAudioSource;

        public GameObject contButton5;
        public float wordSpeed;
        public bool playerIsClose;


        void Start()
        {
            ollieText.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            audioSource = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                if (olliesTalking.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    olliesTalking.SetActive(true);
                    StartCoroutine(Typing());

                }
                //audioSource.PlayOneShot(/*name*/);
            }
            if (ollieText.text == dialogue5[index])
            {
                contButton5.SetActive(true);
            }
        }

        public void zeroText()
        {
            ollieText.text = "";
            index = 0;
            olliesTalking.SetActive(false);
            audioSource.Stop();
        }

        IEnumerator Typing()
        {
            foreach (char letter in dialogue5[index].ToCharArray())
            {
                ollieText.text += letter;
                if (stopAudioSource)
                {
                    audioSource.Stop();
                }
                audioSource.PlayOneShot(ollieBabbles);
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            contButton5.SetActive(false);
            if (index < dialogue5.Length - 1)
            {
                index++;
                ollieText.text = "";
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
