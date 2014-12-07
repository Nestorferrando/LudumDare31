using System;
using UnityEngine;
using System.Collections;

public class Sheriff : MonoBehaviour
{

    private SheriffModel model = new SheriffModel(Role.BusinessMan, Role.Criminal);

    private PersonState currentState;
    private PersonState proposedState;
    public Boolean facingLeft;



    public Sheriff()
    {
        currentState = PersonState.walking;
        proposedState = PersonState.idle;
    }

    public SheriffModel Model
    {
        get { return model; }
        set { model = value; }
    }


    public void moveInstantlyToPosition(Vector2 position)
    {
        rigidbody2D.transform.position = position;
        proposedState = PersonState.idle;
        this.model.setHiddenBehindBarrel(false);
    }

    public PersonState CurrentState
    {
        get { return currentState; }
    }


    public void performShootAnimation()
    {
        proposedState = PersonState.shooting;
    }

    public void performCockAnimation()
    {
        proposedState = PersonState.idle_cock;
    }

    public void performDieAnimation()
    {
        proposedState = PersonState.dead;
    }


    public void crouch()
    {
        this.model.setHiddenBehindBarrel(true);
        proposedState = PersonState.crouch;
    }


    public void standUp()
    {
        if (!this.model.HiddenBehindBarrel) return;
        this.model.setHiddenBehindBarrel(false);
        proposedState = PersonState.idle;
    }


    private void FixedUpdate()
    {

        //set animation
        float initialScaleX = transform.localScale.x;
        if (facingLeft)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Math.Abs(initialScaleX) * -1;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Math.Abs(initialScaleX);
            transform.localScale = theScale;
        }

        if (proposedState != currentState)
        {
            currentState = proposedState;
         //   Debug.Log(model.Role.ToString() + "_" + proposedState.ToString());
            GetComponent<Animator>().Play(model.Role.ToString() + "_" + proposedState.ToString());
        }

    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
