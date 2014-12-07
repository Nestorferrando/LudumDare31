using System;
using UnityEngine;
using System.Collections;

public class Sheriff : MonoBehaviour
{

    private SheriffModel model=new SheriffModel(Role.BusinessMan,Role.Criminal);
  
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
        proposedState = PersonState.cocking;
    }

    public void performDieAnimation()
    {
        proposedState = PersonState.dying;
    }


    public void crouch()
    {
        this.model.setHiddenBehindBarrel(true);
        proposedState = PersonState.crouching; 
    }


    public void standUp()
    {
        this.model.setHiddenBehindBarrel(false);
        proposedState = PersonState.idle; 
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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
