using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Interfaces {

	public interface IRow<out DB, out T, R> 
		where DB : IDatabase
		where T : ITable<DB, T, R>
		where R : IRow<DB, T, R> {

		public DB Database { get; }
		public T Table { get; }
		public int Key { get; }

	}

}