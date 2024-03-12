using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] bool DISABLE;
    [SerializeField] PlayableDirector playableDirector;
    public enum State { Waiting, Playing, Ended}
    [Header("State")]
    [SerializeField] private State currentState;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] bool ableToClick;
    [SerializeField] GameObject camera;
    [Header("Player Movement")]
    [SerializeField] BessyMovement playerMovement;
    [Header("Dialogues")]
    [SerializeField] int currentDialogue;
    [SerializeField] int dialogues;
    [SerializeField] List<dialogues> dialogueHandlers= new List<dialogues>();
    

    private void Awake()
    {
        currentState = State.Waiting;
        DisabledTrigger();

        currentDialogue = 0;
        dialogues = dialogueHandlers.Count;
    }

  // private void Update()
  // {
  //   //  if(Input.GetKeyDown(KeyCode.Space) && currentState == State.Playing)
  //   //  {
  //   //      InvokeDialogue();
  //   //  }
  // }
  //


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") & currentState == State.Waiting && !DISABLE)
        {
            camera.SetActive(true);
            playableDirector.enabled = true;

            currentState = State.Playing;
            playerMovement.enabled = false;
            Debug.Log("Dialogue Handler");
           // InvokeDialogue();
        }
    }


    public void InvokeDialogue()
    {
        
        dialogueText.text = "["+ dialogueHandlers[currentDialogue].actor +"]: " + dialogueHandlers[currentDialogue].dialogue;

        currentDialogue++;

    }



    public void DisableDialogue()
    {
        dialogueText.text = null;
        if (currentDialogue == dialogues)
        {
            currentState = State.Ended;
            DisabledTrigger();
            playerMovement.enabled = true;
        }
    }

    private void DisabledTrigger()
    {
        playableDirector.enabled = false;
        camera.SetActive(false);
        dialogueText.text = null;

    }

}



[System.Serializable]

public class dialogues
{
    public string actor;
    public string dialogue;
    public UnityEvent nextStep;

    
}
