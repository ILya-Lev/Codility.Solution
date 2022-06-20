using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Luxoft.GeneralCsTest;

public class UticQuestionSamples
{
    private readonly ITestOutputHelper _output;

    public UticQuestionSamples(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void SerializeHashTable_Straightforward_Exception()
    {
        var table = new HashSet<int>(new[] { 1, 2, 3, });
        _output.WriteLine(table.ToString());
        var raw = JsonConvert.SerializeObject(table);
        _output.WriteLine(raw);
    }

    [Fact]
    public void Throw_FromCatch_FinallyIsExecuted()
    {
        try
        {
            _output.WriteLine("from try before");
            throw new Exception("test");
            _output.WriteLine("from try after");
        }
        catch (Exception exc)
        {
            _output.WriteLine("from catch before");
            throw;
            _output.WriteLine("from catch after");
        }
        finally
        {
            _output.WriteLine("from finally");
        }
    }

    [Fact]
    public void StackOverflowException_IsNotCaught()
    {
        try
        {
            throw new StackOverflowException("test");
            _output.WriteLine("from try after");
        }
        catch (Exception exc)
        {
            _output.WriteLine("from catch before");
            throw;
            _output.WriteLine("from catch after");
        }
        finally
        {
            _output.WriteLine("from finally");
        }
    }

    [Fact]
    public async Task Await_InsideLock_Forbidden()
    {
        lock (this)
        {
            _ = await File.ReadAllTextAsync("a");
        }
    }
}