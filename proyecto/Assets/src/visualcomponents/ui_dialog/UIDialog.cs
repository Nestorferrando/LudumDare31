using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{

    private Text dialog;
    private float delay;
    private float startTime;

	void Start ()
	{

	    dialog = gameObject.AddComponent<Text>();
	    delay = 1000; //ms

	    dialog.text = Dialog.getDialog(Role.BusinessMan);
	    startTime = Time.fixedTime;

        Debug.Log("DESTROY");
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.fixedTime > startTime + delay)
	    {

	        Destroy(this);
	    }

        Destroy(gameObject.GetComponent<UIDialog>());
	}
}
