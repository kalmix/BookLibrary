using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using BookLibrary.ViewModels;
using System;
using System.IO;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BookLibrary.Views;

public sealed partial class AddBookPage : Page
{
    public AddBookViewModel ViewModel { get; }

    public AddBookPage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<AddBookViewModel>();
        ViewModel.OnBookAdded += ViewModel_OnBookAdded;
    }

    private void ViewModel_OnBookAdded()
    {
        // clerear textboxes
        ViewModel.Title = string.Empty;
        ViewModel.Author = string.Empty;
        ViewModel.PdfPath = string.Empty;

        // ir a la pagina principal
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
        }
    }

    private async void SelectPdf_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        
        // hwnd es para que el file picker sirva
        var hwnd = WindowNative.GetWindowHandle(App.Current.MainWindow);
        InitializeWithWindow.Initialize(picker, hwnd);

        picker.ViewMode = PickerViewMode.List;
        picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
        picker.FileTypeFilter.Add(".pdf");

        var file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            // aqui lo copiamos, por ahora esto lo derajara en el bin el /Storage/Books
            var storagePath = Path.Combine(AppContext.BaseDirectory, "Storage", "Books");
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            // usamos un GUID para identificar el archivo de forma unica
            var newFileName = $"{Guid.NewGuid()}_{file.Name}";
            var destinationPath = Path.Combine(storagePath, newFileName);

            File.Copy(file.Path, destinationPath, true);

            // guardamos la ruta relativa en el ViewModel
            ViewModel.PdfPath = Path.Combine("Storage", "Books", newFileName);
        }
    }
}
