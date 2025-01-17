using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialougeText;
    public AudioSource dialougeAudio;
    // public Animator animator;
    // public GameObject nextButton;
    private Queue<string> sentences;
    private Queue<AudioClip> audioClips;

    void Start()
    {
        sentences = new Queue<string>();
        audioClips = new Queue<AudioClip>();
        // nextButton.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket)){
            DisplayNextSentence();
        }
    }

    public void StartDialouge(Dialouge dialouge)
    {
        // animator.SetBool("IsOpen", true);

        nameText.text = dialouge.name;

        sentences.Clear();
        audioClips.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (AudioClip audio in dialouge.audioClips)
        {
            audioClips.Enqueue(audio);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        // Dialouge Audio
        AudioClip audio = audioClips.Dequeue();
        dialougeAudio.PlayOneShot(audio);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }

    public void EndDialouge()
    {
        Debug.Log("End of dialouge");
        // animator.SetBool("IsOpen", false);
        // nextButton.SetActive(true);
    }
}
