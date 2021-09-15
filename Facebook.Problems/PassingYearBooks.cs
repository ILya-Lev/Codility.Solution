using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class PassingYearBooks
    {
        private class Book
        {
            public int Owner { get; }
            public int Location { get; set; }
            public int SignedTimes { get; set; }
            public Book(int owner)
            {
                Owner = owner;
                Location = owner;
            }
        }

        public static int[] FindSignatureCounts(int[] arr)
        {
            var booksToMove = Enumerable.Range(1, arr.Length).Select(owner => new Book(owner)).ToList();//ideally linked list
            var completelySignedBooks = new List<Book>(arr.Length);

            while (booksToMove.Any())
            {
                SignBooks(booksToMove);

                MoveBooks(arr, booksToMove, completelySignedBooks);
            }

            return completelySignedBooks.OrderBy(b => b.Owner).Select(b => b.SignedTimes).ToArray();
        }

        private static void SignBooks(List<Book> booksToMove) => booksToMove.ForEach(b => b.SignedTimes++);

        private static void MoveBooks(int[] arr, List<Book> booksToMove, List<Book> completelySignedBooks)
        {
            for (int c = 0; c < booksToMove.Count; c++)
            {
                var currentBook = booksToMove[c];
                if (arr[currentBook.Location - 1] == currentBook.Owner)
                {
                    booksToMove.RemoveAt(c--);
                    completelySignedBooks.Add(currentBook);
                    continue;
                }

                currentBook.Location = arr[currentBook.Location - 1];
            }
        }
    }
}