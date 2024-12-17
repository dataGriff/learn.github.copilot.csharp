using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ParallelAsyncExample
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client = new HttpClient { MaxResponseContentBufferSize = 1_000_000 };

        private readonly IEnumerable<string> _urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/xamarin",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/gaming"
        };

        /// <summary>
        /// The MainWindow class demonstrates an example of performing asynchronous operations
        /// in parallel using HttpClient to download content from a list of URLs and display
        /// the results in a WPF application.
        /// </summary>
        public partial class MainWindow : Window
        {
            /// <summary>
            /// Handles the click event of the Start button.
            /// Disables the Start button and clears the results text box.
            /// Initiates the asynchronous operation to start summing page sizes.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The event data.</param>
            private void OnStartButtonClick(object sender, RoutedEventArgs e)
            {
                // Implementation
            }

            /// <summary>
            /// Starts the asynchronous operation to sum the sizes of pages from the URL list.
            /// </summary>
            private async Task StartSumPageSizesAsync()
            {
                // Implementation
            }

            /// <summary>
            /// Sums the sizes of pages from the URL list asynchronously.
            /// </summary>
            private async Task SumPageSizesAsync()
            {
                // Implementation
            }

            /// <summary>
            /// Processes the URL asynchronously by downloading its content and returning the size of the content.
            /// </summary>
            /// <param name="url">The URL to process.</param>
            /// <param name="client">The HttpClient used to download the content.</param>
            /// <returns>The size of the downloaded content in bytes.</returns>
            private async Task<int> ProcessUrlAsync(string url, HttpClient client)
            {
                // Implementation
            }

            /// <summary>
            /// Displays the results of the URL processing asynchronously.
            /// </summary>
            /// <param name="url">The URL that was processed.</param>
            /// <param name="content">The content downloaded from the URL.</param>
            /// <returns>A task representing the asynchronous operation.</returns>
            private Task DisplayResultsAsync(string url, byte[] content)
            {
                // Implementation
            }

            /// <summary>
            /// Disposes the HttpClient when the window is closed.
            /// </summary>
            /// <param name="e">The event data.</param>
            protected override void OnClosed(EventArgs e)
            {
                // Implementation
            }
        }

        private async Task StartSumPageSizesAsync()
        {
            await SumPageSizesAsync();
            await Dispatcher.BeginInvoke(() =>
            {
                _resultsTextBox.Text += $"\nControl returned to {nameof(OnStartButtonClick)}.";
                _startButton.IsEnabled = true;
            });
        }

        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            IEnumerable<Task<int>> downloadTasksQuery =
                from url in _urlList
                select ProcessUrlAsync(url, _client);

            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            int[] lengths = await Task.WhenAll(downloadTasks);
            int total = lengths.Sum();

            await Dispatcher.BeginInvoke(() =>
            {
                stopwatch.Stop();

                _resultsTextBox.Text += $"\nTotal bytes returned:  {total:#,#}";
                _resultsTextBox.Text += $"\nElapsed time:          {stopwatch.Elapsed}\n";
            });
        }

        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            try
            {
            byte[] byteArray = await client.GetByteArrayAsync(url);
            await DisplayResultsAsync(url, byteArray);

            return byteArray.Length;
            }
            catch (HttpRequestException e)
            {
            await Dispatcher.BeginInvoke(() =>
                _resultsTextBox.Text += $"{url,-60} {"Error: " + e.Message,10}\n");
            return 0;
            }
        }

        private Task DisplayResultsAsync(string url, byte[] content) =>
            Dispatcher.BeginInvoke(() =>
                _resultsTextBox.Text += $"{url,-60} {content.Length,10:#,#}\n")
                      .Task;

        protected override void OnClosed(EventArgs e) => _client.Dispose();
    }
}
