using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Asana.Maui.ViewModels
{
	public class ProjectDetailViewModel
	{
		public ProjectDetailViewModel()
		{
			Model = new Projects();
			DeleteCommand = new Command(DoDelete);
		}

		public ProjectDetailViewModel(int id)
		{
			Model = ProjectServiceProxy.Current.GetById(id) ?? new Projects();
			DeleteCommand = new Command(DoDelete);
		}

		public ProjectDetailViewModel(Projects? model)
		{
			Model = model ?? new Projects();
			DeleteCommand = new Command(DoDelete);
		}

		public void DoDelete()
		{
			ProjectServiceProxy.Current.DeleteProject(Model);
		}

		public Projects? Model { get; set; }
		public ICommand? DeleteCommand { get; set; }

		public List<int> Statuses
		{
			get
			{
				return new List<int> { 0, 1, 2, 3 };
			}
		}

		public int SelectedStatus
		{
			get
			{
				return Model?.Status ?? 0;
			}
			set
			{
				if (Model != null && Model.Status != value)
				{
					Model.Status = value;
				}
			}
		}
		
		public void AddOrUpdateProject()
		{
			ProjectServiceProxy.Current.AddOrUpdate(Model);
		}
	}
}