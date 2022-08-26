using GenericDatabase.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Extensions {

	public static class RowExt {

		public static bool IsInTable<DB, T, R, TKey>(this IRow<DB, T, R, TKey> r)
			where DB : IDatabase where T : ITable<DB, T, R, TKey> where R : IRow<DB, T, R, TKey>
		=> r.Table.Contains(r.Key);

		public static bool Remove<DB, T, R, TKey>(this IRow<DB, T, R, TKey> r)
			where DB : IDatabase where T : ITable<DB, T, R, TKey> where R : IRow<DB, T, R, TKey>
			=> r.Table.Remove(r.Key);
	}
}
