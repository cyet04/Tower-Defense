//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class SlaveDataA : ScriptableObject {

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	private Sheet1[] _Sheet1Items;

	public Sheet1 GetSheet1(string name) {
		int min = 0;
		int max = _Sheet1Items.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			Sheet1 item = _Sheet1Items[index];
			if (item.name == name) { return item.Init(mVersion, DataGetterObject); }
			if (string.Compare(name, item.name) < 0) {
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
		Sheet1 GetSheet1(string name);
	}

	private class DataGetter : IDataGetter {
		private Func<string, Sheet1> _GetSheet1;
		public Sheet1 GetSheet1(string name) {
			return _GetSheet1(name);
		}
		public DataGetter(Func<string, Sheet1> getSheet1) {
			_GetSheet1 = getSheet1;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetSheet1);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Sheet1 {

	[SerializeField]
	private string _Name;
	public string name { get { return _Name; } }

	[SerializeField]
	private int _Age;
	public int age { get { return _Age; } }

	[SerializeField]
	private string _Gender;
	public string gender { get { return _Gender; } }

	[NonSerialized]
	private int mVersion = 0;
	[NonSerialized]
	private SlaveDataA.IDataGetter mGetter;

	public Sheet1 Init(int version, SlaveDataA.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mGetter = getter;
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[Sheet1]{{name:{0}, age:{1}, gender:{2}}}",
			name, age, gender);
	}

}

