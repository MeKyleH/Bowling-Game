﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

	private ActionMaster actionMaster;

	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;


	[SetUp]
	public void Setup() {
		actionMaster = new ActionMaster ();
	}

	[Test]
	public void T00PassingTest() {
		Assert.AreEqual (1, 1);
	}
	[Test]
	public void T01OneStrikeReturnsEndTurn() {
		Assert.AreEqual (endTurn, actionMaster.Bowl(10));
	}

	[Test]
	public void T02Bowl8ReturnsTidy() {
		Assert.AreEqual (tidy, actionMaster.Bowl(8));
	}

	[Test]
	public void T03BowlASpareReturnsEndTurn() {
		actionMaster.Bowl (8);
		Assert.AreEqual (endTurn, actionMaster.Bowl (2));
	}

	[Test]
	public void T04CheckResetStrikeInLastFrame(){
		for (int i = 0; i < 18; i++) {
			actionMaster.Bowl (1);
		}
		Assert.AreEqual (reset, actionMaster.Bowl (10));
	}

	[Test]
	public void T05CheckResetSpareInLastFrame(){
		for (int i = 0; i < 18; i++) {
			actionMaster.Bowl (1);
		}
		actionMaster.Bowl (1);
		Assert.AreEqual (reset, actionMaster.Bowl (9));
	}

	[Test]
	public void T06YouTubeRollsEndInEndGame() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2};
		foreach (int roll in rolls) {
			actionMaster.Bowl (roll);
		}
		Assert.AreEqual(endGame, actionMaster.Bowl(9));
	}

	[Test]
	public void T07BowlEndsAtBowl20(){
		for (int i = 0; i < 19; i++) {
			actionMaster.Bowl (1);
		}
		Assert.AreEqual (endGame, actionMaster.Bowl (1));
	}
}