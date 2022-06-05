using FluentAssertions;

namespace Luxoft.GeneralCsTest.Tests;

[Trait("Category", "Unit")]
public class TestTaskL2Tests
{
    private readonly TestTaskL2<double> _target = new()
    {
        {"collection1", new[] { 1.0, 2, 4, 5 }},
        {"collection2", new[] { 1.0, 2, 3, 4, 5 }},
        {"collection4", new[] { 4.0, 5 }},
    };

    private readonly TestTaskL2<double> _source = new()
    {
        {"collection1", new[] { 2.0, 3, 5, 6, 2, 10 }},
        {"collection2", new[] { 2.0, 4, 5, 20 }},
        {"collection3", new[] { 4.0, 5 }},
        {"collection4", new[] { 4.0, 5 }},
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

    public record TestRecord(int Use, int Ignore);

    public class TestRecordFirstPropertyComparer : IEqualityComparer<TestRecord>
    {
        public bool Equals(TestRecord? x, TestRecord? y) => x?.Use == y?.Use;

        public int GetHashCode(TestRecord obj) => obj.Use.GetHashCode();
    }

    [Fact]
    public void Merge_HasSameName_DefaultComparer_ContainBothArraysUniqueItems()
    {
        var target = new TestTaskL2<TestRecord>()
        {
            { "a", new TestRecord[] { new(1, 2), new(2, 3) } }
        };
        var source = new TestTaskL2<TestRecord>()
        {
            { "a", new TestRecord[] { new(1, 3), new(3, 4) } }
        };

        target.Merge(source);

        target["a"].Should().BeEquivalentTo(new TestRecord[]
        {
            new(1, 2), new(1, 3), new(2, 3), new(3, 4)
        });
    }

    [Fact]
    public void Merge_HasSameName_FirstPropertyComparer_ContainBothArraysUniqueItems()
    {
        var target = new TestTaskL2<TestRecord>(new TestRecordFirstPropertyComparer())
        {
            { "a", new TestRecord[] { new(1, 2), new(2, 3) } }
        };
        var source = new TestTaskL2<TestRecord>(new TestRecordFirstPropertyComparer())
        {
            { "a", new TestRecord[] { new(1, 3), new(3, 4) } }
        };

        target.Merge(source);

        target["a"].Should().BeEquivalentTo(new TestRecord[]
        {
            new(1, 2), new(2, 3), new(3, 4)
        });
    }

    [Fact]
    public void Cut_HasSameName_DefaultComparer_LeaveUntouched()
    {
        var target = new TestTaskL2<TestRecord>()
        {
            { "a", new TestRecord[] { new(1, 2), new(2, 3) } }
        };
        var source = new TestTaskL2<TestRecord>()
        {
            { "a", new TestRecord[] { new(1, 3), new(3, 4) } }
        };

        target.Cut(source);

        target["a"].Should().BeEquivalentTo(new TestRecord[] { new(1, 2), new(2, 3) });
    }

    [Fact]
    public void Cut_HasSameName_FirstPropertyComparer_RemoveFirstElement()
    {
        var target = new TestTaskL2<TestRecord>(new TestRecordFirstPropertyComparer())
        {
            { "a", new TestRecord[] { new(1, 2), new(2, 3) } }
        };
        var source = new TestTaskL2<TestRecord>(new TestRecordFirstPropertyComparer())
        {
            { "a", new TestRecord[] { new(1, 3), new(3, 4) } }
        };

        target.Cut(source);

        target["a"].Should().BeEquivalentTo(new TestRecord[] { new(2, 3) });
    }
}