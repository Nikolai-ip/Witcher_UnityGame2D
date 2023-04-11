using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;
    [SerializeField] private TextMeshProUGUI _dialogueWindowText;
    [SerializeField] private TextMeshProUGUI _dialogueWindowSpeakerName;
    [SerializeField] private Image _currentFaceImage;
    [SerializeField] private Animator _dialogueWindowAnimator;
    [SerializeField] private float _typeLettersDelay;
    private static DialogueManager _instance;
    [SerializeField] private Sprite _mainCharacterFace;
    [SerializeField] private string _mainCharacterName;
    private Sprite _currentNpcFace;
    private string _currentNpcName;
    private int _sentenceIndex;
    private void Start()
    {
        if (_instance == null)
        {
            _sentences = new Queue<string>();
            _instance = this;
        }
        else if (_instance == this)
            Destroy(gameObject);
    }

    public static DialogueManager Instance => _instance;

    public void StartDialog(Dialogue dialog, Sprite faceSprite)
    {
        _dialogueWindowAnimator.SetBool("IsOpen", true);
        _currentNpcFace = faceSprite;
        _currentNpcName = dialog.SpeakerName;
        _sentences.Clear();
        SetSpeakerToNPC();
        _sentenceIndex = 0;
        foreach (string text in dialog)
        {
            _sentences.Enqueue(text);
        }
        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {

        if (_sentences.Count == 0)
        {
            EndDialog();
            return false;
        }
        string sentence = _sentences.Dequeue();
        _sentenceIndex++;
        if (sentence == "")
        {
            DisplayNextSentence();
            return true;
        }
            

        if (_sentenceIndex % 2 == 0)
            SetSpeakerToMainCharacter();
        else
            SetSpeakerToNPC();
        StopAllCoroutines();
        
        StartCoroutine(TypeLetters(sentence));
        return true;
    }
    private void SetSpeakerToMainCharacter()
    {
        _currentFaceImage.sprite = _mainCharacterFace;
        _dialogueWindowSpeakerName.text = _mainCharacterName;
        _currentFaceImage.SetNativeSize();
    }
    private void SetSpeakerToNPC()
    {
        _currentFaceImage.sprite = _currentNpcFace;
        _dialogueWindowSpeakerName.text = _currentNpcName;
        _currentFaceImage.SetNativeSize();
    }
    private IEnumerator TypeLetters(string sentence)
    {
        _dialogueWindowText.text = "";
        var delay = new WaitForSeconds(_typeLettersDelay);
        foreach (var symbol in sentence)
        {
            _dialogueWindowText.text += symbol;
            yield return delay;
        }
    }

    public void EndDialog()
    {
        _dialogueWindowAnimator.SetBool("IsOpen", false);
    }
}