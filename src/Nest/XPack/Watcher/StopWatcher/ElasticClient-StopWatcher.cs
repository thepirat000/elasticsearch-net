﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stops the Watcher/Alerting service, if the service is running
		/// </summary>
		StopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null);

		/// <inheritdoc />
		StopWatcherResponse StopWatcher(IStopWatcherRequest request);

		/// <inheritdoc />
		Task<StopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<StopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) =>
			StopWatcher(selector.InvokeOrDefault(new StopWatcherDescriptor()));

		/// <inheritdoc />
		public StopWatcherResponse StopWatcher(IStopWatcherRequest request) =>
			DoRequest<IStopWatcherRequest, StopWatcherResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StopWatcherResponse> StopWatcherAsync(
			Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		) => StopWatcherAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()), ct);

		/// <inheritdoc />
		public Task<StopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStopWatcherRequest, StopWatcherResponse>
				(request, request.RequestParameters, ct);
	}
}
