using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ForTypeTests
    {
        private readonly IFixture _fixture = new Fixture()
                .UseConstructorAndSetProperties()
                .UseInstance<string>("OuterScopedString")
                .ForType<TestModel>(f => f
                    .UseInstance<string>("InnerScopedString"));

        private class TestModel
        {
            public string Text { get; set; }
        }

        private class TestParentModel
        {
            public TestModel ChildModel { get; set; }
            
            public string ParentText { get; set; }
        }

        [Fact]
        public void ParentModelHasOuterScopedString()
        {
            var result = _fixture.Create<TestParentModel>();

            result.ParentText.Should().Be("OuterScopedString");
        }

        [Fact]
        public void InnerScopeModelHasInnerScopedString()
        {
            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Text.Should().Be("InnerScopedString");
        }

        [Fact]
        public void ChildModelHasInnerScopedString()
        {
            var result = _fixture.Create<TestModel>();

            result.Text.Should().Be("InnerScopedString");
        }
    }
}
