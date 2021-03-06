using System;
using MPRV.Common;
using MPRV.Model;
using System.Collections.Generic;
using System.Linq;

namespace Intake.Core.Model.Factory
{
	/// <summary>
	/// Factory to build instances of <see cref="Model.Datum"/>
	/// </summary>
	public class DatumFactory : MPRV.Common.Factory<DatumFactory>
	{
		#region Public Methods

		/// <summary>
		/// Gets a paged enumerable of <see cref="Model.Datum"/> models
		/// </summary>
		/// <returns>Enumerable of <see cref="Model.Datum"/> models</returns>
		/// <param name="userId">Optional user identifier to filter by</param>
		/// <param name="tags">Optional enumerable of tags to filter by</param>
		public IPagedEnumerable<Datum> GetData(long? userId = null, IEnumerable<string> tags = null)
		{
			var data = new PagedReadableModelEnumerable<Datum>(
				delegate(long page, long perPage, out long totalItems, out long totalPages)
				{
					return Data.Datum.Current
						.GetLatestPaged(userId, tags, page, perPage, out totalItems, out totalPages)
						.Select(r => (ReadableModelPopulator)new DataRowPopulator(r).Populator);
				}
			);

			return data;
		}

		/// <summary>
		/// Gets an instance of <see cref="Model.Datum"/>
		/// </summary>
		/// <returns>Instance of <see cref="Model.Datum"/></returns>
		/// <param name="datumId">Datum identifier</param>
		public Datum GetDatum(long datumId)
		{
			var row = Data.Datum.Current.Get(datumId);
			var populator = new MPRV.Model.DataRowPopulator(row);

			var datum = new Model.Datum();
			datum.Populate(populator.Populator);

			return datum;
		}
		#endregion
	}
}

