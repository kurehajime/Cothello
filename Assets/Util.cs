using System.Collections;
using System.Collections.Generic;

public static class Util {
	const int OCT = 8;
	enum Arrow{
		LEFT_TOP,
		TOP,
		RIGHT_TOP,
		LEFT,
		RIGHT,
		LEFT_BOTTOM,
		BOTTOM,
		RIGHT_BOTTOM
	}

	/// <summary>
	/// 石を置いた後のマップを返す
	/// </summary>
	/// <returns>The change map.</returns>
	/// <param name="map">Map.</param>
	/// <param name="point">Point.</param>
	/// <param name="val">Value.</param>
	public static int[] GetChangeMap(int[] map,int point,int val){
		int[] result = (int[])map.Clone ();
		bool ok = false;
		result [point] = val;
		foreach (int target in GetEffect(result,point)) {
			ok = true;
			result [target] =-1 * result [target];;
		}
		if (ok == true) {
			return result;
		} else {
			return new int[0];
		}
	}

	/// <summary>
	/// おけるマスを返す
	/// </summary>
	/// <returns>The putable.</returns>
	/// <param name="map">Map.</param>
	/// <param name="point">Point.</param>
	/// <param name="val">Value.</param>
	public static int[] GetPutable(int[] map,int point,int val){
		var result = new List<int> ();
		var arrows = new Arrow[8]{
			Arrow.LEFT_TOP,
			Arrow.TOP,
			Arrow.RIGHT_TOP,
			Arrow.LEFT,
			Arrow.RIGHT,
			Arrow.LEFT_BOTTOM,
			Arrow.BOTTOM,
			Arrow.RIGHT_BOTTOM,
		};
		for (int target = 0; target < map.Length; target++) {
			if(map[target]!=0){
				break;
			}
			foreach (var arrow in arrows) {
				if (getEffectLine (map, target, getLine (target, arrow)).Count > 0) {
					result.Add (target);
					break;
				}
			}
		}
		return result.ToArray();
	}

	/// <summary>
	/// 反転するマスを返す
	/// </summary>
	/// <returns>The effect.</returns>
	/// <param name="map">Map.</param>
	/// <param name="point">Point.</param>
	public static List<int> GetEffect(int[] map,int point){
		var result = new List<int> ();
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.LEFT_TOP)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.TOP)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.RIGHT_TOP)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.LEFT)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.RIGHT)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.LEFT_BOTTOM)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.BOTTOM)));
		result.AddRange (getEffectLine (map, point,getLine (point, Arrow.RIGHT_BOTTOM)));
		return result;
	}

	/// <summary>
	/// そのラインの反転するマスを返す
	/// </summary>
	/// <returns>The effect line.</returns>
	/// <param name="map">Map.</param>
	/// <param name="point">Point.</param>
	/// <param name="line">Line.</param>
	private static List<int> getEffectLine(int[] map,int point,List<int> line){
		int targetVal = 0;
		int pointVal= map [point];
		var result = new List<int> ();

		foreach (int target in line) {
			targetVal = map [target];
			if(targetVal*pointVal<0){
				result.Add (target);
			}
			if (targetVal == 0) {
				break;
			}
			if (target * point < 0) {
				return result;
			}
		}
		return new List<int>();
	}

	/// <summary>
	/// ラインを返す
	/// </summary>
	/// <returns>The Line.</returns>
	/// <param name="point">Point.</param>
	private static List<int> getLine(int point,Arrow arrow){
		var result = new List<int> ();
		int target = 0;
		switch (arrow) {
		case Arrow.TOP:
			while (true) {
				target = target - OCT;
				if (target < 0) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.BOTTOM:
			while (true) {
				target = target + OCT;
				if (target >= OCT * OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.LEFT:
			while (true) {
				target = target - 1;
				if (target % OCT > point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.RIGHT:
			while (true) {
				target = target + 1;
				if (target % OCT < point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.LEFT_TOP:
			while (true) {
				target = target -OCT + 1;
				if (target < 0||target % OCT > point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.RIGHT_TOP:
			while (true) {
				target = target -OCT - 1;
				if (target < 0||target % OCT < point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.LEFT_BOTTOM:
			while (true) {
				target = target +OCT - 1;
				if (target >= OCT * OCT||target % OCT > point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		case Arrow.RIGHT_BOTTOM:
			while (true) {
				target = target +OCT + 1;
				if (target >= OCT * OCT||target % OCT < point % OCT) {
					break;
				}
				result.Add (target);
			}
			break;
		default:
			break;
		}
		return result;
	}
}
