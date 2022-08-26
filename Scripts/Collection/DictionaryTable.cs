using GenericDatabase.Interfaces;
using Gist2.Extensions.LinqExt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericDatabase.Collection {

	public class DictionaryTable<DB, T, R, TKey> : ITable<DB, T, R, TKey>
		where DB : IDatabase
		where T : ITable<DB, T, R, TKey>
		where R : IRow<DB, T, R, TKey> {

		protected Dictionary<TKey, R> table = new Dictionary<TKey, R>();
		//protected int idCounter = 0;

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
		void ICollection<R>.Add(R item) => _Add(item);
		public bool Remove(R item) => _Remove(item.Key);
		public void Clear() => _Clear();
		public bool Contains(R item) => table.ContainsKey(item.Key);
		public void CopyTo(R[] array, int arrayIndex) => table.Values.CopyTo(array, arrayIndex);
		#endregion

		#region IReadOnlyDictionary
		public IEnumerable<TKey> Keys => table.Keys;
		public IEnumerable<R> Values => table.Values;
		public R this[TKey key] => table[key];
		public bool ContainsKey(TKey key) => table.ContainsKey(key);
		public bool TryGetValue(TKey key, out R value) => table.TryGetValue(key, out value);
		IEnumerator<KeyValuePair<TKey, R>> IEnumerable<KeyValuePair<TKey, R>>.GetEnumerator() => table.GetEnumerator();
		#endregion

		#region ITable
		public virtual DB Database { get; }

		public virtual bool Add(R item) => _Add(item);
		public virtual bool Remove(TKey key) => _Remove(key);
		public virtual bool Contains(TKey key) => table.ContainsKey(key);

		public virtual R CreateRow(TKey key) => (R)CTOR.Invoke(new object[] { Database, this, key });
		//public virtual R CreateRow() => CreateRow(CreateUniqueID());
		#endregion

		//public virtual int CreateUniqueID() => idCounter++;
		#endregion

		#region member
		protected virtual bool _Add(R item) {
			var isNew = !table.ContainsKey(item.Key);
			table[item.Key] = item;
			return isNew;
		}
		protected virtual bool _Remove(TKey key) => table.Remove(key);
		protected virtual void _Clear() => table.Clear();

		protected virtual ConstructorInfo CTOR { get; } = typeof(R).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(DB), typeof(T), typeof(int) }, null);
		#endregion
	}
}
