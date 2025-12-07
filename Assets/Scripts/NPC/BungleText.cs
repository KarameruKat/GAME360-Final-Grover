using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BungleSpeaks
{
    public class BungleText : MonoBehaviour
    {
        public GameObject bungleFlavortext;
        public TMP_Text bungleDialogue;
        public string[] dialogue4;
        private int index;
        //public Animator animator;
        private AudioSource audioSource;
        public AudioClip textnoise;
        [SerializeField] private bool stopAudioSource;

        public GameObject contButton4;
        public float wordSpeed;
        public bool playerIsClose;


        void Start()
        {
            bungleDialogue.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            audioSource = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
            {
                if (bungleFlavortext.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    bungleFlavortext.SetActive(true);
                    StartCoroutine(Typing());

                }
                //audioSource.PlayOneShot(/*name*/);
            }
            if (bungleDialogue.text == dialogue4[index])
            {
                contButton4.SetActive(true);
            }
        }

        public void zeroText()
        {
            bungleDialogue.text = "";
            index = 0;
            bungleFlavortext.SetActive(false);
            audioSource.Stop();
        }

        IEnumerator Typing()
        {
            foreach (char letter in dialogue4[index].ToCharArray())
            {
                bungleDialogue.text += letter;
                if (stopAudioSource)
                {
                    audioSource.Stop();
                }
                audioSource.PlayOneShot(textnoise);
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {
            contButton4.SetActive(false);
            if (index < dialogue4.Length - 1)
            {
                index++;
                bungleDialogue.text = "";
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


