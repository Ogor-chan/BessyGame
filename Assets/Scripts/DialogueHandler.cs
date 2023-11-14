using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum Speaker
{
    Bessy,
    Bear
}
public class DialogueHandler : MonoBehaviour
{
    [SerializeField] GameObject TextBox;
    private GameObject SpeakerImage;
    private GameObject TextObject;

    public TextAsset WhichText;
    public Speaker WhichSpeaker;

    public TextAsset TempText;
    public Speaker TempSpeaker;

    private void Start()
    {
        StartDialogue(TempText, TempSpeaker);
    }
    public void StartDialogue(TextAsset textasset, Speaker speaker)
    {
        WhichSpeaker = speaker;
        WhichText = textasset;

        string[] text = WhichText.text.Split('\n');

    }
}

