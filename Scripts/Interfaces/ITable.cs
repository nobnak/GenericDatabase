using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Interfaces {

	public interface ITable<out DB, out T, R> : ICollection<R>, IReadOnlyDictionary<int, R> 
		where DB : IDatabase
		where T : ITable<DB, T, R>
		where R : IRow<DB, T, R> {

		DB Database { get; }

		new bool Add(R item);
		bool Contains(int key);
		bool Remove(int key);
	}
}
