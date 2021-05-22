using Kouch.App.Constants;
using Kouch.App.ViewModels;
using System;
using Xamarin.Forms;

namespace Kouch.App.DataTemplateSelectors
{
    internal class RegistrationPageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CodeInputDataTemplate { get; set; }
        public DataTemplate EmailAndPasswordDataTemplate { get; set; }



        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((RegisterStateViewModel)item).State)
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
