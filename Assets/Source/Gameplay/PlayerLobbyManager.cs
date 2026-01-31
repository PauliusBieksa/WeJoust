using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyManager : MonoBehaviour
{
   public static List<Player> Players =  new List<Player>();
   public List<PlayerCard> playerCards = new List<PlayerCard>();
   public static Action<Player> RegisterPlayerEvent;
   public static Action<Player> UnregisterPlayerEvent;

   private void OnEnable()
   {
      RegisterPlayerEvent+=RegisterPlayer;
   }
   
   private void OnDisable()
   {
      RegisterPlayerEvent-=RegisterPlayer;
   }

   private void RegisterPlayer(Player player)
   {
      Players.Add(player);
      ActivatePlayerCard(Players.Count-1);
   }

   private void ActivatePlayerCard(int index)
   {
      playerCards[index].gameObject.SetActive(true);
   }

}
