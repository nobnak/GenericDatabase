using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Interfaces {

	public interface ITable<out DB, out T, R, TKey> : ICollection<R>, IReadOnlyDictionary<TKey, R> 
		where DB : IDatabase
		where T : ITable<DB, T, R, TKey>
		where R : IRow<DB, T, R, TKey> {

		DB Database { get; }

		new bool Add(R item);
		bool Contains(TKey key);
		bool Remove(TKey key);

		R CreateRow(TKey key);
		//R CreateRow();
	}
}
