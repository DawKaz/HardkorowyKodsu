
using HardkorowyKodsuThickClient.Models;
using HardkorowyKodsuThickClient.Utilis;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace HardkorowyKodsuThickClient.ViewModels
{
    public class DatabaseViewModel
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<TableOrView> TablesAndViews { get; set; } = new();
        public ObservableCollection<TableOrView> SelectedTableStructure { get; set; } = new();

        public ICommand LoadTablesAndViewsCommand { get; }
        public ICommand LoadTableStructureCommand { get; }

        public DatabaseViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("http://localhost:5000/api/")
            };

            LoadTablesAndViewsCommand = new RelayCommand(async _ => await LoadTablesAndViewsAsync());
            LoadTableStructureCommand = new RelayCommand(async parameter =>
                await LoadTableStructureAsync(parameter?.ToString()),
                parameter => parameter != null);
        }

        public async Task LoadTablesAndViewsAsync()
        {
            var response = await _httpClient.GetAsync("dbschema/tables-and-views");
            if (response.IsSuccessStatusCode)
            {
                var rawJson = await response.Content.ReadAsStringAsync();
                try
                {
                    var tableNames = JsonSerializer.Deserialize<List<string>>(rawJson);
                    if (tableNames != null)
                    {
                        TablesAndViews.Clear();
                        foreach (var tableName in tableNames)
                        {
                            TablesAndViews.Add(new TableOrView { Name = tableName });
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                    Console.WriteLine($"Raw JSON: {rawJson}");
                    throw;
                }
            }
        }

        private async Task LoadTableStructureAsync(string tableName)
        {
            var response = await _httpClient.GetAsync($"dbschema/structure?tableName={tableName}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                try
                {
                    var structure = JsonSerializer.Deserialize<List<string>>(data);

                    if (structure != null)
                    {
                        SelectedTableStructure.Clear();

                        foreach (var column in structure)
                        {
                            SelectedTableStructure.Add(new TableOrView { Name = column });
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                    Console.WriteLine($"Raw JSON: {data}");
                    throw;
                }
            }
            else
            {
                Console.WriteLine($"Error: Failed to fetch table structure for {tableName}. Status code: {response.StatusCode}");
            }
        }
    }
}