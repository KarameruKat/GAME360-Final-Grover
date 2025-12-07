using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GideonSpeaks
{
    public class GideonSpeaks : MonoBehaviour
    {
        public GameObject gideonTalks;
        public TMP_Text gideonText;
        public string[] dialogue3;
        private int index;
        public Animator animator;
        private AudioSource audioSource;
        public AudioClip BigGuyTalks;
        [SerializeField] private bool stopAudioSource;

        public GameObject contButton3;
        public float wordSpeed;
        public bool playerIsClose;


        void Start()
        {
            gideonText.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            audioSource = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                if (gideonTalks.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    gideonTalks.SetActive(true);
                    StartCoroutine(Typing());

                }
                //audioSource.PlayOneShot(/*name*/);
            }
            if (gideonText.text == dialogue3[index])
            {
                contButton3.SetActive(true);
            }
        }

        public void zeroText()
        {
            gideonText.text = "";
            index = 0;
            gideonTalks.SetActive(false);
            audioSource.Stop();
        }

        IEnumerator Typing()
        {
            foreach (char letter in dialogue3[index].ToCharArray())
            {
                gideonText.text += letter;
                if (stopAudioSource)
                {
                    audioSource.Stop();
                }
                audioSource.PlayOneShot(BigGuyTalks);
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            contButton3.SetActive(false);
            if (index < dialogue3.Length - 1)
            {
                index++;
                gideonText.text = "";
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
                animator.Play("GideonSpeaks");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;
                zeroText();
                animator.Play("GideonsIdle");
            }
        }
    }
    
    }


