/* Snowboarding1 - SentientDragon5
 * This Script's purpose is to display different types of information on a text component in Unity's UI.
 * 
 * Enums are very useful. They can be used as State changes for players (idle mode or walk or attack for instance)
 * In this I am using it as a Multiple Choice box for input.
 */
using UnityEngine;
using UnityEngine.UI;
public enum DisplayItem 
{
    Coins,
    Music,
    SFX
}
[RequireComponent(typeof(Text))]
public class LoadDisplay : MonoBehaviour
{
    private Text textComponentVar;
    public DisplayItem Item;//Declare the Enum in the Script
    private int   Coins = 0;
    private float Music = 0;
    private float SFX   = 0;

    void Start()
    {
        textComponentVar = GetComponent<Text>();//Get the TextComponent
    }
    void Update()
    {
        Coins = LoadManager.instance.data.Coins; // Get the info from the LoadManager, .instance is important because we want the one valid LoadManager, not a duplicate that could have old info.
        Music = LoadManager.instance.data.volumeMusic;
        SFX = LoadManager.instance.data.volumeSFX;
        if (Item == DisplayItem.Coins)
        {
            textComponentVar.text = (Coins).ToString();// display info on a canvas
        }
        if (Item == DisplayItem.Music)
        {
            textComponentVar.text = (Mathf.Round(Music * 100)).ToString(); //Volume is in a decimal, I convert it to a percent
        }
        if (Item == DisplayItem.SFX)
        {
            textComponentVar.text = (Mathf.Round(SFX * 100)).ToString();
        }
    }

}