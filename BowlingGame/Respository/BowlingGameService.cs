using System;
using System.Collections.Generic;
using BowlingGame.Models;

namespace BowlingGame.Respository
{
    public class BowlingGameService : IBowlingGameService
    {
        private readonly List<int> _rolls;
        private readonly Random _random;
        private const int FRAMES = 10;
        private const int PERFECT = 10;
        public BowlingGameService()
        {
            _rolls = new List<int>();
            _random = new Random();
        }

        public BowlingGameModel PlayRadom()
        {
            BowlingGameModel bowling = new BowlingGameModel();
            for (int i = 0; i < FRAMES; i++)
            {
                int pin1 = GenerateRandPin(PERFECT+1);
                AddPin1(bowling, pin1, i);
                if (i < FRAMES - 1)
                {
                    if (pin1 < PERFECT)
                    {
                        int pin2 = GenerateRandPin(PERFECT - pin1+1);
                        AddPin2(bowling, pin2, i);
                    }
                }
                else
                    GetTenth(bowling, pin1, i);
            }
            bowling.score = Score();
            return bowling;
        }

        public BowlingGameModel PlayStrike()
        {
            BowlingGameModel bowling = new BowlingGameModel();
            int rowIndex = GenerateRandPin(PERFECT);
            for (int i = 0; i < FRAMES; i++)
            {
                int pin1;
                if (i == rowIndex)
                    pin1 = PERFECT;
                else
                    pin1 = GenerateRandPin(PERFECT+1);
                AddPin1(bowling, pin1, i);
                if (i < FRAMES - 1)
                {
                    if (pin1 < PERFECT)
                    {
                        int pin2 = GenerateRandPin(PERFECT - pin1 + 1);
                        AddPin2(bowling, pin2, i);
                    }
                }
                else
                    GetTenth(bowling, pin1, i);                
            }
            bowling.score = Score();
            return bowling;
        }
        public BowlingGameModel PlaySpare()
        {
            BowlingGameModel bowling = new BowlingGameModel();
            int rowIndex = GenerateRandPin(PERFECT);
            for (int i = 0; i < FRAMES; i++)
            {
                int pin2 = 0;
                int pin1 = GenerateRandPin(PERFECT);              
                AddPin1(bowling, pin1, i);
                if (i < FRAMES - 1)
                {
                    if (pin1 < PERFECT)
                    {
                        if (i == rowIndex)
                            pin2 = PERFECT - pin1;
                        else
                            pin2 = GenerateRandPin(PERFECT - pin1 + 1);
                        AddPin2(bowling, pin2, i);
                    }
                }          
                else
                    GetTenth(bowling, pin1, i);           
            }
            bowling.score = Score();
            return bowling;
        }

        public BowlingGameModel PlayTenth()
        { 
            BowlingGameModel bowling = new BowlingGameModel();
            for(int i =0; i< FRAMES; i++)
            {
                int pin1 = 0;
                if (i == FRAMES-1)
                    pin1 = PERFECT;
                else
                    pin1 = GenerateRandPin(PERFECT);
                AddPin1(bowling, pin1, i);               
                if (i < FRAMES - 1)
                {
                    if (pin1 < PERFECT)
                    {
                        int pin2 = GenerateRandPin(PERFECT - pin1+1);
                        AddPin2(bowling, pin2, i);
                    }
                }
               else                
                    GetTenth(bowling, pin1, i);                
            }
            bowling.score = Score();
            return bowling;
        }

        public BowlingGameModel PlayPerfect()
        {
            BowlingGameModel bowling = new BowlingGameModel();
            for (int i = 0; i < FRAMES; i++)
            {
                int pin1 = PERFECT;
                AddPin1(bowling, pin1, i);
                
                if (i == FRAMES - 1 )
                {
                    int pin2 = PERFECT;
                    AddPin2(bowling, pin2, i);
                    int pin3 = PERFECT;
                    AddPin3(bowling, pin3);
                }               
            }
            bowling.score = Score();
            return bowling;
        }

        public void Roll(int pins)
        {
            _rolls.Add(pins);
        }

        public int Score()
        {
            int score = 0;
            int rollIndex = 0;
            for (int i = 0; i < FRAMES; i++)
            {
                if (IsStrike(rollIndex))
                {
                    score = score + GetStrikeScore(rollIndex);
                    rollIndex = rollIndex + 1;
                }
                else if (IsSpare(rollIndex))
                {
                    score = score + GetSpareScore(rollIndex);
                    rollIndex = rollIndex + 2;
                }
                else
                {
                    score = score + GetNormalScore(rollIndex);
                    rollIndex = rollIndex + 2;
                }
            }
            return score;
        }
        private int GenerateRandPin(int max)
        {
            return _random.Next(max);
        }
        private void AddPin1(BowlingGameModel bowling, int pin1,  int rollIndex)
        {
           
            bowling.rollArray[rollIndex, 0] = pin1.ToString();
            _rolls.Add(pin1);
        }
        private void AddPin2(BowlingGameModel bowling, int pin2, int rollIndex)
        {                       
               _rolls.Add(pin2);
               bowling.rollArray[rollIndex, 1] = pin2.ToString();            
        }
        
        private void CreatePin3(BowlingGameModel bowling, int pin2)
        {
            int pin3 = 0;
            if (pin2 == PERFECT)
                pin3 = GenerateRandPin(PERFECT + 1);
            else
                pin3 = GenerateRandPin(PERFECT - pin2 + 1);
            AddPin3(bowling, pin3);
        }
        private void AddPin3(BowlingGameModel bowling, int pin3)
        {
            _rolls.Add(pin3);
            bowling.bonus = pin3.ToString();
        }
        private void GetTenth(BowlingGameModel bowling, int pin1, int rollIndex)
        {            
            int pin3 = 0;                     
            int pin2 = 0;
            bool add_pin3 = false;

            if (pin1 == PERFECT)
            {
                pin2 = GenerateRandPin(PERFECT + 1);
                AddPin2(bowling, pin2, rollIndex);
                add_pin3 = true;
            }
            else
            {
                pin2 = GenerateRandPin(PERFECT - pin1 + 1);
                AddPin2(bowling, pin2, rollIndex);
                if ((pin1 + pin2) == PERFECT)
                    add_pin3 = true;
            }           
            if (add_pin3==true)
                CreatePin3(bowling, pin2);
        }
       

        private bool IsSpare(int rollIndex)
        {
            return (_rolls[rollIndex] + _rolls[rollIndex + 1] == PERFECT);
        }

        private bool IsStrike(int rollIndex)
        {
            return (_rolls[rollIndex] == PERFECT);
        }
        private int GetStrikeScore(int rollIndex)
        {
            return _rolls[rollIndex] + _rolls[rollIndex + 1] + _rolls[rollIndex + 2];
        }
        private int GetSpareScore(int rollIndex)
        {
            return _rolls[rollIndex] + _rolls[rollIndex + 1] + _rolls[rollIndex + 2];
        }

        private int GetNormalScore(int rollIndex)
        {
            return _rolls[rollIndex] + _rolls[rollIndex + 1];
        }
    }


}
