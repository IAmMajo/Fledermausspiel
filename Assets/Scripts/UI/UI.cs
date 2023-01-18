using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI health;
    public TMPro.TextMeshProUGUI munition;
    public Bat player;
    public GunScriptPlayer gunScriptPlayer;
    void Update()
    {
        health.text = player.health.ToString() + "/10"; 
        munition.text = gunScriptPlayer.bulletsLeft.ToString() + "/"+ gunScriptPlayer.magazineSize.ToString();
    }
}
