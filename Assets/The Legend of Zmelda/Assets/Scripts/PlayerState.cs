using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum CharacterState
{
    walk,
    roll,
    idle,
    Attack
}

public class PlayerState : MonoBehaviour
{
    //public CharacterState State;
    //public Sprite WalkSprite;
    //public Sprite AttackSprite;
    SpriteRenderer SpriteRenderer;

    protected Rigidbody2D body;

    protected virtual void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        //SetState(State);
    }

    public void SetState(CharacterState newState)
    {
        //State = newState;

        /*if (State == CharacterState.walk)
        {
            SpriteRenderer.sprite = WalkSprite;
        }
        else if (State == CharacterState.Attack)
        {
            SpriteRenderer.sprite = AttackSprite;
        }*/
    }
}
