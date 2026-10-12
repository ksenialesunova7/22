using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace _22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        Book selectBook = null;

        public MainWindow()
        {
            InitializeComponent();

            books.Add(new Book { Title = "Белые ночи", Author = "Ф. Достоевский", Taken = 2, Count = 8, Cover = @"/Images/belie.jpg" });
            books.Add(new Book { Title = "Скотный двор", Author = "Д. Оруэль", Taken = 17, Count = 170, Cover = @"/Images/dvor.jpg" });
            books.Add(new Book { Title = "Нос", Author = "Н. Гоголь", Taken = 7, Count = 3, Cover = @"/Images/nos.jpg" });
            books.Add(new Book { Title = "Превращение", Author = "Ф. Кафка", Taken = 2, Count = 11, Cover = @"/Images/prevrashenie.jpg" });
            books.Add(new Book { Title = "Сон смешного человека", Author = "Ф. Достоевский", Taken = 0, Count = 4, Cover = @"/Images/son.jpg" });

            BookPanel.ItemsSource = books;
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = Brushes.White;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = Brushes.Beige;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as StackPanel).DataContext is Book book)
            {

                selectBook = book;

                txtName.Text = book.Title;
                txtAuthor.Text = book.Author;
                txtCover.Text = book.Cover;
                txtCount.Text = book.Count.ToString();

                UpdateStatus();
            }
        }


        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectBook == null) return;
            selectBook.Title = txtName.Text;
        }

        private void TxtAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectBook == null) return;
            selectBook.Author = txtAuthor.Text;
        }

        private void TxtCover_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectBook == null) return;
            selectBook.Cover = txtCover.Text;
        }

        private void TxtCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectBook == null) return;
            if (int.TryParse(txtCount.Text, out int count) && count >= 0)
            {
                selectBook.Count = count;
                UpdateStatus();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            books.Add(new Book { Title = "Новая книга", Author = "Неизвестен", Taken = 0, Count = 1, Cover = @"/Images/kirp.jpg" });
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (selectBook == null)
            {
                MessageBox.Show("Сначала выберите книгу в списке.",
                                "Удаление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Вы точно хотите удалить  «{selectBook.Title}»?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                books.Remove(selectBook);
                selectBook = null;
                ClearFields();
            }
        }

        private void Take_Click(object sender, RoutedEventArgs e)
        {
            if (selectBook == null) return;
            if (selectBook.Count <= 0) return;

            selectBook.Count--;
            selectBook.Taken++;

            txtCount.Text = selectBook.Count.ToString();

            UpdateStatus();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (selectBook == null) return;
            if (selectBook.Taken <= 0) return;

            selectBook.Taken--;
            selectBook.Count++;

            txtCount.Text = selectBook.Count.ToString();

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (selectBook != null)
                txtStatus.Text = selectBook.Status;
        }

        private void ClearFields()
        {
            selectBook = null;
            txtName.Text = "";
            txtAuthor.Text = "";
            txtCover.Text = "";
            txtCount.Text = "";
            txtStatus.Text = "";
        }
    }

    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Taken { get; set; }
        public int Count { get; set; }
        public string Cover { get; set; }
        public string Status { get; internal set; }
    }
}