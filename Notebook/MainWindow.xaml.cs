using System.Windows;
using System.Windows.Controls;

namespace Notebook
{
    class Contact
    {
        public string FirstName
        {
            get;
            private set;
        }
        public string LastName
        {
            get;
            private set;
        }
        public string Phone
        {
            get;
            private set;
        }
        public string Description
        {
            get;
            private set;
        }
        public string ToolTipInfo
        {
            get
            {
                return string.Format("{0} {1}\n---< Телефон >---\n{2}\n---< Описание >---\n{3}", FirstName, LastName, Phone, Description);
            }
        }

        public Contact(string firstName, string lastName, string phone, string description)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;      
            Description = description;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddOrEdit(bool isAdd)
        {
            Contact contact = new Contact(TextBox_FirstName.Text, TextBox_LastName.Text, TextBox_Phone.Text, TextBox_Description.Text);

            ListBoxItem listBoxItem = new ListBoxItem
            {
                Tag = contact,
                Content = contact,
                ToolTip = contact.ToolTipInfo
            };

            ToolTipService.SetShowDuration(listBoxItem, 30000);

            if (isAdd)
                ListBox_Notebook.Items.Add(listBoxItem);
            else
            {
                if (ListBox_Notebook.SelectedIndex != -1)
                    ListBox_Notebook.Items[ListBox_Notebook.SelectedIndex] = listBoxItem;
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddOrEdit(true);
        }
        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            AddOrEdit(false);
        }
        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Notebook.SelectedItem != null)
                ListBox_Notebook.Items.Remove(ListBox_Notebook.SelectedItem);
        }
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            ListBox_Notebook.Items.Clear();
        }

        private void ListBox_Notebook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Notebook.SelectedIndex != -1)
            {
                ListBoxItem listBoxItem = (ListBoxItem)ListBox_Notebook.SelectedItem;
                Contact contact = (Contact)listBoxItem.Tag;

                TextBox_FirstName.Text = contact.FirstName;
                TextBox_LastName.Text = contact.LastName;
                TextBox_Phone.Text = contact.Phone;
                TextBox_Description.Text = contact.Description;
            }
            else
            {
                TextBox_FirstName.Text = null;
                TextBox_LastName.Text = null;
                TextBox_Phone.Text = null;
                TextBox_Description.Text = null;
            }
        }
        private void ComboBox_Styles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary style = new ResourceDictionary();

            switch (ComboBox_Styles.SelectedIndex)
            {
                case 1:
                    {
                        style = (ResourceDictionary)Application.Current.Resources["Style_Quad"];
                    }
                    break;
                case 2:
                    {
                        style = (ResourceDictionary)Application.Current.Resources["Style_Ellipse"];
                    }
                    break;
                default:
                    {
                        style = (ResourceDictionary)Application.Current.Resources["Style_Default"];
                    }
                    break;
            }

            if (Resources.MergedDictionaries.Count == 0)
                Resources.MergedDictionaries.Add(style);
            else
                Resources.MergedDictionaries[0] = style;
        }
    }
}
