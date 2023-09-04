using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.NotifyChange;
using Immowert4You.Presentation.Properties.Views.Create;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using Immowert4You.Presentation.Common.Services.PopUps;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertyRoomsViewModel : BaseViewModel
    {
        private ICommand _navigateToUserFeedback;
        private bool _isWarningVisible;
        private readonly ITempStorage _tempStorage;

        public PropertyRoomsViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage) : base(busyManager, navigationService)
        {
            Header = "Teil 3/5";

            _tempStorage = tempStorage;

            Photos = new ObservableCollection<Photo>(
                tempStorage.Read<List<Photo>>() ?? new List<Photo>(GetEmptyPhotosTemplate()));

            foreach (var photo in Photos)
            {
                photo.TakePhoto = new Command<Photo>(async (photo) => await TakePhotoExecute(photo));
                photo.PickPhotoFromGallery = new Command<Photo>(async (photo) => await PickPhotoExecute(photo));
            }

            AskForPermissions();
        }

        private async void AskForPermissions()
        {
            await Permissions.RequestAsync<Permissions.Camera>();
            await Permissions.RequestAsync<Permissions.Photos>();
        }

        public ObservableCollection<Photo> Photos { get; }

        public bool IsWarningVisible
        {
            get => _isWarningVisible;
            set => RiseAndSetIfChanged(ref _isWarningVisible, value);
        }

        public ICommand NavigateToUserFeedback => _navigateToUserFeedback ??= 
            new Command(async () => await NavigateToUserFeedbackExecute());

        private async Task NavigateToUserFeedbackExecute()
        {
            var photos = Photos.Where(p => p.File != null);

            if (photos.Count() < 2)
            {
                IsWarningVisible = true;
                return;
            }

            _tempStorage.Save(photos.ToList());

            await _navigationService.PushAsync<PropertyAddressPage>();
        }


        private async Task TakePhotoExecute(Photo photo)
        {
            if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)
            {
                try
                {
                    var takenPhoto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() { });

                    if (takenPhoto != null)
                    {
                        photo.File = takenPhoto;
                        _tempStorage.Save(Photos.ToList());
                    }
                }
                catch (Exception ex)
                {
                    await PopUpHelper.ShowAlert("Error", ex.Message);
                }
            }
        }
        private async Task PickPhotoExecute(Photo photo)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                try
                {
                    var mediaOption = new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var pickedPhoto = await CrossMedia.Current.PickPhotoAsync(mediaOption);

                    if (pickedPhoto != null)
                    {
                        photo.File = pickedPhoto;
                        _tempStorage.Save(Photos.ToList());
                    }
                }
                catch(Exception ex)
                {
                    await PopUpHelper.ShowAlert("Error", ex.Message);
                }
            }
        }

        private List<Photo> GetEmptyPhotosTemplate()
        {
            return new List<Photo>
            {
                new Photo { Title = "Küche" },
                new Photo { Title = "Wohnzimmer" },
                new Photo { Title = "Badezimmer" },
                new Photo { Title = "Vorzimmer" },
                new Photo { Title = "Aussenansicht" },
                new Photo { Title = "Garten" },
                new Photo { Title = "Plan" },
                new Photo
                {
                    Title = "Foto Ihrer Wahl",
                    IsRequred = false
                },
                new Photo
                {
                    Title = "Foto Ihrer Wahl",
                    IsRequred = false
                },
            };
        }
    }

    public class Photo : PropertyChangeHelper
    {
        private bool _isPhotoProvided;
        private string _buttonColor;
        private string _fontColor;
        private MediaFile _file;

        public string Title { get; set; }

        public MediaFile File
        {
            get => _file;
            set
            {
                IsPhotoProvided = value != null;
                RiseAndSetIfChanged(ref _file, value);
            }
        }

        public bool IsRequred { get; set; } = true;

        public bool IsPhotoProvided
        {
            get => _isPhotoProvided;
            set
            {
                ButtonColor = value ? "#0D62B0" : "#e6e6e6";
                FontColor = value ? "#ffffff" : "#000000";
                RiseAndSetIfChanged(ref _isPhotoProvided, value);
            }
        }

        public string ButtonColor
        {
            get => _buttonColor;
            set => RiseAndSetIfChanged(ref _buttonColor, value);
        }

        public string FontColor
        {
            get => _fontColor;
            set => RiseAndSetIfChanged(ref _fontColor, value);
        }

        public ICommand TakePhoto { get; set; }
        public ICommand PickPhotoFromGallery { get; set; }
    }
}