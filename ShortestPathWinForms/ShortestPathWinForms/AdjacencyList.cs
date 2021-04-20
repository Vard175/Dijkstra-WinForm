using System;
using System.Collections.Generic;

public class AdjacencyList
{
	// there are more methods than I need
	LinkedList<KeyValuePair<int, int>>[] adj;
	public AdjacencyList(int vertices)
	{
		adj = new LinkedList<KeyValuePair<int, int>>[vertices];

		for (int i = 0; i < adj.Length; ++i)
		{
			adj[i] = new LinkedList<KeyValuePair<int, int>>();
		}
	}

	/// <summary>
	///  Appends a new Edge to the linked list
	/// </summary>
	public void AddEdge(int startVertex, int endVertex, int weight)
	{
		adj[startVertex].AddLast(new KeyValuePair<int, int>(endVertex, weight));
	}

	/// <summary>
	/// Returns number of vertices
	/// Does not change for an object
	/// </summary>
	/// <returns></returns>
	public int GetNumberOfVertices()
	{
		return adj.Length;
	}

	/// <summary>
	/// weight from vertex1 to vertex2
	/// </summary>
	/// <returns>Returns Weight</returns>
	public int GetWeight(int vertex1, int vertex2)
	{
		var list = adj[vertex1];
		int w = -1;
		foreach (KeyValuePair<int, int> edge in list)
		{
			if (edge.Key == vertex2)
			{
				w = edge.Value;
				break;
			}

		}
		return w;
	}

	/// <summary>
	/// Prints the Adjacency List
	/// </summary>
	public void PrintAdjacencyList()
	{
		int i = 0;

		foreach (LinkedList<KeyValuePair<int, int>> list in adj)
		{
			Console.Write("adjacencyList[" + i + "] -> ");

			foreach (KeyValuePair<int, int> edge in list)
			{
				Console.Write(edge.Key + "(" + edge.Value + ")  ");
			}

			++i;
			Console.WriteLine();
		}
	}

	/// <summary>
	/// get adjacences of vertex
	/// </summary>
	public List<KeyValuePair<int, int>> GetAdjacences(int vertex)
	{
		List<KeyValuePair<int, int>> arr = new List<KeyValuePair<int, int>>();

		var list = adj[vertex];
		foreach (KeyValuePair<int, int> edge in list)
			arr.Add(new KeyValuePair<int, int>(edge.Key, edge.Value));

		return arr;
	}

	/// <summary>
	/// gets number of adjacences of vertex
	/// </summary>
	public int GetNumOfAdj(int vertex)
	{
		var list = adj[vertex];
		return list.Count;
	}
}