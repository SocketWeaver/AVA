using UnityEngine;
using System.Collections;

public class BotHealth : Health
{
    Bot bot;

    void Awake()
    {
        bot = GetComponent<Bot>();
    }

    public override void HPUpdated(int hp)
    {
        if (hp <= 0)
        {
            bot.Killed();
        }
    }
}
