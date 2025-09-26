//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class Weapon_Towers : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	private WeaponInfo[] _WeaponInfoItems;

	public WeaponInfo GetWeaponInfo(int id) {
		int min = 0;
		int max = _WeaponInfoItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			WeaponInfo item = _WeaponInfoItems[index];
			if (item.id == id) { return item.Init(mVersion, DataGetterObject); }
			if (id < item.id) {
				max = index;
			} else {
				min = index + 1;
			}
		}
		return null;
	}

	public void Reset() {
		mVersion++;
	}

	public interface IDataGetter {
		WeaponInfo GetWeaponInfo(int id);
	}

	private class DataGetter : IDataGetter {
		private Func<int, WeaponInfo> _GetWeaponInfo;
		public WeaponInfo GetWeaponInfo(int id) {
			return _GetWeaponInfo(id);
		}
		public DataGetter(Func<int, WeaponInfo> getWeaponInfo) {
			_GetWeaponInfo = getWeaponInfo;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetWeaponInfo);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class WeaponInfo {

	[SerializeField]
	private int _Id;
	public int id { get { return _Id; } }

	[SerializeField]
	private string _Name;
	public string name { get { return _Name; } }

	[SerializeField]
	private int _Cost;
	public int cost { get { return _Cost; } }

	[SerializeField]
	private int _Hp;
	public int hp { get { return _Hp; } }

	[SerializeField]
	private int[] _Levels;
	public int[] levels { get { return _Levels; } }

	[SerializeField]
	private float[] _Damage;
	public float[] damage { get { return _Damage; } }

	[SerializeField]
	private float[] _Range;
	public float[] range { get { return _Range; } }

	[SerializeField]
	private float[] _Fire_rate;
	public float[] fire_rate { get { return _Fire_rate; } }

	[SerializeField]
	private float[] _Splash_radius;
	public float[] splash_radius { get { return _Splash_radius; } }

	[SerializeField]
	private string[] _Special_effect;
	public string[] special_effect { get { return _Special_effect; } }

	[NonSerialized]
	private int mVersion = 0;
	[NonSerialized]
	private Weapon_Towers.IDataGetter mGetter;

	public WeaponInfo Init(int version, Weapon_Towers.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mGetter = getter;
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[WeaponInfo]{{id:{0}, name:{1}, cost:{2}, hp:{3}, levels:{4}, damage:{5}, range:{6}, fire_rate:{7}, splash_radius:{8}, special_effect:{9}}}",
			id, name, cost, hp, array2string(levels), array2string(damage), array2string(range), array2string(fire_rate), array2string(splash_radius), array2string(special_effect));
	}

	private string array2string(Array array) {
		int len = array.Length;
		string[] strs = new string[len];
		for (int i = 0; i < len; i++) {
			strs[i] = string.Format("{0}", array.GetValue(i));
		}
		return string.Concat("[", string.Join(", ", strs), "]");
	}

}

