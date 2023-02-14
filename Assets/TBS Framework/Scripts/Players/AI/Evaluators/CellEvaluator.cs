﻿using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Units;
using UnityEngine;

namespace TbsFramework.Players.AI.Evaluators
{
    public abstract class CellEvaluator : MonoBehaviour
    {
        public float Weight = 1;

        public virtual void Precalculate(Unit evaluatingUnit, Player currentPlayer, CellGrid cellGrid) { }
        public abstract float Evaluate(Cell cellToEvaluate, Unit evaluatingUnit, Player currentPlayer, CellGrid cellGrid);
    }
}
