using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Person : MonoBehaviour
{


    protected static readonly float moving_speed = 0.01f;

    protected InmigrantModel model = new InmigrantModel(Role.Criminal);

    protected InmigrantState currentState;
    protected InmigrantState proposedState;
    public Boolean facingLeft;


    public Person()
    {
        currentState = InmigrantState.walking;
        proposedState = InmigrantState.idle;
    }

    public InmigrantModel Model
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


    public void startMoving()
    {
        proposedState = InmigrantState.walking;
    }

    public void stopMoving()
    {
        proposedState = InmigrantState.idle;
    }


    public void performShootAnimation()
    {
        proposedState = InmigrantState.shooting;
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

    public InmigrantState CurrentState
    {
        get { return currentState; }
    }


    private void FixedUpdate()
    {
        //set position
        if (currentState == InmigrantState.walking)
        {
            rigidbody2D.velocity = new Vector2(-moving_speed, 0);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 0);
        }

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





}

