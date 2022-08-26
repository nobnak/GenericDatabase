using GenericDatabase.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Collection {

	public class BaseRow<DB, T, R, TKey> : IRow<DB, T, R, TKey>
		where DB : IDatabase
		where T : ITable<DB, T, R, TKey>
		where R : IRow<DB, T, R, TKey> {

		public BaseRow(DB db, T table, TKey key) {
			this.Database = db;
			this.Table = table;
			this.Key = key;
		}

		public virtual DB Database { get; }
		public virtual T Table { get; }
		public virtual TKey Key { get; }
	}
}
