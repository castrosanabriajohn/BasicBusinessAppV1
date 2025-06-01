using Xunit;

namespace BasicBusinessApp.Application.Tests;

public class SanityTests
{
    [Fact]
    public void AlwaysPasses()
    {
        Assert.True(true); // Siempre pasa
    }
}