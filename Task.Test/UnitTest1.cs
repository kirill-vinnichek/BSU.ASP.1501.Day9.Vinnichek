using System;
using NUnit.Framework;
using System.Collections.Generic;
using Task1.Book;
namespace Task.Test
{
    public struct Point2D {
        public int x;
        public int y;
        public Point2D(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    public class IntComparer : IComparer<int> {
        public int Compare(int a, int b)
        {
            return Math.Abs(a).ToString().Length - Math.Abs(b).ToString().Length;
        }

    }
    public class StringComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            return a.Length - b.Length;
        }

    }

    public class BookComparer : IComparer<Book>
    {
        public int Compare(Book a, Book b)
        {
            return a.Series - b.Series;
        }

    }
    [TestFixture]
    public class UnitTest1
    {
        private IEnumerable<TestCaseData> IntTestData
        {
            get
            {
                yield return new TestCaseData(new[] {15,23,17,-23,123,1,103 }, null, -23,123);
                yield return new TestCaseData(new[] { 15, 23, 17, -23, 123, 1, 103 }, new IntComparer(), 1, 103);
            }
        }
        [Test,TestCaseSource("IntTestData")]
        public void IntTest(int[] array,IComparer<int> comparer,int min,int max)
        {
            var tree = new BinaryTree.BinaryTree<int>(comparer);
            foreach (var i in array)
            {
                tree.Add(i);
            }
            Assert.AreEqual(tree.Max, max);
            Assert.AreEqual(tree.Min, min);
        }

        private IEnumerable<TestCaseData> StringTestData
        {
            get
            {
                yield return new TestCaseData(new[] {"p","AB","qwesas","Oisks" ,"ZZZ"}, null, "AB", "ZZZ");
                yield return new TestCaseData(new[] { "p", "AB", "qwesaswe", "Oisks", "ZZZ" }, new StringComparer(), "p", "qwesaswe");
            }
        }
        [Test, TestCaseSource("StringTestData")]
        public void StringTest(string[] array, IComparer<string> comparer, string min, string max)
        {
            var tree = new BinaryTree.BinaryTree<string>(comparer);
            foreach (var i in array)
            {
                tree.Add(i);
            }
            Assert.AreEqual(tree.Max, max);
            Assert.AreEqual(tree.Min, min);
        }

        private IEnumerable<TestCaseData> BookTestData
        {
            get
            {
                yield return new TestCaseData(new[]  {new Book("Albahari",@"C# in Nutshell",100,"ru"),                         
                             new Book("Esposito",@"ASP .NET MVC 4",1,"ru"),
                                 new Book("Albahari",@"C# in Nutshell",3,"en"),
                             new Book("Фримен",@"Паттерны проектирования",15,"ru")
                            }, 
                            null, new Book("Albahari", @"C# in Nutshell", 3, "en"), new Book("Фримен", @"Паттерны проектирования", 15, "ru"));

                yield return new TestCaseData(new[]{new Book("Albahari",@"C# in Nutshell",100,"ru"),                         
                             new Book("Esposito",@"ASP .NET MVC 4",1,"ru"),
                                 new Book("Albahari",@"C# in Nutshell",3,"en"),
                             new Book("Фримен",@"Паттерны проектирования",15,"ru")
                            }, new BookComparer(), new Book("Esposito", @"ASP .NET MVC 4", 1, "ru"), new Book("Albahari",@"C# in Nutshell",100,"ru"));
            }
        }
        [Test, TestCaseSource("BookTestData")]
        public void BookTest(Book[] array, IComparer<Book> comparer, Book min, Book max)
        {
            var tree = new BinaryTree.BinaryTree<Book>(comparer);
            foreach (var i in array)
            {
                tree.Add(i);
            }
            Assert.AreEqual(tree.Max, max);
            Assert.AreEqual(tree.Min, min);
        }

        private IEnumerable<TestCaseData> PointTestData
        {
            get
            {
                yield return new TestCaseData(new[] { new Point2D(1, 2), new Point2D(5, 6), new Point2D(-2, -12), new Point2D(8, 0) }, null, new Point2D(1, 2), new Point2D(-2, -12));
            }
        }
        [Test, TestCaseSource("PointTestData")]
        public void PointTest(Point2D[] array, IComparer<Point2D> comparer, Point2D min, Point2D max)
        {
            var tree = new BinaryTree.BinaryTree<Point2D>(comparer);
            foreach (var i in array)
            {
                tree.Add(i);
            }
            Assert.AreEqual(tree.Max, max);
            Assert.AreEqual(tree.Min, min);
        }
    }
}
