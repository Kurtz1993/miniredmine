﻿using MiniRedmine.Web.Models.Redmine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiniRedmine.Web.Services
{
    public class RedmineHttpService : BaseHttpClientService
    {
        private const string REDMINE_AUTH_HEADER = "X-Redmine-API-Key";
        public RedmineHttpService(HttpClient client) : base(client)
        {

        }

        public async Task<CurrentUser> GetCurrentUserAsync(string userApiKey)
        {
            CurrentUser result = null;
            _httpClient.DefaultRequestHeaders.Add(REDMINE_AUTH_HEADER, userApiKey);
            using (var response = await _httpClient.GetAsync("users/current.json"))
            {
                response.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<CurrentUserContainer>(await response.Content.ReadAsStringAsync())?.User;
            }
            return result;
        }

        public async Task<Issue> GetIssueAsync(string userApiKey,int issueId)
        {
            Issue result = null;
            _httpClient.DefaultRequestHeaders.Add(REDMINE_AUTH_HEADER, userApiKey);
            using (var response = await _httpClient.GetAsync($"issues/{issueId}.json"))
            {
                response.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<IssueContainer>(await response.Content.ReadAsStringAsync())?.Issue;
            }
            return result;
        }

        public async Task<IEnumerable<Activity>> GetTimeEntryActivitiesASync(string userApiKey)
        {
            IEnumerable<Activity> result = null;
            _httpClient.DefaultRequestHeaders.Add(REDMINE_AUTH_HEADER, userApiKey);
            using (var response = await _httpClient.GetAsync("enumerations/time_entry_activities.json"))
            {
                response.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<TimeEntryActivitiesContainer>(await response.Content.ReadAsStringAsync())?.TimeEntryActivites;
            }
            return result;
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesAsync(string userApiKey, int userId, string from, string to)
        {
            IEnumerable<TimeEntry> result = null;
            _httpClient.DefaultRequestHeaders.Add(REDMINE_AUTH_HEADER, userApiKey);
            using (var response = await _httpClient.GetAsync($"time_entries.json?limit=100&user_id={userId}&from={from}&to={to}"))
            {
                response.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<TimeEntriesContainer>(await response.Content.ReadAsStringAsync())?.TimeEntries;
            }
            return result;
        }
        public async Task<IEnumerable<TimeEntry>> CreateTimeEntriesAsync(string userApiKey, List<CreateTimeEntryContainer> createTimeEntries)
        {
            var result = new List<TimeEntry>();
            //_httpClient.DefaultRequestHeaders.Add(REDMINE_AUTH_HEADER, userApiKey);
            foreach (var entry in createTimeEntries)
            {
                using (var httpContent = CreateJsonHttpContent(entry))
                {
                    using (var response = await _httpClient.PostAsync($"time_entries.json?key={userApiKey}", httpContent))
                    {
                        response.EnsureSuccessStatusCode();
                        var createTimeEntryResult = JsonConvert.DeserializeObject<CreateTimeEntryResult>(await response.Content.ReadAsStringAsync());
                        if (createTimeEntryResult is CreateTimeEntryResult)
                        {
                            result.Add(createTimeEntryResult.TimeEntry);
                        }
                    }
                }
            }
            return result;
        }
    }
}
