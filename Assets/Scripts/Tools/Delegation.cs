using System;

public class Delegation
{
   public static Action<string> BugsStatChange;
   public static void OnBugsStatChange(string statName)
   {
        if(BugsStatChange == null) return;
        OnBugsStatChange(statName);
   }

   public static Action ShowTips;
   public static void OnTipsShow()
   {
        if(ShowTips == null) return;
        OnTipsShow();
   }

}
