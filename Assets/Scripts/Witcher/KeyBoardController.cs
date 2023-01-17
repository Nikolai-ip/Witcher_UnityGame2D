using UnityEngine;

public class KeyBoardController : InputController
{


    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<AttackController>();
    }

    private void Update()
    {
        if (playerAttack!= null)
        {
            CheckAttackButtonsPressed();
        }
        if (playerMove!= null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerMove.Escape();
            }
        }

    }

    private void CheckAttackButtonsPressed()
    {
        if (Input.GetMouseButtonDown((int)Mouse.LeftButton))
        {
            playerAttack.Attack(leftAttack);
        }
        if (Input.GetMouseButtonDown((int)Mouse.RightButton))
        {
            playerAttack.Attack(rightAttack);
        }
        if (Input.GetMouseButtonDown((int)Mouse.CenterButton))
        {
            playerAttack.Attack(pirouetteAttack);
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(moveX, 0))
        {
            playerMove.Move(moveX);
        }
    }
}