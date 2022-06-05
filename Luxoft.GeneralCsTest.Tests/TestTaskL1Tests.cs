using FluentAssertions;

namespace Luxoft.GeneralCsTest.Tests;

[Trait("Category", "Unit")]
public class TestTaskL1Tests
{
    private readonly TestTaskL1 _target = new()
    {
        {"collection1", new[] { 1, 2, 4, 5 }},
        {"collection2", new[] { 1, 2, 3, 4, 5 }},
        {"collection4", new[] { 4, 5 }},
    };

    private readonly TestTaskL1 _source = new()
    {
        {"collection1", new[] { 2, 3, 5, 6, 2, 10 }},
        {"collection2", new[] { 2, 4, 5, 20 }},
        {"collection3", new[] { 4, 5 }},
        {"collection4", new[] { 4, 5 }},
    };

    [Fact]
    public void Merge_HasNewNamesAndValues_AddNewArraysAndMergeExisting()
    {
        _target.KnowsName("collection3").Should().BeFalse();

        _target.Merge(_source);

        _target["collection1"].Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 10 });
        _target["collection2"].Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 20 });
        _target["collection3"].Should().BeEquivalentTo(new[] { 4, 5 });
        _target["collection4"].Should().BeEquivalentTo(new[] { 4, 5 });
    }

    [Fact]
    public void Cut_HasNewNamesAndValues_Remove4LeaveSomeIn1And2()
    {
        _target.KnowsName("collection3").Should().BeFalse();
        _target.KnowsName("collection4").Should().BeTrue();

        _target.Cut(_source);

        _target["collection1"].Should().BeEquivalentTo(new[] { 1, 4 });
        _target["collection2"].Should().BeEquivalentTo(new[] { 1, 3 });
        _target.KnowsName("collection3").Should().BeFalse();
        _target.KnowsName("collection4").Should().BeFalse();
    }
}