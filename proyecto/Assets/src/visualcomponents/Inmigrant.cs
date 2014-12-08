using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Inmigrant : MonoBehaviour
{


    protected static readonly float moving_speed = 2f;

    protected InmigrantModel model = new InmigrantModel(Role.Criminal);

    protected PersonState currentState;
    protected PersonState proposedState;
    public Boolean facingLeft;



    public Inmigrant()
    {
        currentState = PersonState.idle;
        proposedState = PersonState.idle;
    }

    public InmigrantModel Model
    {
        get { return model; }
        set { model = value;
            currentState = PersonState.idle_cock;
        }
    }


    public void moveInstantlyToPosition(Vector3 position)
    {
        rigidbody2D.transform.position = position;
        proposedState = PersonState.idle;
        this.model.setHiddenBehindBarrel(false);
    }


    public void startMoving()
    {
        proposedState = PersonState.walking;
    }

    public void stopMoving()
    {
        proposedState = PersonState.idle;
    }


    public void performShootAnimation()
    {
        proposedState = PersonState.shooting;
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
        this.model.setHiddenBehindBarrel(false);
        proposedState = PersonState.idle;
    }

    public PersonState CurrentState
    {
        get { return currentState; }
    }


    private void FixedUpdate()
    {
        //set position
        if (currentState == PersonState.walking)
        {
            Vector3 position = rigidbody2D.transform.position;
            position.x += -moving_speed*Time.deltaTime;
            rigidbody2D.transform.position = position;
        }


        //set animation

        float initialScaleX = transform.localScale.x;
        if (facingLeft)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Math.Abs(initialScaleX)*-1;
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
            GetComponent<Animator>().Play(model.Role.ToString() + "_" + proposedState.ToString());
        }

    }





}

