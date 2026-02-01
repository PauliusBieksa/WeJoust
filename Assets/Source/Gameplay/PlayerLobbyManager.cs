using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLobbyManager : MonoBehaviour
{
   public static List<Player> Players =  new List<Player>();
   public List<PlayerCard> playerCards = new List<PlayerCard>();
   public List<Color> colors = new List<Color>();
   public PlayerInputManager playerInputManager;
   public Image WinScreen;
   
   
   public static Action<Player> RegisterPlayerEvent;
   public static Action<Player> UnregisterPlayerEvent;
   
   private int joinedPLayerCount = 0;
   private bool playersJoining = true;
   
   Player winner;

   private void OnEnable()
   {
      RegisterPlayerEvent+=RegisterPlayer;
      Time.timeScale = 0;
   }

   private void Update()
   {
      LobbyJoining();
      Winner();
   }

   private void Winner()
   {
      if (winner != null || joinedPLayerCount < 2)
      {
         return;
      }
      
      int deadCount = 0;
      foreach (Player player in Players)
      {
         if (player.dead)
         {
            deadCount++;
         }
      }
      if (deadCount == joinedPLayerCount - 1)
      {
         WinScreen.gameObject.SetActive(true);
         foreach (Player player in Players)
         {
            if (player.dead == false)
            {
               winner = player;
               player.gameObject.SetActive(false);
               winner.playerCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920, 1080);
               winner.playerCard.GetComponent<RectTransform>().position = new Vector2(0f, 0f);
            }
         }
      }
   }

   private void LobbyJoining()
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
            player.playerCard.itemIcon.sprite = player.playerCard.defaultItemSprite;
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
   
   //restart
   void OnAnyButton(InputControl control)
   {
      if(winner != null)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
   }

}
