﻿using System;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Players;
using TbsFramework.Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TbsFramework.Example1
{
    public class GuiController : MonoBehaviour
    {
        public CellGrid CellGrid;
        public Button NextTurnButton;

        public Image UnitImage;
        public Text InfoText;
        public Text StatsText;
        public Text NameStats;

        void Awake()
        {
            UnitImage.color = Color.gray;

            CellGrid.GameStarted += OnGameStarted;
            CellGrid.TurnEnded += OnTurnEnded;
            CellGrid.GameEnded += OnGameEnded;
            CellGrid.UnitAdded += OnUnitAdded;
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            foreach (Transform cell in CellGrid.transform)
            {
                cell.GetComponent<Cell>().CellHighlighted += OnCellHighlighted;
                cell.GetComponent<Cell>().CellDehighlighted += OnCellDehighlighted;
            }

            OnTurnEnded(sender, e);
        }

        private void OnGameEnded(object sender, EventArgs e)
        {
            InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + " wins!";
            var remainingHP = (sender as CellGrid).Units.Where(u => u.PlayerNumber == (sender as CellGrid).CurrentPlayerNumber).Sum(u => u.HitPoints);
        }
        private void OnTurnEnded(object sender, EventArgs e)
        {
            NextTurnButton.interactable = ((sender as CellGrid).CurrentPlayer is HumanPlayer);

            InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1);
        }
        private void OnCellDehighlighted(object sender, EventArgs e)
        {
            UnitImage.color = Color.gray;
            StatsText.text = "";
            NameStats.text = "";
        }
        private void OnCellHighlighted(object sender, EventArgs e)
        {
            UnitImage.color = Color.gray;
            StatsText.text = "Movement Cost: " + (sender as Cell).MovementCost;
        }
        private void OnUnitAttacked(object sender, AttackEventArgs e)
        {
            if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;
            OnUnitDehighlighted(sender, EventArgs.Empty);

            if ((sender as Unit).HitPoints <= 0) return;

            OnUnitHighlighted(sender, e);
        }
        private void OnUnitDehighlighted(object sender, EventArgs e)
        {
            StatsText.text = "";
            NameStats.text = "";
            UnitImage.color = Color.gray;
        }
        private void OnUnitHighlighted(object sender, EventArgs e)
        {
            var unit = sender as MyUnit;
            NameStats.text = unit.UnitName;
            StatsText.text = "\nHit Points: " + unit.HitPoints + "/" + unit.TotalHitPoints + "\nAttack: " + unit.AttackFactor + "\nDefence: " + unit.DefenceFactor + "\nRange: " + unit.AttackRange;
            UnitImage.color = unit.GetComponentInChildren<Renderer>().material.color;
        }
        private void OnUnitAdded(object sender, UnitCreatedEventArgs e)
        {
            RegisterUnit(e.unit);
        }

        private void RegisterUnit(Transform unit)
        {
            unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
            unit.GetComponent<Unit>().UnitAttacked += OnUnitAttacked;
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}
