using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ModelFixtureTests
    {
        private readonly IFixture _fixture;

        public ModelFixtureTests()
        {
            _fixture = new Fixture().Register(new CtorAndPropsFactory());
        }

        private class TestModel
        {
            public int Number { get; set; }

            public string Text { get; set; }
        }

        private class TestParentModel
        {
            public TestModel ChildModel { get; set; }
            
            public string ParentText { get; set; }
        }

        [Theory]
        [MemberData(nameof(TestCases.Ints))]
        public void SetsNumericProperty(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<TestModel>();

            result.Number.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(TestCases.Ints))]
        public void SetsNumericPropertiesOnNestedModels(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Number.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(TestCases.Strings))]
        public void SetsStringProperty(string text)
        {
            _fixture.RegisterType<string>(text);

            var result = _fixture.Create<TestModel>();

            result.Text.Should().Be(text);
        }

        [Theory]
        [MemberData(nameof(TestCases.Strings))]
        public void SetsStringPropertiesOnNestedModels(string text)
        {
            _fixture.RegisterType<string>(text);

            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Text.Should().Be(text);
        }
    }
}
