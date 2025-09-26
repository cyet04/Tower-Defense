//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

public partial class ExcelExample : ScriptableObject {

	public enum eGender {
		Default, Female, Male
	}

	public enum eRelationship {
		Default, Mother, Grandfather, Grandmother, Father
	}

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	private Student[] _StudentItems;

	public Student GetStudent(int id) {
		int min = 0;
		int max = _StudentItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			Student item = _StudentItems[index];
			if (item.id == id) { return item.Init(mVersion, DataGetterObject); }
			if (id < item.id) {
				max = index;
			} else {
				min = index + 1;
			}
		}
		return null;
	}

	[SerializeField]
	private Parent[] _ParentItems;

	private Parent GetParent(string parent_id) {
		int min = 0;
		int max = _ParentItems.Length;
		while (min < max) {
			int index = (min + max) >> 1;
			Parent item = _ParentItems[index];
			if (item.parent_id == parent_id) { return item.Init(mVersion, DataGetterObject); }
			if (string.Compare(parent_id, item.parent_id) < 0) {
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
		Student GetStudent(int id);
		Parent GetParent(string parent_id);
	}

	private class DataGetter : IDataGetter {
		private Func<int, Student> _GetStudent;
		public Student GetStudent(int id) {
			return _GetStudent(id);
		}
		private Func<string, Parent> _GetParent;
		public Parent GetParent(string parent_id) {
			return _GetParent(parent_id);
		}
		public DataGetter(Func<int, Student> getStudent, Func<string, Parent> getParent) {
			_GetStudent = getStudent;
			_GetParent = getParent;
		}
	}

	[NonSerialized]
	private DataGetter mDataGetterObject;
	private DataGetter DataGetterObject {
		get {
			if (mDataGetterObject == null) {
				mDataGetterObject = new DataGetter(GetStudent, GetParent);
			}
			return mDataGetterObject;
		}
	}
}

[Serializable]
public class Student {

	[SerializeField]
	private int _Id;
	public int id { get { return _Id; } }

	[SerializeField]
	private string _Name;
	public string name { get { return _Name; } }

	[SerializeField]
	private ExcelExample.eGender _Gender;
	public ExcelExample.eGender gender { get { return _Gender; } }

	[SerializeField]
	private string _Nationality;
	public string nationality { get { return _Nationality; } }

	[SerializeField]
	private string _Father;
	private Parent _Father_;
	public Parent father {
		get {
			return _Father_;
		}
	}

	[SerializeField]
	private string _Mother;
	private Parent _Mother_;
	public Parent mother {
		get {
			return _Mother_;
		}
	}

	[SerializeField]
	private int _Grade;
	public int grade { get { return _Grade; } }

	[SerializeField]
	private int _Class_level;
	public int class_level { get { return _Class_level; } }

	[SerializeField]
	private string _Birthday;
	public string birthday { get { return _Birthday; } }

	[SerializeField]
	private int[] _Favourite_numbers;
	public int[] favourite_numbers { get { return _Favourite_numbers; } }

	[SerializeField]
	private string[] _Hobbies;
	public string[] hobbies { get { return _Hobbies; } }

	[NonSerialized]
	private int mVersion = 0;
	[NonSerialized]
	private ExcelExample.IDataGetter mGetter;

	public Student Init(int version, ExcelExample.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mGetter = getter;
		_Father_ = getter.GetParent(_Father);
		_Mother_ = getter.GetParent(_Mother);
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[Student]{{id:{0}, name:{1}, gender:{2}, nationality:{3}, father:{4}, mother:{5}, grade:{6}, class_level:{7}, birthday:{8}, favourite_numbers:{9}, hobbies:{10}}}",
			id, name, gender, nationality, father, mother, grade, class_level, birthday, array2string(favourite_numbers), array2string(hobbies));
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

[Serializable]
public class Parent {

	[SerializeField]
	private string _Parent_id;
	public string parent_id { get { return _Parent_id; } }

	[SerializeField]
	private string _Name;
	public string name { get { return _Name; } }

	[SerializeField]
	private ExcelExample.eRelationship _Relationship;
	public ExcelExample.eRelationship relationship { get { return _Relationship; } }

	[SerializeField]
	private string _Telephone;
	public string telephone { get { return _Telephone; } }

	[NonSerialized]
	private int mVersion = 0;
	[NonSerialized]
	private ExcelExample.IDataGetter mGetter;

	public Parent Init(int version, ExcelExample.IDataGetter getter) {
		if (mVersion == version) { return this; }
		mGetter = getter;
		mVersion = version;
		return this;
	}

	public override string ToString() {
		return string.Format("[Parent]{{parent_id:{0}, name:{1}, relationship:{2}, telephone:{3}}}",
			parent_id, name, relationship, telephone);
	}

}

