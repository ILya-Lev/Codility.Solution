using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using UjvalsProposal.Models;

namespace UjvalsProposal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //public partial class MainWindow : Window
    //{
    //    private readonly Course course;

    //    public MainWindow()
    //    {
    //        InitializeComponent();

    //        var bw = new BackgroundWorker();//older approach
    //        bw.DoWork += bw_DoWork;
    //        bw.RunWorkerAsync();

    //        course = new Course();
    //        course.Name = "The main course";
    //        course.Students = new List<Student>();
    //        DataContext = course;
    //    }

    //    private void bw_DoWork(object? sender, DoWorkEventArgs e)
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            var student = new Student()
    //            {
    //                Name = $"George the {i} th",
    //                Age = 20 + i
    //            };
    //            Dispatcher.BeginInvoke(() => course.Students.Add(student), DispatcherPriority.DataBind);
    //            //course.Students.Add(student);

    //            Dispatcher.Invoke(() => tbCount.Text = $"Total Students: {course.Students.Count}",
    //                DispatcherPriority.DataBind);

    //            Thread.Sleep(TimeSpan.FromSeconds(1));
    //        }
    //    }

    //}

    public partial class MainWindow : Window
    {
        public Course Course { get; }

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += DoWork;

            Course = new Course
            {
                Name = "The main course",
                Students = new List<Student>()
            };

            DataContext = Course;
        }

        private async void DoWork(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var student = new Student()
                {
                    Name = $"George the {i} th",
                    Age = 20 + i
                };

                Course.Students.Add(student);

                tbCount.Text = $"Total Students: {Course.Students.Count}";
                
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
