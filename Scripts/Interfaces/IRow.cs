using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Interfaces {

	public interface IRow<out DB, out T, R, TKey> 
		where DB : IDatabase
		where T : ITable<DB, T, R, TKey>
		where R : IRow<DB, T, R, TKey> {

		public DB Database { get; }
		public T Table { get; }
		public TKey Key { get; }

	}

}