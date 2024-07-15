using dosymep.Bim4Everyone.SimpleServices.Configuration;

namespace dosymep.Bim4Everyone.SimpleServices {
    internal sealed class PlatformConfigurationService : IPlatformConfigurationService {
        private readonly IniConfigurationService _configurationService;

        public PlatformConfigurationService(IniConfigurationService configurationService) {
            _configurationService = configurationService;
        }

        public LogTrace LogTrace => new LogTrace(_configurationService);
        public LogTraceJournal LogTraceJournal => new LogTraceJournal(_configurationService);

        public CorpSettings CorpSettings => new CorpSettings(_configurationService);
        public SocialsSettings SocialsSettings => new SocialsSettings(_configurationService);

        public NotificationSettings NotificationSettings => new NotificationSettings(_configurationService);
    }
}