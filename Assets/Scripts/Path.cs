using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path{

	private List<Vector3> nodes;

	public Path() {
		this.nodes = new List<Vector3>();
	}

	public void addNode(Vector3 node) {
		nodes.Add(node);
	}

	public List<Vector3> getNodes() {
	  return nodes;
	}
}