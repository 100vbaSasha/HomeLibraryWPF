using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using HomeLibrary.Model;
using HomeLibrary.View;
using System.Collections;
using OxyPlot;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.Generic;
using System.Windows.Controls;

class Keyvalue : INotifyPropertyChanged
{ 
    private string _Key;
    public string Key { 
        get 
        { 
            return _Key; 
        } 
        set 
        {
            _Key = value;
            OnPropertyChanged("Key");
        } 
    }

    private int _Value;
    public int Value
    {
        get
        {
            return _Value;
        }
        set
        {
            _Value = value;
            OnPropertyChanged("Value");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}


class TestModel : INotifyPropertyChanged
{
    private List<Keyvalue> _DataList;
    public List<Keyvalue> DataList { 
        get 
        { 
            return _DataList; 
        } 
        set 
        {
            _DataList = value;
            OnPropertyChanged("DataList");
        } 
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}


namespace HomeLibrary.ViewModel
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addReadBookCommand;
        RelayCommand editReadBookCommand;
        RelayCommand deleteReadBookCommand;
        RelayCommand addCurrentBookCommand;
        RelayCommand editCurrentBookCommand;
        RelayCommand deleteCurrentBookCommand;
        RelayCommand addPlannedBookCommand;
        RelayCommand editPlannedBookCommand;
        RelayCommand deletePlannedBookCommand;
        RelayCommand addPagesPerDayCommand;
        RelayCommand editPagesPerDayCommand;
        RelayCommand deletePagesPerDayCommand;
        RelayCommand updateSchedule;
        IEnumerable<ReadBook> readBooks;
        IEnumerable<CurrentBook> currentBooks;
        IEnumerable<PlannedBook> plannedBooks;
        IEnumerable<PagesPerDay> pagesPerDays;
        private TestModel testModel = new TestModel();
        List<Keyvalue> tempList = new List<Keyvalue>();
        GroupBox shedule;
        public IEnumerable<ReadBook> ReadBooks
        {
            get { return readBooks; }
            set
            {
                readBooks = value;
                OnPropertyChanged("ReadBooks");
            }
        }
        public IEnumerable<CurrentBook> CurrentBooks
        {
            get { return currentBooks; }
            set
            {
                currentBooks = value;
                OnPropertyChanged("CurrentBooks");
            }
        }
        public IEnumerable<PlannedBook> PlannedBooks
        {
            get { return plannedBooks; }
            set
            {
                plannedBooks = value;
                OnPropertyChanged("PlannedBooks");
            }
        }
        public IEnumerable<PagesPerDay> PagesPerDays
        {
            get { return pagesPerDays; }
            set
            {
                pagesPerDays = value;
                OnPropertyChanged("PagesPerDays");
            }
        }
        public ApplicationViewModel(GroupBox GroupBoxDynamicChart)
        {
            db = new ApplicationContext();
            db.ReadBooks.Load();
            db.CurrentBooks.Load();
            db.PlannedBooks.Load();
            db.PagesPerDays.Load();
            ReadBooks = db.ReadBooks.Local.ToBindingList();
            CurrentBooks = db.CurrentBooks.Local.ToBindingList();
            PlannedBooks = db.PlannedBooks.Local.ToBindingList();
            PagesPerDays = db.PagesPerDays.Local.ToBindingList();
            foreach(PagesPerDay ppd in pagesPerDays)
            {
                tempList.Add(new Keyvalue() { Key = ppd.Date, Value = ppd.Pages });
            }

            testModel.DataList = tempList;

            Chart dynamicChart = new Chart();
            LineSeries lineseries = new LineSeries();
            lineseries.ItemsSource = tempList;
            lineseries.DependentValuePath = "Value";
            lineseries.IndependentValuePath = "Key";
            dynamicChart.Series.Add(lineseries);
            GroupBoxDynamicChart.Content = dynamicChart;
            shedule = GroupBoxDynamicChart;
        }
        public RelayCommand UpdateScheduleCommand
        {
            get
            {
                return updateSchedule ?? (updateSchedule = new RelayCommand((o) =>
                {
                    List<Keyvalue> list = new List<Keyvalue>();
                    foreach (PagesPerDay ppd in pagesPerDays)
                    {
                        list.Add(new Keyvalue() { Key = ppd.Date, Value = ppd.Pages });
                    }
                    tempList = list;
                    Chart dynamicChart = new Chart();
                    LineSeries lineseries = new LineSeries();
                    lineseries.ItemsSource = tempList;
                    lineseries.DependentValuePath = "Value";
                    lineseries.IndependentValuePath = "Key";
                    dynamicChart.Series.Add(lineseries);
                    shedule.Content = dynamicChart;
                }));
            }
        }
        public TestModel TestModel
        {
            get 
            { 
                return testModel; 
            }
            set 
            {
                testModel = value;
                OnPropertyChanged("TestModel");
            }
        }


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //Команда додавання в БД readBook
        public RelayCommand AddReadBookCommand
        {
            get
            {
                return addReadBookCommand ??
                  (addReadBookCommand = new RelayCommand((o) =>
                  {
                      ReadBookWindow readBookWindow = new ReadBookWindow(new ReadBook());
                      if (readBookWindow.ShowDialog() == true)
                      {
                          ReadBook readBook = readBookWindow.ReadBook;
                          db.ReadBooks.Add(readBook);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редагування readBook
        public RelayCommand EditReadBookCommand
        {
            get
            {
                return editReadBookCommand ??
                  (editReadBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      ReadBook readBook = selectedItem as ReadBook;

                      ReadBook rb = new ReadBook()
                      {
                          Id = readBook.Id,
                          Title = readBook.Title,
                          Author = readBook.Author,
                          Description = readBook.Description,
                          Rating = readBook.Rating,
                          NumberOfPages = readBook.NumberOfPages
                      };
                      ReadBookWindow readBookWindow = new ReadBookWindow(rb);


                      if (readBookWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          readBook = db.ReadBooks.Find(readBookWindow.ReadBook.Id);
                          if (readBook != null)
                          {
                              readBook.Author = readBookWindow.ReadBook.Author;
                              readBook.Title = readBookWindow.ReadBook.Title;
                              readBook.Description = readBookWindow.ReadBook.Description;
                              readBook.Rating = readBookWindow.ReadBook.Rating;
                              readBook.NumberOfPages = readBookWindow.ReadBook.NumberOfPages;
                              db.Entry(readBook).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления readBook
        public RelayCommand DeleteReadBookCommand
        {
            get
            {
                return deleteReadBookCommand ??
                  (deleteReadBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      ReadBook readBook = selectedItem as ReadBook;
                      db.ReadBooks.Remove(readBook);
                      db.SaveChanges();
                  }));
            }
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// //Команда додавання в БД currentBook
        public RelayCommand AddCurrentBookCommand
        {
            get
            {
                return addCurrentBookCommand ??
                  (addCurrentBookCommand = new RelayCommand((o) =>
                  {
                      CurrentBookWindow currentBookWindow = new CurrentBookWindow(new CurrentBook());
                      if (currentBookWindow.ShowDialog() == true)
                      {
                          CurrentBook currentBook = currentBookWindow.CurrentBook;
                          db.CurrentBooks.Add(currentBook);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования currentBook
        public RelayCommand EditCurrentBookCommand
        {
            get
            {
                return editCurrentBookCommand ??
                  (editCurrentBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      CurrentBook currentBook = selectedItem as CurrentBook;

                      CurrentBook cb = new CurrentBook()
                      {
                          Id = currentBook.Id,
                          Title = currentBook.Title,
                          Author = currentBook.Author,
                          NumberOfPages = currentBook.NumberOfPages,
                          NumberOfReadPages = currentBook.NumberOfReadPages
                      };
                      CurrentBookWindow currentBookWindow = new CurrentBookWindow(cb);


                      if (currentBookWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          currentBook = db.CurrentBooks.Find(currentBookWindow.CurrentBook.Id);
                          if (currentBook != null)
                          {
                              currentBook.Author = currentBookWindow.CurrentBook.Author;
                              currentBook.Title = currentBookWindow.CurrentBook.Title;
                              currentBook.NumberOfPages = currentBookWindow.CurrentBook.NumberOfPages;
                              currentBook.NumberOfReadPages = currentBookWindow.CurrentBook.NumberOfReadPages;
                              db.Entry(currentBook).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления currentBook
        public RelayCommand DeleteCurrentBookCommand
        {
            get
            {
                return deleteCurrentBookCommand ??
                  (deleteCurrentBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      CurrentBook currentBook = selectedItem as CurrentBook;
                      db.CurrentBooks.Remove(currentBook);
                      db.SaveChanges();
                  }));
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //Команда додавання в БД plannedBook
        public RelayCommand AddPlannedBookCommand
        {
            get
            {
                return addPlannedBookCommand ??
                  (addPlannedBookCommand = new RelayCommand((o) =>
                  {
                      PlannedBookWindow plannedBookWindow = new PlannedBookWindow(new PlannedBook());
                      if (plannedBookWindow.ShowDialog() == true)
                      {
                          PlannedBook plannedBook = plannedBookWindow.PlannedBook;
                          db.PlannedBooks.Add(plannedBook);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования plannedBook
        public RelayCommand EditPlannedBookCommand
        {
            get
            {
                return editPlannedBookCommand ??
                  (editPlannedBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      PlannedBook plannedBook = selectedItem as PlannedBook;

                      PlannedBook pb = new PlannedBook()
                      {
                          Id = plannedBook.Id,
                          Title = plannedBook.Title,
                          Author = plannedBook.Author,
                          NumberOfPages = plannedBook.NumberOfPages
                      };
                      PlannedBookWindow plannedBookWindow = new PlannedBookWindow(pb);


                      if (plannedBookWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          plannedBook = db.PlannedBooks.Find(plannedBookWindow.PlannedBook.Id);
                          if (plannedBook != null)
                          {
                              plannedBook.Author = plannedBookWindow.PlannedBook.Author;
                              plannedBook.Title = plannedBookWindow.PlannedBook.Title;
                              plannedBook.NumberOfPages = plannedBookWindow.PlannedBook.NumberOfPages;
                              db.Entry(plannedBook).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда видалення plannedBook
        public RelayCommand DeletePlannedBookCommand
        {
            get
            {
                return deletePlannedBookCommand ??
                  (deletePlannedBookCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      PlannedBook plannedBook = selectedItem as PlannedBook;
                      db.PlannedBooks.Remove(plannedBook);
                      db.SaveChanges();
                  }));
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //Команда додавання в БД pagesPerDay
        public RelayCommand AddPagesPerDayCommand
        {
            get
            {       
                return addPagesPerDayCommand ??
                  (addPagesPerDayCommand = new RelayCommand((o) =>
                  {
                      PagesPerDayWindow pagesPerDayWindow = new PagesPerDayWindow(new PagesPerDay());
                      if (pagesPerDayWindow.ShowDialog() == true)
                      {
                          PagesPerDay pagesPerDay = pagesPerDayWindow.PagesPerDay;
                          db.PagesPerDays.Add(pagesPerDay);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редагування pagesPerDay
        public RelayCommand EditPagesPerDayCommand
        {
            get
            {
                return editPagesPerDayCommand ??
                  (editPagesPerDayCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      PagesPerDay pagesPerDay = selectedItem as PagesPerDay;

                      PagesPerDay ppd = new PagesPerDay()
                      {
                          Id = pagesPerDay.Id,
                          Date = pagesPerDay.Date,
                          Pages = pagesPerDay.Pages
                      };
                      PagesPerDayWindow pagesPerDayWindow = new PagesPerDayWindow(ppd);


                      if (pagesPerDayWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          pagesPerDay = db.PagesPerDays.Find(pagesPerDayWindow.PagesPerDay.Id);
                          if (pagesPerDay != null)
                          {
                              pagesPerDay.Date = pagesPerDayWindow.PagesPerDay.Date;
                              pagesPerDay.Pages = pagesPerDayWindow.PagesPerDay.Pages;
                              db.Entry(pagesPerDay).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда видалення PagesPerDay
        public RelayCommand DeletePagesPerDayCommand
        {
            get
            {
                return deletePagesPerDayCommand ??
                  (deletePagesPerDayCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // отримуємо виділений об'єкт
                      PagesPerDay pagesPerDay = selectedItem as PagesPerDay;
                      db.PagesPerDays.Remove(pagesPerDay);
                      db.SaveChanges();
                  }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
    }
}
