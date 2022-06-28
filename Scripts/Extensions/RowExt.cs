using GenericDatabase.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericDatabase.Extensions {

	public static class RowExt {

		public static bool IsInTable<DB, T, R>(this IRow<DB, T, R> r)
			where DB : IDatabase where T : ITable<DB, T, R> where R : IRow<DB, T, R>
			=> r.Table.Contains(r.Key);

		public static bool Remove<DB, T, R>(this IRow<DB, T, R> r)
			where DB : IDatabase where T : ITable<DB, T, R> where R : IRow<DB, T, R>
			=> r.Table.Remove(r.Key);
	}
}
