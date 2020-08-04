/* Snowboarding1 - Logan Shehane
 * This script displays the number of coins a player has from the LoadManager directly.
 */
using UnityEngine;
using UnityEngine.UI;
public class CoinDisplay : MonoBehaviour
{
    public Text textComponentVar;
    public float Coins = 0;
    void Start()
    {
        Coins = LoadManager.instance.data.Coins;
    }
    void Update()
    {
        textComponentVar.text = (Coins).ToString();
    }
}