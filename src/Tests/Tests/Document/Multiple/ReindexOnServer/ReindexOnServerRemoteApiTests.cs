﻿using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerRemoteApiTests
		: ApiTestBase<IntrusiveOperationCluster, ReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor, ReindexOnServerRequest>
	{
		private readonly Uri _host = new Uri("http://myremoteserver.example:9200");

		public ReindexOnServerRemoteApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
				},
				source = new
				{
					remote = new
					{
						host = "http://myremoteserver.example:9200",
						username = "user",
						password = "changeme"
					},
					index = CallIsolatedValue,
					size = 100
				}
			};

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Remote(r => r.Host(_host).Username("user").Password("changeme"))
				.Index(CallIsolatedValue)
				.Size(100)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest()
		{
			Source = new ReindexSource
			{
				Remote = new RemoteSource
				{
					Host = _host,
					Username = "user",
					Password = "changeme"
				},
				Index = CallIsolatedValue,
				Size = 100
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_reindex";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexOnServer(f),
			(client, f) => client.ReindexOnServerAsync(f),
			(client, r) => client.ReindexOnServer(r),
			(client, r) => client.ReindexOnServerAsync(r)
		);
	}
}
