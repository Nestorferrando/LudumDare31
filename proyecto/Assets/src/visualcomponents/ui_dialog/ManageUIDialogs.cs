using UnityEngine;
using System.Collections;

public class ManageUIDialogs : MonoBehaviour
{

    [Range(0f, 10f)]
    public float delay = 3f; // seconds

    [Range(0f, 10f)]
    public float delayWave = 5f; // seconds

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
        dialog.init(delayWave, font, fontSize, fadeSpeed, DialogType.waveEnters, role);
    }

    private void setSheriffRejectDialog()
    {
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, DialogType.sheriffReject, Role.Neutral);
    }

    private void setSheriffAcceptsDialog()
    {
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, DialogType.sheriffAccept, Role.Neutral);
    }

    public void setSheriffSurrenderDialog()
    {

        //DOES NOT WORK AND WE DONT HAVE TIME
        /*
        child = GameObject.Find("PanelSheriff");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, DialogType.sheriffSurrender, Role.Neutral);
        */
    }


    public void startDuelDialog()
    {
        dialog.destroy();
        setSheriffRejectDialog();
    }

    public void avoidDuelDialog()
    {
        dialog.destroy();
        setSheriffAcceptsDialog();
    }

    public void setNewSheriffDialog()
    {
        dialog.destroy();
        child = GameObject.Find("PanelWave");
        child.AddComponent<UIDialog>();
        dialog = child.GetComponent<UIDialog>();
        dialog.init(delay, font, fontSize, fadeSpeed, DialogType.waveWinDuel, Role.Neutral);

    }

    public void stop()
    {
        child = GameObject.Find("PanelSheriff");
        dialog = child.GetComponent<UIDialog>();
        if (dialog != null) dialog.stop();

        child = GameObject.Find("PanelWave");
        dialog = child.GetComponent<UIDialog>();
        if (dialog != null) dialog.stop();
    }
}