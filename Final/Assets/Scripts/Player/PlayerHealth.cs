using UnityEngine;

public class PlayerHealth : Health
{
    GUIManager guiManager;
    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
        guiManager = FindObjectOfType<GUIManager>();
    }

    public override void HPUpdated(int hp)
    {
        guiManager.SetHp(hp);

        if(hp <= 0)
        {
            player.Killed();
        }
    }
}
