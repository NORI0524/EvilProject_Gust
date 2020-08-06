using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager
{
    public enum Rank
    {
        Beginner,
        Bronze,
        Iron,
        Silver,
        Gold,
        Ruby,
        Platinum,
        Diamond,
        Master,
        Predator,
        Num,
    }

    private class RankJudge
    {
        public const int Beginner = 0;
        public const int Bronze = 500;
        public const int Iron = 800;
        public const int Silver = 1000;
        public const int Gold = 1500;
        public const int Ruby = 1800;
        public const int Platinum = 2000;
        public const int Diamond = 2500;
        public const int Master = 2800;
        public const int Predator = 3000;
    }




    public static Rank GetRank(int value)
    {
        Rank rank = Rank.Beginner;
        if (value >= RankJudge.Bronze)
        {
            rank = Rank.Bronze;
        }
        if (value >= RankJudge.Iron)
        {
            rank = Rank.Iron;
        }
        if (value >= RankJudge.Silver)
        {
            rank = Rank.Silver;
        }
        if (value >= RankJudge.Gold)
        {
            rank = Rank.Gold;
        }
        if (value >= RankJudge.Ruby)
        {
            rank = Rank.Ruby;
        }
        if (value >= RankJudge.Platinum)
        {
            rank = Rank.Platinum;
        }
        if (value >= RankJudge.Diamond)
        {
            rank = Rank.Diamond;
        }
        if (value >= RankJudge.Master)
        {
            rank = Rank.Master;
        }
        if (value >= RankJudge.Predator)
        {
            rank = Rank.Predator;
        }
        return rank;
    }

}
