using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum SortSpecialField
	{
		[EnumMember(Value = "_score")]
		Score,

		[EnumMember(Value = "_doc")]
		DocumentIndexOrder
	}
}
