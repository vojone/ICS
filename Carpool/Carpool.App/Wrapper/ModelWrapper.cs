using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Carpool.App.ViewModel;
using Carpool.BL.Models;
using Microsoft.IdentityModel.Tokens;

//Based on example project "CookBook"
namespace Carpool.App.Wrapper
{
    public abstract class ModelWrapper<T> : ViewModelBase, IModel, INotifyDataErrorInfo
        where T : IModel
    {
        protected ModelWrapper(T? model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Model = model;
        }

        public Guid Id
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public T Model { get; }

        public virtual void Validate(string? propertyName = null)
        {
        }

        protected TValue? GetValue<TValue>([CallerMemberName] string? propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName ?? string.Empty);
            return (propertyInfo?.GetValue(Model) is TValue
                ? (TValue?)propertyInfo.GetValue(Model)
                : default);
        }

        protected void SetValue<TValue>(TValue value, [CallerMemberName] string? propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName ?? string.Empty);
            var currentValue = propertyInfo?.GetValue(Model);
            if (!Equals(currentValue, value))
            {
                propertyInfo?.SetValue(Model, value);
                OnPropertyChanged(propertyName);
            }

            ClearErrors(propertyName);

            Validate(propertyName);

            OnErrorsChanged(propertyName);
        }

        protected void RegisterCollection<TWrapper, TModel>(
            ObservableCollection<TWrapper> wrapperCollection,
            ICollection<TModel> modelCollection)
            where TWrapper : ModelWrapper<TModel>, IModel
            where TModel : IModel
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                foreach (var model in wrapperCollection.Select(i => i.Model))
                {
                    modelCollection.Add(model);
                }
            };
        }


        //Validation is based on https://www.youtube.com/watch?v=XW88Aa12mx8
        private readonly Dictionary<string?, List<string>> _propNameToErrorsDict = new Dictionary<string?, List<string>>();

        public bool HasErrors => _propNameToErrorsDict.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propName)
        {
            return _propNameToErrorsDict!.GetValueOrDefault(propName, new List<string>());
        }

        protected void OnErrorsChanged(string? propName)
        {
            if (propName == null) throw new ArgumentNullException(nameof(propName));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(propName)));
        }

        protected void AddError(string? propName, string errorDetails)
        {
            if (!_propNameToErrorsDict.ContainsKey(propName))
            {
                _propNameToErrorsDict.Add(propName, new List<string>());
                _propNameToErrorsDict[propName].Add(errorDetails);
                OnErrorsChanged(propName);
            }
            else
            {
                if (!_propNameToErrorsDict[propName].Contains(errorDetails))
                {
                    _propNameToErrorsDict[propName].Add(errorDetails);
                    OnErrorsChanged(propName);
                }
            }
        }

        protected void ClearErrors(string? propName)
        {
            if (!_propNameToErrorsDict.ContainsKey(propName)) return;
            _propNameToErrorsDict.Remove(propName);
            OnErrorsChanged(propName);
        }
    }
}
