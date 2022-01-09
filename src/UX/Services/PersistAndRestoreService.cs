using Seemon.Authenticator;
using Seemon.Authenticator.Contracts.Services;
using System.Collections;

namespace Authenticator.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly string _settingsFilename = "authenticator.settings.json";

        public PersistAndRestoreService(IFileService fileService, IApplicationInfoService applicationInfoService)
        {
            _fileService = fileService;
            _applicationInfoService = applicationInfoService;
        }

        public void PersistData()
        {
            if (App.Current.Properties != null)
            {
                _fileService.Save(_applicationInfoService.DataPath, _settingsFilename, App.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var properties = _fileService.Read<IDictionary>(_applicationInfoService.DataPath, _settingsFilename);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    App.Current.Properties.Add(property.Key, property.Value);
                }
            }
        }
    }
}
