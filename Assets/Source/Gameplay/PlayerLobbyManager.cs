using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyManager : MonoBehaviour
{
   public static List<Player> Players =  new List<Player>();
   public List<PlayerCard> playerCards = new List<PlayerCard>();
   public List<Color> colors = new List<Color>();
   
   
   public static Action<Player> RegisterPlayerEvent;
   public static Action<Player> UnregisterPlayerEvent;
   
   private int joinedPLayerCount = 0;

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
      if(joinedPLayerCount < 4)
      {
         Players.Add(player);
         player.playerID =  joinedPLayerCount;
         player.chair.color = colors[joinedPLayerCount] * 2;
         ActivatePlayerCard(joinedPLayerCount, player);
         joinedPLayerCount++;
      }
      else
      {
         Destroy(player.gameObject);
      }
   }

   private void ActivatePlayerCard(int index, Player player)
   {
      playerCards[index].gameObject.SetActive(true);
      player.playerCard = playerCards[index];
      player.playerCard.borderImage.color = colors[joinedPLayerCount] * 2;
   }

}
