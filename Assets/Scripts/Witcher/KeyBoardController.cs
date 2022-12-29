using UnityEngine;

public class KeyBoardController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private AttackController _playerAttack;
    enum Mouse
    {
        LeftButton,
        RightButton,
        CenterButton
    }
    private LeftAttack _leftAttack = new LeftAttack();
    private RightAttack _rightAttack = new RightAttack();
    private Pirouette _pirouetteAttack = new Pirouette();

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack= GetComponent<AttackController>();
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown((int)Mouse.LeftButton))
        {
            _playerAttack.Attack(_leftAttack);
        }
        if (Input.GetMouseButtonDown((int)Mouse.RightButton))
        {
            _playerAttack.Attack(_rightAttack);
        }
        if (Input.GetMouseButtonDown((int)Mouse.CenterButton))
        {
            _playerAttack.Attack(_pirouetteAttack);
        }
    }
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(moveX, 0))
        {
            _playerMove.Move(moveX);
        }    
    }
}
