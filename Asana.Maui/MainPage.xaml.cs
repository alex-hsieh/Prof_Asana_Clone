﻿using Asana.Maui.ViewModels;

namespace Asana.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        // ToDo Events
        private void AddNewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ToDoDetails");
        }
        
        private void EditClicked(object sender, EventArgs e)
        {
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedToDoId ?? 0;
            Shell.Current.GoToAsync($"//ToDoDetails?toDoId={selectedId}");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.DeleteToDo();
        }

        private void InLineDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        // Project Events
        private void AddProjectClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ProjectDetailView");
        }

        private void EditProjectClicked(object sender, EventArgs e)
        {
            var selectedId = (BindingContext as MainPageViewModel)?.SelectedProjectId ?? 0;
            Shell.Current.GoToAsync($"//ProjectDetailView?projectId={selectedId}");
        }

        private void DeleteProjectClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.DeleteProject();
        }

        private void InLineProjectDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        // Navigation Events
        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as MainPageViewModel)?.RefreshPage();
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {

        }
    }
}