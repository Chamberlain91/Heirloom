using System;

using Xunit.Abstractions;

namespace Heirloom.Testing.Unit
{
    public abstract class TestingFixture
    {
        public TestingFixture(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public ITestOutputHelper Output { get; }
    }
}
