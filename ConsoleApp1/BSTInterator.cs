// C# implementation of the approach
using System;
using System.Collections.Generic;

// Node of the binary tree
public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int x)
	{
		val = x;
	}
}

// Iterator for BST
public class BSTIterator
{

	// Stack to store the nodes
	// of BST
	Stack<TreeNode> s;

	// Constructor for the class
	public BSTIterator(TreeNode root)
	{

		// Initializing stack
		s = new Stack<TreeNode>();
		TreeNode curr = root;

		while (curr != null)
		{
			s.Push(curr);
			curr = curr.left;
		}
	}

	// Function to return
	// current element iterator
	// is pointing to
	TreeNode curr()
	{
		return s.Peek();
	}

	// Function to iterate to next
	// element of BST
	public void next()
	{
		TreeNode temp = s.Peek().right;
		s.Pop();

		while (temp != null)
		{
			s.Push(temp);
			temp = temp.left;
		}
	}

	// Function to check if
	// stack is empty
	public bool isEnd()
	{
		return s.Count != 0;
	}

	// Function to iterator to every element
	// using iterator
	public void iterate()
	{
		while (isEnd())
		{
			Console.Write(curr().val + " ");
			next();
		}
	}
}

public class BinaryTree
{

	TreeNode root;

	// Driver code
	public static void Main(String[] args)
	{

		// Let us construct a tree shown in
		// the above figure
		BinaryTree tree = new BinaryTree();
		tree.root = new TreeNode(5);
		tree.root.left = new TreeNode(3);
		tree.root.right = new TreeNode(7);
		tree.root.left.left = new TreeNode(2);
		tree.root.left.right = new TreeNode(4);
		tree.root.right.left = new TreeNode(6);
		tree.root.right.right = new TreeNode(8);

		// Iterator to BST
		BSTIterator it = new BSTIterator(tree.root);

		// Function to test iterator
		it.iterate();
	}
}

// This code is contributed by Rajput-Ji
