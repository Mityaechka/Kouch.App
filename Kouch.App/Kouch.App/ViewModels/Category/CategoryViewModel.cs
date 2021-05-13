using Kouch.App.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class CategorySelectorViewModel : AsyncBaseViewModel
    {
        public ICommand SelectCategoryCommand => new Command<object>(id => SelectCategory(Convert.ToInt32(id)));
        private CategoryViewModel parentCategory;
        public CategoryViewModel ParentCategory
        {
            get => parentCategory;
            set
            {
                parentCategory = value;
                OnPropertyChanged();
                OnPropertyChanged("AvailableCategories");
            }
        }

        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                OnPropertyChanged("PreviousCategories");
                OnPropertyChanged("AvailableCategories");
            }
        }
        public List<CategoryViewModel> PreviousCategories
        {
            get
            {
                List<CategoryViewModel> viewModels = new List<CategoryViewModel>();
                CategoryViewModel current = selectedCategory?.Parent;
                while (current != null)
                {
                    viewModels.Add(current);
                    current = current.Parent;
                }
                viewModels.Reverse();
                return viewModels;
            }
        }
        public List<CategoryViewModel> AvailableCategories
        {
            get
            {
                List<CategoryViewModel> categories = new List<CategoryViewModel>();
                if (selectedCategory == null)
                {
                    categories.AddRange(parentCategory.Children);
                }
                else
                {
                    categories.AddRange(selectedCategory.Children);
                }
                return categories;
            }
        }
        public CategorySelectorViewModel(Category parentCategory, INavigation navigation) : base(navigation)
        {
            Init(parentCategory);
        }

        private async void Init(Category parentCategory)
        {
            if (parentCategory == null)
            {
                List<Category> categories = await LoadCategory();
                CategoryViewModel viewModel = CategoryViewModel.GenerateViewModel(categories);
                ParentCategory= viewModel;
                if (SelectedCategory == null)
                {
                    SelectedCategory = viewModel;
                }
            }
            else
            {
                ParentCategory = CategoryViewModel.GenerateViewModel(parentCategory);
            }
        }
        private async Task<List<Category>> LoadCategory()
        {
            IsLoading = true;
            await Task.Delay(2000);
            IsLoading = false;
            return new List<Category>{
                new Category
                {

                    Id = 1,
                    Name = "Красота, здоровье",
                    Children = new List<Category>
                    {
                        new Category
                        {
                            Id=2,
                            Name="Макияж",
                            Children = new List<Category>
                            {
                                new Category
                                {
                                    Id=5,
                                    Name="Глазки",
                                    Children = new List<Category>
                                    {
                                        new Category
                                        {
                                            Id = 7,
                                            Name="Ресницы"
                                        },
                                        new Category
                                        {
                                            Id=8,
                                            Name = "Брови",
                                            Children = new List<Category>
                                            {
                                                new Category
                                                {
                                                    Id=8,
                                                    Name="Правая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                },
                                                new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                }
                                                ,new Category
                                                {
                                                    Id=9,
                                                    Name="Левая бровь"
                                                }
                                            }
                                        }
                                    }
                                },
                                new Category
                                {
                                    Id = 6,
                                    Name ="Губки"
                                }
                            }
                        }
                    }
                },
                new Category
                {
                    Id = 3,
                    Name = "Обучение, курсы",
                },
                new Category
                {
                    Id = 4,
                    Name = "Другое",
                }
            };
        }
        private void SelectCategory(int id)
        {
            SelectedCategory = ParentCategory.FindById(id);
        }
    }


    public class CategoryViewModel : BaseViewModel
    {
        private bool isRoot;
        private int id;
        private string name;
        public CategoryViewModel Parent { get; set; }
        public int Id
        {
            get => id; set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => name; set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsRoot
        {
            get => isRoot; set
            {
                if (isRoot != value)
                {
                    isRoot = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<CategoryViewModel> Children { get; set; }


        public CategoryViewModel() : base(null)
        {
        }
        public CategoryViewModel FindById(int id)
        {
            if (Id == id)
            {
                return this;
            }
            foreach (var item in Children)
            {
                var search = item.FindById(id);
                if (search != null)
                {
                    return search;
                }
            }
            return null;
        }
        public static CategoryViewModel GenerateViewModel(List<Category> categories)
        {
            try
            {
                CategoryViewModel root = new CategoryViewModel
                {
                    Id = 0,
                    Name = "Все",
                    Children = new ObservableCollection<CategoryViewModel>(),
                    IsRoot = true
                };
                foreach (var category in categories)
                {
                    root.Children.Add(GenerateViewModel(category, root));
                }

                return root;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static CategoryViewModel GenerateViewModel(Category category, CategoryViewModel parent = null)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Children = new ObservableCollection<CategoryViewModel>(),
                Parent = parent
            };
            if (category.Children != null && category.Children.Count != 0)
            {
                foreach (var item in category.Children)
                {
                    categoryViewModel.Children.Add(GenerateViewModel(item, categoryViewModel));
                }
            }
            return categoryViewModel;
        }
    }
}
