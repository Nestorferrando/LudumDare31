using UnityEngine;
using System.Collections;

public class ManageUIDialogs : MonoBehaviour
{

    [Range(0f, 4f)]
    public float delay = 3f; // seconds

    public Font font;// = new Font();
    public int fontSize = 20;
    [Range(0, 1f)]
    public float fadeSpeed = .5f;

    private GameObject child;
    private UIDialog dialog;

    void Start()
    {
        //setWaveDialog(Role.Miner);
        //setNewSheriffDialog();
        //startDuelDialog();
        //setSheriffSurrenderDialog();

    }

    public void setWaveDialog(Role role)
    {
        child = GameObject.Find("PanelWave");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, false,false)
            .setRole(role);
    }

    private void setSheriffDialog()
    {
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, true, false);
    }

    public void setSheriffSurrenderDialog()
    {
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, true, true).setSheriffDied(false);
    }

    public void startDuelDialog()
    {
        dialog.startDuel();
        setSheriffDialog();
    }

    public void setNewSheriffDialog()
    {
        child = GameObject.Find("PanelWave");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, false, false)
            .setSheriffDied(true);
    }

}