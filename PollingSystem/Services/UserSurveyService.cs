using Microsoft.EntityFrameworkCore.Diagnostics;
using PollingSystem.Dtos;
using PollingSystem.Entities;
using PollingSystem.Enum;
using PollingSystem.Interface.IRepositories;
using PollingSystem.Interface.IServices;
using Spectre.Console;

namespace PollingSystem.Services
{
    public class UserSurveyService : IUserSurveyService
    {
        private readonly IUserSurveyRepository _userSurveyRepo;

        public UserSurveyService(IUserSurveyRepository userSurveyRepo)
        {
            _userSurveyRepo = userSurveyRepo;
        }

        public void AddUserSurvey(int userId, int surveyId)
        {
            _userSurveyRepo.AddUserSurvey(userId,surveyId,SurveyStatus.NotDone);
        }

        public bool HasUserSurvey(int userId, int surveyId)
        {
            return _userSurveyRepo.HasUserSurvey(userId,surveyId);
        }

        public void SetSurveyDone(int userId, int surveyId)
        {
            _userSurveyRepo.SetStatus(userId, surveyId, SurveyStatus.Done);
        }

        public void UpdateUserSurvey(int userId, int surveyId, SurveyStatus status)
        {
            if (_userSurveyRepo.HasUserSurvey(userId, surveyId))
            {
                _userSurveyRepo.SetStatus(userId, surveyId, status);
            }
            else
            {
                _userSurveyRepo.AddUserSurvey(userId, surveyId, status);
            }
        }

        public UserSurveydto? GetUserSurvey(int userId, int surveyId)
        {
            var us = _userSurveyRepo.getByUserSurvey(userId, surveyId);
            if (us == null) return null;

            return new UserSurveydto
            {
                UserId = us.UserId,
                surveyId = us.SurveyId,
                Status = us.Status.ToString()
            };
        }
    }
}
