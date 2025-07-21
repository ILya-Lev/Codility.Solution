using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace LuxoftPolandContest.Tests;

public class TransactionedStackTests : IClassFixture<TestOutputHelper>
{
    private readonly TestOutputHelper _outputHelper;

    public TransactionedStackTests(TestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void NoTransactions_SimpleStack()
    {
        var stack = new TransactionedStack();

        stack.push(1);
        stack.top().Should().Be(1);

        stack.push(2);
        stack.top().Should().Be(2);

        stack.pop();
        stack.top().Should().Be(1);

        stack.pop();
        stack.top().Should().Be(0);

        stack.pop();
        stack.top().Should().Be(0);
    }

    [Fact]
    public void OneTransaction_Commit_Preserve()
    {
        var stack = new TransactionedStack();
        stack.push(1);
        stack.push(2);
        stack.push(3);

        stack.begin();

        stack.pop();
        stack.push(4);
        stack.push(5);
        stack.push(6);
        stack.pop();
        stack.pop();
        stack.commit();

        stack.Values.Should().Equal(new[] { 1, 2, 4 });
    }

    [Fact]
    public void OneTransaction_RollBack_LeaveInAStateAsWasBeforeTransaction()
    {
        var stack = new TransactionedStack();
        stack.push(1);
        stack.push(2);
        stack.push(3);

        stack.begin();

        stack.pop();
        stack.push(4);
        stack.push(5);
        stack.push(6);
        stack.pop();
        stack.pop();
        stack.rollback();

        stack.Values.Should().Equal(new[] { 1, 2, 3 });
    }

    [Fact]
    public void NestedTransactions_RollBackInner_DoesNotAffectOuter()
    {
        var stack = new TransactionedStack();
        stack.push(1);
        stack.push(2);
        stack.push(3);

        stack.begin();
        stack.pop();
        stack.push(4);
        stack.push(5);
        stack.push(6);
        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6 });

        stack.begin();
        stack.pop();
        stack.pop();
        stack.Values.Should().Equal(new[] { 1, 2, 4 });
        stack.rollback();
        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6 });

        stack.push(7);
        stack.push(8);

        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6, 7, 8 });

        stack.rollback();
        stack.Values.Should().Equal(new[] { 1, 2, 3 });
    }


    [Fact]
    public void TwoTransactions_AreIndependent()
    {
        var stack = new TransactionedStack();
        stack.push(1);
        stack.push(2);
        stack.push(3);

        stack.begin();
        stack.pop();
        stack.push(4);
        stack.push(5);
        stack.push(6);
        stack.commit();
        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6 });

        stack.begin();
        stack.pop();
        stack.pop();
        stack.Values.Should().Equal(new[] { 1, 2, 4 });
        stack.rollback();
        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6 });

        stack.push(7);
        stack.push(8);

        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6, 7, 8 });

        stack.rollback();
        stack.Values.Should().Equal(new[] { 1, 2, 4, 5, 6, 7, 8 });
    }

    [Fact]
    public void Sample1()
    {
        var stack = new TransactionedStack();
        stack.push(5);
        stack.push(2);
        stack.pop();
        stack.Values.Should().Equal(new[] { 5 });

        var stack1 = new TransactionedStack();
        stack1.top().Should().Be(0);
        stack1.pop();

    }

    [Fact]
    public void Sample2()
    {
        var sol = new TransactionedStack();
        sol.push(4);

        sol.begin();                     // start transaction 1
        sol.push(7);                     // stack: [4,7]

        sol.begin();                     // start transaction 2
        sol.push(2);                     // stack: [4,7,2]
        sol.rollback().Should().BeTrue();
        sol.top().Should().Be(7);

        sol.begin();                     // start transaction 3
        sol.push(10);                    // stack: [4,7,10]
        sol.commit().Should().BeTrue();    // transaction 3 is committed
        sol.top().Should().Be(10);

        sol.rollback().Should().BeTrue();
        sol.top().Should().Be(4);
        sol.commit().Should().BeFalse();
    }

    // this test shows using List instead of LinkedList for _values and _transactions
    // gives at least 20 times performance advantage
    [Fact]
    public void PushPop_Performance_Fast()
    {
        var stack = new TransactionedStack();
        var inserted = 0;
        var removed = 0;
        for (int i = 0; i < 10_000_000; ++i)
        {
            if (i % 3 != 0 || i % 5 != 0 || i % 7 != 0)
            {
                stack.push(i);
                inserted++;
            }
            else
            {
                stack.pop();
                removed++;
            }

            if (i % 800 == 0) stack.begin();
            else if (i % 3701 == 0) stack.commit();
            else if (i % 28177 == 0) stack.rollback();
        }

        _outputHelper.WriteLine($"inserted {inserted} and removed {removed}; count {stack.Values.Count}");

        stack.Values.Should().NotBeEmpty();
    }
}