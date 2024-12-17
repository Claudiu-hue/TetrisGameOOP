﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }

        }

        public GameGrid GameGrid { get; }

        public BlockQueue BlockQueue { get; }

        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        private bool BlockFits()
        {
            foreach(Position p in CurrentBlock.TilePosition())
            {
                if(!GameGrid.IsEmpty(p.Row,p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if(!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if(!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0,1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach(Position p in CurrentBlock.TilePosition())
            {
                GameGrid[p.Row, p.Column] = currentBlock.Id;
            }

            GameGrid.ClearFullRows();

            if(IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                currentBlock = BlockQueue.GetAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if(!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
        
    }
}
