using UnityEngine;
using System.Collections;

public class ManageUIDialogs : MonoBehaviour
{

    [Range(0f, 4f)]
    public float delay = 3f; // seconds
    public Font font = new Font();
    public int fontSize = 20;
    [Range(0, 1f)]
    public float fadeSpeed = .5f;

    private GameObject child;
    private UIDialog dialog;

    void Start()
    {
        //setWaveDialog(Role.Miner);
        setNewSheriffDialog();
        //startDuelDialog();

    }

    public void setWaveDialog(Role role)
    {
        child = GameObject.Find("PanelWave");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, false)
            .setRole(role);
    }

    private void setSheriffDialog()
    {
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, true);
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
        dialog.init(delay, font, fontSize, fadeSpeed, false)
            .setSheriffDied(true);
    }

}