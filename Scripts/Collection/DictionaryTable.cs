using GenericDatabase.Interfaces;
using Gist2.Extensions.LinqExt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericDatabase.Collection {

	public class DictionaryTable<DB, T, R> : ITable<DB, T, R>
		where DB : IDatabase
		where T : ITable<DB, T, R>
		where R : IRow<DB, T, R> {

		protected Dictionary<int, R> table = new Dictionary<int, R>();
		protected int idCounter = 0;

		public DictionaryTable(DB db) {
			this.Database = db;
		}

		#region interface

		#region IEnumerable
		public IEnumerator<R> GetEnumerator() => table.Values.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		#endregion

		#region ICollection
		public int Count => table.Count;
		public bool IsReadOnly => false;
		void ICollection<R>.Add(R item) => Add(item);
		public bool Remove(R item) => table.Remove(item.Key);
		public void Clear() => table.Clear();
		public bool Contains(R item) => table.ContainsKey(item.Key);
		public void CopyTo(R[] array, int arrayIndex) => table.Values.CopyTo(array, arrayIndex);
		#endregion

		#region IReadOnlyDictionary
		public IEnumerable<int> Keys => table.Keys;
		public IEnumerable<R> Values => table.Values;
		public R this[int key] => table[key];
		public bool ContainsKey(int key) => table.ContainsKey(key);
		public bool TryGetValue(int key, out R value) => table.TryGetValue(key, out value);
		IEnumerator<KeyValuePair<int, R>> IEnumerable<KeyValuePair<int, R>>.GetEnumerator() => table.GetEnumerator();
		#endregion

		#region ITable
		public virtual DB Database { get; }

		public virtual bool Add(R item) {
			var isNew = !table.ContainsKey(item.Key);
			table.Add(item.Key, item);
			return isNew;
		}
		public virtual bool Remove(int key) => table.Remove(key);
		public virtual bool Contains(int key) => table.ContainsKey(key);

		public virtual R CreateRow(int key) => (R)CTOR.Invoke(new object[] { Database, this, key });
		public virtual R CreateRow() => CreateRow(CreateUniqueID());
		#endregion

		#endregion

		#region member
		protected virtual int CreateUniqueID() => idCounter++;
		protected virtual ConstructorInfo CTOR { get; } = typeof(R).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(DB), typeof(T), typeof(int) }, null);
		#endregion
	}
}
