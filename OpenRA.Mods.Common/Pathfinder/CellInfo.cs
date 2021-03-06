﻿#region Copyright & License Information
/*
 * Copyright 2007-2015 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System;
using System.Collections.Generic;

namespace OpenRA.Mods.Common.Pathfinder
{
	/// <summary>
	/// Describes the three states that a node in the graph can have.
	/// Based on A* algorithm specification
	/// </summary>
	public enum CellStatus
	{
		Unvisited,
		Open,
		Closed
	}

	/// <summary>
	/// Stores information about nodes in the pathfinding graph
	/// </summary>
	public struct CellInfo
	{
		/// <summary>
		/// The cost to move from the start up to this node
		/// </summary>
		public readonly int CostSoFar;

		/// <summary>
		/// The estimation of how far is the node from our goal
		/// </summary>
		public readonly int EstimatedTotal;

		/// <summary>
		/// The previous node of this one that follows the shortest path
		/// </summary>
		public readonly CPos PreviousPos;

		/// <summary>
		/// The status of this node
		/// </summary>
		public readonly CellStatus Status;

		public CellInfo(int costSoFar, int estimatedTotal, CPos previousPos, CellStatus status)
		{
			CostSoFar = costSoFar;
			PreviousPos = previousPos;
			Status = status;
			EstimatedTotal = estimatedTotal;
		}
	}

	/// <summary>
	/// Compares two nodes according to their estimations
	/// </summary>
	public class PositionComparer : IComparer<CPos>
	{
		readonly IGraph<CellInfo> graph;

		public PositionComparer(IGraph<CellInfo> graph)
		{
			this.graph = graph;
		}

		public int Compare(CPos x, CPos y)
		{
			return Math.Sign(graph[x].EstimatedTotal - graph[y].EstimatedTotal);
		}
	}
}
