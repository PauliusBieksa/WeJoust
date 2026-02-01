using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLobbyManager : MonoBehaviour
{
   public static List<Player> Players =  new List<Player>();
   public List<PlayerCard> playerCards = new List<PlayerCard>();
   public List<Color> colors = new List<Color>();
   public PlayerInputManager playerInputManager;
   
   
   public static Action<Player> RegisterPlayerEvent;
   public static Action<Player> UnregisterPlayerEvent;
   
   private int joinedPLayerCount = 0;
   private bool playersJoining = true;

   private void OnEnable()
   {
      RegisterPlayerEvent+=RegisterPlayer;
      Time.timeScale = 0;
   }

   private void Update()
   {
      if(playersJoining)
      {
         
         bool playersWaiting = false;
         foreach (Player player in Players)
         {
            if (player.readyState == Player.ReadyState.WAITING)
            {
               playersWaiting = true;
               player.playerCard.itemIcon.sprite = player.playerCard.waitingSprite;
            }
            else if (player.readyState == Player.ReadyState.READY)
            {
               player.playerCard.itemIcon.sprite = player.playerCard.readySprite;
            }
         }

         if (!playersWaiting && joinedPLayerCount > 1)
         {
            playersJoining = false;
         }
      }
      else
      {
         foreach (Player player in Players)
         {
            player.readyState = Player.ReadyState.PLAYING;
         }
         Time.timeScale = 1;
         playerInputManager.DisableJoining();
      }
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
