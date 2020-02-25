using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GLOBAL 
{
   public static Color[] _PlayerColors = {Color.red, Color.blue,Color.green, Color.yellow};
   public static int _MaxPlayerCount = 4;
   public static List<PlayerController> _PlayerList;
   static GLOBAL()
   {
      

   }
   // Adds player to global player list and returns its player index
  public static int AddPlayerController(PlayerController pc)
   {
       if(_PlayerList == null)
       {
           _PlayerList = new List<PlayerController>();
       }
       //player already added to array and assigned an index
       if(_PlayerList.Contains(pc))
       {
           return _PlayerList.IndexOf(pc);
       }
       // player max capacity acheived
       if(_PlayerList.Count>=_MaxPlayerCount-1)
       {
           return -1;
       }

       _PlayerList.Add(pc);
       return  _PlayerList.Count-1;
   }
}
