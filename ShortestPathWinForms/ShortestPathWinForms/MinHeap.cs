using System;
using System.Collections.Generic;

public class MinHeap<TKey, TValue> where TValue : IComparable<TValue>
{
	#region initial values and constructor

	// in order to modify values in heap
	// keep dinmaic array with key,value (where key is vertex,value weight) 
	// keep dictionary (where key is the same vertex,value index of key,value in array)

	private List<KeyValuePair<TKey, TValue>> heap;
	private Dictionary<TKey, int> indices;
	private IComparer<TValue> comparer;

	public MinHeap(IEnumerable<KeyValuePair<TKey, TValue>> items, IComparer<TValue> comparer)
	{
		heap = new List<KeyValuePair<TKey, TValue>>();
		indices = new Dictionary<TKey, int>();
		this.comparer = comparer;
	}
	#endregion

	#region something I get from Internet :D

	public MinHeap(IComparer<TValue> comparer) : this(new KeyValuePair<TKey, TValue>[0], comparer)
	{ }
	public MinHeap() : this(Comparer<TValue>.Default)
	{ }

	#endregion

	#region main functionality
	/// <summary>
	/// Inserts vertex and weight pair in heap
	/// </summary>
	/// <param name="vertex"> vertex at graph</param>
	/// <param name="weight"> vertex -source vertex distance </param>
	public void Add(TKey vertex, TValue weight)
	{
		int count = heap.Count;
		indices.Add(vertex, count);
		heap.Add(new KeyValuePair<TKey, TValue>(vertex, weight));
		MoveUp(count);
	}

	/// <summary>
	/// Returns and removes min element from heap
	/// </summary>
	/// <returns> min element</returns>
	public KeyValuePair<TKey, TValue> RemoveMin()
	{
		int count = heap.Count - 1;

		if (count == -1)
		{
			throw new InvalidOperationException("Heap is empty.");
		}

		var min = heap[0];
		Swap(0, count);
		indices.Remove(heap[count].Key);
		heap.RemoveAt(count);

		if (heap.Count > 1)
		{
			indices[heap[0].Key] = 0;
			MoveDown(0);
		}
		return min;
	}

	/// <summary>
	/// Checks whether the heap is empty or not
	/// </summary>
	/// <returns></returns>
	public bool Empty()
	{
		int count = heap.Count;
		return count == 0;
	}

	/// <summary>
	/// Returns min element in heap
	/// </summary>
	/// <returns>min element</returns>
	public KeyValuePair<TKey, TValue> Peek()
	{
		return heap[0];
	}

	/// <summary>
	/// Chnages vertex's value to the new one
	/// </summary>
	/// <param name="vertex">where the value should be changed</param>
	/// <param name="newWeight"> new value(in this case newWeight from vertex to source vertex)</param>
	public void ChangeValue(TKey vertex, TValue newWeight)
	{
		int index = indices[vertex];
		int compareVal = comparer.Compare(newWeight, heap[index].Value);
		heap[index] = new KeyValuePair<TKey, TValue>(heap[index].Key, newWeight);

		if (compareVal > 0)
		{
			MoveDown(index);
		}
		else if (compareVal < 0)
		{
			MoveUp(index);
		}
	}

	/// <summary>
	/// Gets value of vertex
	/// </summary>
	/// <param name="vertex"></param>
	/// <returns></returns>
	public TValue GetValue(TKey vertex)
	{
		var index = indices[vertex];
		return heap[index].Value;
	}
	/// <summary>
	/// wether heap contains the vertex or not
	/// </summary>
	/// <returns>true if it's ther ,otherwise false</returns>
	public bool ContainsVertex(TKey vertex)
	{
		return indices.ContainsKey(vertex);
	}

	/// <summary>
	/// Gets value of vertex
	/// </summary>
	/// <returns> returns true in case of success , false otherwise </returns>
	public bool TryGetValue(TKey vertex, out TValue weight)
	{
		int index;
		if (indices.TryGetValue(vertex, out index))
		{
			weight = heap[index].Value;
			return true;
		}

		weight = default(TValue);
		return false;
	}

	#endregion

	#region help functions

	/// <summary>
	/// MoveUp/MinHeapify ,modifies the heap,so it meets minHeaps needs
	/// </summary>
	/// <param name="index">begin at index</param>
	public void MoveUp(int index)
	{
		int parent = index / 2;

		while (index > 0 && CompareResult(parent, index) > 0)
		{
			Swap(index, parent);
			index = parent;
			parent /= 2;
		}
	}

	/// <summary>
	/// MoveDown/MaxHeapify
	/// </summary>
	private void MoveDown(int index)
	{
		int min;

		while (true)
		{
			int left = index * 2;
			int right = index * 2 + 1;

			if (left < heap.Count && CompareResult(left, index) < 0)
				min = left;
			else
				min = index;

			if (right < heap.Count && CompareResult(right, min) < 0)
				min = right;

			if (min != index)
			{
				Swap(index, min);
				index = min;
			}
			else
				return;
		}
	}
	private int CompareResult(int index1, int index2)
	{
		return comparer.Compare(heap[index1].Value, heap[index2].Value);
	}

	private void Swap(int index, int max)
	{
		var tmp = heap[index];
		heap[index] = heap[max];
		heap[max] = tmp;

		indices[heap[index].Key] = index;
		indices[heap[max].Key] = max;
	}

	#endregion

}