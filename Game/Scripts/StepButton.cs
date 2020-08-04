/* Snowboarding1 - Logan Shehane
 * this button is used to add a step to a float, that it gets from the loadManager, whether that is the settings page, or the volume.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ButtonUse
{
    Coin,
    Music,
    SFX,
    SettingsMenu
}
public class StepButton : MonoBehaviour
{
    public ButtonUse Use;
    [Header("Coin")]
    public bool increment = false;
    public int SetValue = 0;
    [Header("Audio")]
    public float Step = 0.05f;
    private float[] volume = { LoadManager.instance.data.volumeMusic, LoadManager.instance.data.volumeSFX };
    [Header("SettingsMenu")]
    public List<GameObject> Pages = new List<GameObject>();
    private int pageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        pageNum = 0;
        for (int i = 0; i < Pages.Count; i++)
        {
            Pages[i].SetActive(false);
        }
        Pages[pageNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        #region enforceAudioLimits
        if (LoadManager.instance.data.volumeSFX > 1f)
        {
            LoadManager.instance.data.volumeSFX = 1f;
        }
        if (LoadManager.instance.data.volumeSFX < 0f)
        {
            LoadManager.instance.data.volumeSFX = 0f;
        }

        if (LoadManager.instance.data.volumeMusic > 1f)
        {
            LoadManager.instance.data.volumeMusic = 1f;
        }
        if (LoadManager.instance.data.volumeMusic < 0f)
        {
            LoadManager.instance.data.volumeMusic = 0f;
        }
        #endregion
    }
    void OnTriggerEnter()
    {
        if (Use == ButtonUse.Music)
        {
            if (increment)
            {
                LoadManager.instance.data.Coins += SetValue;
            }
            else
            {
                LoadManager.instance.data.Coins = SetValue;
            }
        }
        if (Use == ButtonUse.Music)
        {
            LoadManager.instance.data.volumeMusic += Step;
        }
        if (Use == ButtonUse.SFX)
        {
            LoadManager.instance.data.volumeSFX += Step;
        }
        if (Use == ButtonUse.SettingsMenu)
        {
            pageNum += 1;
            if (pageNum > Pages.Count) { pageNum = 0; }
            for (int i = 0; i < Pages.Count; i++)
            {
                Pages[i].SetActive(false);
            }
            Pages[pageNum].SetActive(true);
        }
    }
}
