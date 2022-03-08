using GenericDatabase.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Collection {

	public class BaseRow<DB, T, R> : IRow<DB, T, R>
		where DB : IDatabase
		where T : ITable<DB, T, R>
		where R : IRow<DB, T, R> {

		public BaseRow(DB db, T table, int key) {
			this.Database = db;
			this.Table = table;
			this.Key = key;
		}

		public DB Database { get; }
		public T Table { get; }
		public int Key { get; }

	}
}
