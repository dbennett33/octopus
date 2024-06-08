namespace Octopus.ApiClient
{
    public static class ApiGlobal
    {
        public static class Endpoints
        {
            public const string BASE_URL = "https://api-football-v1.p.rapidapi.com/v3";
            public const string COUNTRIES = $"{BASE_URL}/countries";
            public const string LEAGUES = $"{BASE_URL}/leagues";
            public const string TEAMS = $"{BASE_URL}/teams";
            public const string TEAMS_STATS = $"{BASE_URL}/teams/statistics";
            public const string PLAYERS = $"{BASE_URL}/players";
            public const string FIXTURES = $"{BASE_URL}/fixtures";
        }
        
        public static class Headers
        {
            public const string NAME_API_KEY = "x-rapidapi-key";
            public const string NAME_HOST = "x-rapidapi-host";
        }

        public static class ResponseHeaders
        {
            public const string NAME_CALL_LIMIT = "x-ratelimit-requests-limit";
            public const string NAME_CALLS_REMAINING = "x-ratelimit-requests-remaining";
        }
    }
}