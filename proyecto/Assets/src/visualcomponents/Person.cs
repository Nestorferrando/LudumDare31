using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Person : MonoBehaviour
{


    private static readonly float moving_speed = 0.01f;

    private PersonModel model;

    private PersonState currentState;
    private PersonState proposedState;
    public Boolean facingLeft;



    public Person()
    {
        currentState = PersonState.walking;
        proposedState = PersonState.idle;
    }

    public PersonModel Model
    {
        get { return model; }
        set { model = value; }
    }


    public void moveInstantlyToPosition(Vector2 position)
    {  
        rigidbody2D.transform.position = position;
        proposedState = PersonState.idle;
        this.model.HiddenBehindBarrel = false;
    }


    public void startMoving()
    {
        proposedState = PersonState.walking;
    }

    public void stopMoving()
    {
        proposedState = PersonState.idle;
    }


    public void performShootAnimation(Vector2 position)
    {
        proposedState = PersonState.shooting;
    }

    public void performDieAnimation(Vector2 position)
    {
        proposedState = PersonState.dying;
    }


    public void crouch()
    {
        this.model.HiddenBehindBarrel = true;
        proposedState = PersonState.crouching; 
    }


    public void standUp()
    {
        this.model.HiddenBehindBarrel = false;
        proposedState = PersonState.idle; 
    }


    private void FixedUpdate()
    {
        //set position
        if (currentState == PersonState.walking)
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


