using System.Collections.Generic;
using System.Linq;

namespace LuxoftPolandContest
{
    public class TransactionedStack
    {
        private readonly List<int> _values = new List<int>();
        private readonly List<StackTransaction> _transactions = new List<StackTransaction>();
        public IReadOnlyList<int> Values => _values;
        public TransactionedStack()
        {
        }

        public void push(int value)
        {
            _values.Add(value);
            _transactions.LastOrDefault()?.Add(value);
        }

        public int top()
        {
            return _values.LastOrDefault();
        }

        public void pop()
        {
            if (_values.Count != 0)
            {
                _transactions.LastOrDefault()?.Remove(top());
                _values.RemoveAt(_values.Count - 1);
            }
        }

        public void begin()
        {
            _transactions.Add(new StackTransaction());
        }

        public bool rollback()
        {
            var lastTransaction = _transactions.LastOrDefault();
            if (lastTransaction == null)
                return false;

            lastTransaction.Rollback(_values);
            _transactions.RemoveAt(_transactions.Count - 1);
            return true;
        }

        public bool commit()
        {
            var nestedTransaction = _transactions.LastOrDefault();
            if (nestedTransaction == null)
                return false;

            _transactions.RemoveAt(_transactions.Count - 1);

            var parentTransaction = _transactions.LastOrDefault();
            parentTransaction?.CopyHistoryFrom(nestedTransaction);

            return true;
        }

        private class StackTransaction
        {
            private readonly List<Operation> _history = new List<Operation>();

            public void Add(int value) => SaveOperation(new Operation(value, true));
            
            public void Remove(int value) => SaveOperation(new Operation(value, false));
            
            public void Rollback(List<int> values)
            {
                for (int i = _history.Count - 1; i >= 0; --i)
                {
                    if (_history[i].Inserted)
                        values.RemoveAt(values.Count - 1);
                    else
                        values.Add(_history[i].Value);
                }
            }

            public void CopyHistoryFrom(StackTransaction nestedTransaction)
            {
                _history.AddRange(nestedTransaction._history);
            }

            private void SaveOperation(Operation currentOperation)
            {
                var previousOperation = _history.LastOrDefault();

                if (currentOperation.IsOppositeTo(previousOperation))
                    _history.RemoveAt(_history.Count - 1);
                else
                    _history.Add(currentOperation);
            }

            private class Operation
            {
                public int Value { get; }
                public bool Inserted { get; }

                public Operation(int value, bool inserted)
                {
                    Value = value;
                    Inserted = inserted;
                }

                public bool IsOppositeTo(Operation other)
                {
                    return other != null && other.Value == Value && other.Inserted != Inserted;
                }
            }
        }
    }
}
