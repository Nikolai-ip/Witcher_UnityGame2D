using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private Sprite _faceSprite;
    [SerializeField] private float _distanceToTrigger;
    private Collider2D hit;
    private PlayerInputController _player;
    [SerializeField] private bool _dialogueIsStarted = false;
    [SerializeField] private GameObject _dilogueSticker;

    private Collider2D Hit
    {
        set
        {
            if (value != hit)
                if (value != null)
                {
                    if (_player == null)
                    {
                        _player = value.GetComponent<PlayerInputController>();
                    }
                    _player.OnIteractButtonPerformed += DialogueOptionEnabled;
                    _dilogueSticker.SetActive(true);
                }
                else
                {
                    _player.OnIteractButtonPerformed -= DialogueOptionEnabled;
                    DialogueOptionDisabled();
                    _dilogueSticker.SetActive(false);
                }
            hit = value;
        }
    }

    private void Update()
    {
        Hit = Physics2D.OverlapCircle(transform.position, _distanceToTrigger, LayerMask.GetMask("Player"));
    }

    private void DialogueOptionEnabled()
    {
        _dilogueSticker.SetActive(false);
        if (_dialogueIsStarted)
        {
            bool dialogueIsContinue = DialogueManager.Instance.DisplayNextSentence();
            _dialogueIsStarted = dialogueIsContinue ? true : false;
            return;
        }
        _dialogueIsStarted = true;
        DialogueManager.Instance.StartDialog(_dialogue, _faceSprite);
    }

    private void DialogueOptionDisabled()
    {
        _dialogueIsStarted = false;
        DialogueManager.Instance.EndDialog();
    }

    private void Start()
    {
        _dilogueSticker.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 140, 0);
        Gizmos.DrawWireSphere(transform.position, _distanceToTrigger);
    }
}