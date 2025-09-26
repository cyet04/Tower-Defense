//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class Enemy_Tower : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	private EnemyInfo[] _EnemyInfoItems;

	public EnemyInfo GetEnemyInfo(int id) {
		int min = 0;
		int max = _EnemyInfoItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			EnemyInfo item = _EnemyInfoItems[index];
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
		EnemyInfo GetEnemyInfo(int id);
	}

	private class DataGetter : IDataGetter {
		private Func<int, EnemyInfo> _GetEnemyInfo;
		public EnemyInfo GetEnemyInfo(int id) {
			return _GetEnemyInfo(id);
		}
		public DataGetter(Func<int, EnemyInfo> getEnemyInfo) {
			_GetEnemyInfo = getEnemyInfo;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetEnemyInfo);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class EnemyInfo {

	[SerializeField]
	private int _Id;
	public int id { get { return _Id; } }

	[SerializeField]
	private string _Name;
	public string name { get { return _Name; } }

	[SerializeField]
	private float _Hp;
	public float hp { get { return _Hp; } }

	[SerializeField]
	private float _Speed;
	public float speed { get { return _Speed; } }

	[SerializeField]
	private float _Damage_base;
	public float damage_base { get { return _Damage_base; } }

	[SerializeField]
	private float _Weapon_damage;
	public float weapon_damage { get { return _Weapon_damage; } }

	[SerializeField]
	private float _Armor;
	public float armor { get { return _Armor; } }

	[SerializeField]
	private float _Reward;
	public float reward { get { return _Reward; } }

	[NonSerialized]
	private int mVersion = 0;
	[NonSerialized]
	private Enemy_Tower.IDataGetter mGetter;

	public EnemyInfo Init(int version, Enemy_Tower.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mGetter = getter;
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[EnemyInfo]{{id:{0}, name:{1}, hp:{2}, speed:{3}, damage_base:{4}, weapon_damage:{5}, armor:{6}, reward:{7}}}",
			id, name, hp, speed, damage_base, weapon_damage, armor, reward);
	}

}

