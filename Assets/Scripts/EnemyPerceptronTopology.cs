using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyPerceptronTopology {
	public static int InputLayerSize {
		get { return 9; }
	}
	public static int OutputLayerSize {
		get { return 4; }
	}
	public static int HiddenLayerSize {
		get { return 5; }
	}

	public class prevLayerConnections {
		private int[] indices;
		public int this[int index] {
			get { return indices[index]; }
			set { indices[index] = value; }
		}
		
		public prevLayerConnections() {
			indices = new int[0];
		}
		public prevLayerConnections(int[] ind) {
			indices = ind;
		}
		public int Size {
			get { return indices.Length; }
		}
	}

	private static prevLayerConnections[] hiddenLayer = new prevLayerConnections[5] {
		new prevLayerConnections(new int[] {0, 1, 2, 3, 4, 5, 8}),
		new prevLayerConnections(new int[] {0, 1, 2, 3, 8}),
		new prevLayerConnections(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8}),
		new prevLayerConnections(new int[] {6, 7, 8}),
		new prevLayerConnections(new int[] {6, 7, 8})
	};
	public static prevLayerConnections[] HiddenLayer {
		get { return hiddenLayer; }
	}

	private static prevLayerConnections[] outputLayer = new prevLayerConnections[4]{
		new prevLayerConnections(new int[] {9, 10, 12}),
		new prevLayerConnections(new int[] {9, 10, 12}),
		new prevLayerConnections(new int[] {11, 13}),
		new prevLayerConnections(new int[] {11, 13})
	};
	public static prevLayerConnections[] OutputLayer {
		get { return outputLayer; }
	}
}
