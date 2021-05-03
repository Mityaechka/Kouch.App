using Kouch.App.Constants;
using Kouch.App.ViewModels;
using Kouch.App.Views.DataTemplates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kouch.App.DataTemplateSelectors
{
    class RegistrationPageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CodeInputDataTemplate { get; set; }
        public DataTemplate EmailAndPasswordDataTemplate { get; set; }



        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((RegisterViewModel.RegisterState)item).State)
            {
                case RegistrationPageConstants.EMAIL:
                    return EmailAndPasswordDataTemplate;
                case RegistrationPageConstants.CODE:
                    return CodeInputDataTemplate;
                default:
                    throw new Exception("Передан неправильный шаблон.");
            }
        }
    }
}
