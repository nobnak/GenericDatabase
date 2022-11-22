using GenericDatabase.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenericDatabase.Extensions {

	public static class TableExt {

		public static void RemoveAll<DB, T, R, TKey>(
			this ITable<DB, T, R, TKey> table, 
			Predicate<R> match)
			where DB : IDatabase
			where T : ITable<DB, T, R, TKey>
			where R : IRow<DB, T, R, TKey> 
		{

			foreach (var k in table.Values.Where(v => match(v)).ToArray())
				table.Remove(k);
		}
	}
}
