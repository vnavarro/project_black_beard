using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path{

	private List<Vector2> nodes;

	public Path() {
		this.nodes = new List<Vector2>();
	}

	public void AddNode(Vector2 node) {
		nodes.Add(node);
	}

	public void AddNodes(List<Vector2> newNodes){
		nodes.AddRange (newNodes);
	}

	public List<Vector2> GetNodes() {
	  return nodes;
	}
}