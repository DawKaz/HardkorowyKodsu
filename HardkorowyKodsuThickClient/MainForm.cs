using HardkorowyKodsuThickClient.Models;
using HardkorowyKodsuThickClient.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HardkorowyKodsuThickClient
{
    public partial class MainForm : Form
    {
        private readonly DatabaseViewModel _viewModel;

        public MainForm()
        {
            InitializeComponent();
            _viewModel = new DatabaseViewModel();

            DataContext = _viewModel;
            btn_Load.Click += (s, e) => _viewModel.LoadTablesAndViewsCommand.Execute(null);
            listBox1.SelectedIndexChanged += (s, e) =>
            {
                var selectedTable = listBox1.SelectedItem as TableOrView;
                _viewModel.LoadTableStructureCommand.Execute(selectedTable?.Name);
            };

            _viewModel.TablesAndViews.CollectionChanged += (s, e) => UpdateListBox(listBox1, _viewModel.TablesAndViews);
            _viewModel.SelectedTableStructure.CollectionChanged += (s, e) => UpdateListBox(listBox2, _viewModel.SelectedTableStructure);
        }

        private void UpdateListBox<T>(ListBox listBox, ObservableCollection<T> collection)
        {
            listBox.DataSource = null;
            listBox.DataSource = collection.ToList();
        }
    }
}
