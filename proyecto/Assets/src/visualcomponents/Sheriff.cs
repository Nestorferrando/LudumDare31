using System;
using UnityEngine;
using System.Collections;

public class Sheriff : MonoBehaviour
{

    private SheriffModel model = new SheriffModel(Role.BusinessMan, Role.Criminal);

    private InmigrantState currentState;
    private InmigrantState proposedState;
    public Boolean facingLeft;



    public Sheriff()
    {
        currentState = InmigrantState.walking;
        proposedState = InmigrantState.idle;
    }

    public SheriffModel Model
    {
        get { return model; }
        set { model = value; }
    }


    public void moveInstantlyToPosition(Vector2 position)
    {
        rigidbody2D.transform.position = position;
        proposedState = InmigrantState.idle;
        this.model.setHiddenBehindBarrel(false);
    }

    public InmigrantState CurrentState
    {
        get { return currentState; }
    }


    public void performShootAnimation()
    {
        proposedState = InmigrantState.shooting;
    }

    public void performCockAnimation()
    {
        proposedState = InmigrantState.cocking;
    }

    public void performDieAnimation()
    {
        proposedState = InmigrantState.dying;
    }


    public void crouch()
    {
        this.model.setHiddenBehindBarrel(true);
        proposedState = InmigrantState.crouching;
    }


    public void standUp()
    {
        this.model.setHiddenBehindBarrel(false);
        proposedState = InmigrantState.idle;
    }


    private void FixedUpdate()
    {

        //set animation

        if (facingLeft)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }


        if (proposedState != currentState)
        {
            currentState = proposedState;
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
