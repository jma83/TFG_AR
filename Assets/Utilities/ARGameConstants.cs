using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ARGameConstants {

    public static string SCENE_WORLD = "World";
    public static string SCENE_COMBAT = "FightScene";
    public static string SCENE_PUZZLE = "PuzzleScene";
    public enum TypeObj : int { Health = 0, BigHealth = 1 , XPMultiplier = 2 , ExtendCapture = 3 , DurabilityUP = 4 };
    public enum EquipmentQuality : int { Basic = 0, Rare = 1, Epic = 2, Legend = 3 };
    public enum EquipmentType : int { Defensive = 0, Ofensive = 1, Balanced = 2, Fast = 3 };
    public const int RATE1 = 5;
    public const int RATE2 = 20;
    public const int RATE3 = 50;
    public const int MINRATE = 0;
    public const int MAXRATE = 100;


}
