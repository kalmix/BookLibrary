using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using BookLibrary.ViewModels;
using System.Threading.Tasks;
using System;
using BookLibrary.Entities.Models;

namespace BookLibrary.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; }

    public HomePage()
    {
        this.InitializeComponent();
        ViewModel = App.Current.Services.GetRequiredService<HomeViewModel>();
        this.Loaded += HomePage_Loaded;
    }

    private async void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadBooksAsync();
    }


    // pa abrir el pdf
    private void OpenPdf_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is Book book)
        {
            ViewModel.OpenPdfCommand.Execute(book);
        }
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is Book book)
        {
            var dialog = new ContentDialog
            {
                Title = "Eliminar Libro",
                Content = $"Estas seguro de que deseas eliminar '{book.Title}'?",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar",
                XamlRoot = this.XamlRoot,
                DefaultButton = ContentDialogButton.Close
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ViewModel.DeleteBookCommand.Execute(book);
            }
        }
    }
}
