using UnityEngine;
using System;
public class Kills : IComparable<Kills>
{
    public string playerName;
    public int playerKills;
    public Kills(string newplayerName, int newPlayerKills)
    {
        playerName = newplayerName;
        playerKills = newPlayerKills;
    }
    public int CompareTo(Kills other)
    {
        return other.playerKills - playerKills; // Sort in descending order
    }
}
