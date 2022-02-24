using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public void SinglePlayer()
    {
        GameBuilder.instance.StartSinglePlayer();
    }

    public void TwoPlayers()
    {
        GameBuilder.instance.StartTwoPlayers();
    }

}
