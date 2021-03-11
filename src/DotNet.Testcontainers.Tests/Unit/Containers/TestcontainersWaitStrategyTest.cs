namespace DotNet.Testcontainers.Tests.Unit.Containers
{
  using System;
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Containers.WaitStrategies;
  using Testcontainers.Containers.Configurations;
  using Xunit;

  public static class TestcontainersWaitStrategyTest
  {
    public class Finish : IWaitUntil, IWaitWhile
    {
      [Fact]
      public async Task UntilImmediately()
      {
        await WaitStrategy.WaitUntil(() => this.Until(null, DockerClientAuthConfig.Anonymous(), string.Empty));
      }

      [Fact]
      public async Task WhileImmediately()
      {
        await WaitStrategy.WaitWhile(() => this.While(null, string.Empty));
      }

      public Task<bool> Until(Uri endpoint, DockerClientAuthConfig clientAuthConfig, string id)
      {
        return Task.FromResult(true);
      }

      public Task<bool> While(Uri endpoint, string id)
      {
        return Task.FromResult(false);
      }
    }

    public class Timeout : IWaitUntil, IWaitWhile
    {
      [Fact]
      public async Task UntilAfter1Ms()
      {
        await Assert.ThrowsAsync<TimeoutException>(() => WaitStrategy.WaitUntil(() => this.Until(null, DockerClientAuthConfig.Anonymous(), string.Empty), 1000, 1));
      }

      [Fact]
      public async Task WhileAfter1Ms()
      {
        await Assert.ThrowsAsync<TimeoutException>(() => WaitStrategy.WaitWhile(() => this.While(null, string.Empty), 1000, 1));
      }

      public Task<bool> Until(Uri endpoint, DockerClientAuthConfig clientAuthConfig, string id)
      {
        return Task.FromResult(false);
      }

      public Task<bool> While(Uri endpoint, string id)
      {
        return Task.FromResult(true);
      }
    }

    public class Rethrow : IWaitUntil, IWaitWhile
    {
      [Fact]
      public async Task RethrowUntil()
      {
        await Assert.ThrowsAsync<NotImplementedException>(() => WaitStrategy.WaitUntil(() => this.Until(null, DockerClientAuthConfig.Anonymous(), string.Empty)));
      }

      [Fact]
      public async Task RethrowWhile()
      {
        await Assert.ThrowsAsync<NotImplementedException>(() => WaitStrategy.WaitWhile(() => this.While(null, string.Empty)));
      }

      public Task<bool> Until(Uri endpoint, DockerClientAuthConfig clientAuthConfig, string id)
      {
        throw new NotImplementedException();
      }

      public Task<bool> While(Uri endpoint, string id)
      {
        throw new NotImplementedException();
      }
    }
  }
}
