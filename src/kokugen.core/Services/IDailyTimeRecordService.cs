using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Services
{
    public interface IDailyTimeRecordService
    {
        IEnumerable<DailyTimeRecord> GetAllTimeRecords();
        INotification Save(DailyTimeRecord dailyTimeRecord);
        DailyTimeRecord Get(Guid id);
    }

    public class DailyTimeRecordService : IDailyTimeRecordService
    {
        private readonly IDailyTimeRecordRepository _dailyTimeRecordRepository;
        private readonly IValidator _validator;

        public DailyTimeRecordService(IDailyTimeRecordRepository dailyTimeRecordRepository, IValidator validator)
        {
            _dailyTimeRecordRepository = dailyTimeRecordRepository;
            _validator = validator;
        }

        public IEnumerable<DailyTimeRecord> GetAllTimeRecords()
        {
            return _dailyTimeRecordRepository.Query();
        }


        public INotification Save(DailyTimeRecord dailyTimeRecord)
        {
            var notification = _validator.Validate(dailyTimeRecord);

            if(notification.IsValid())
            {
                _dailyTimeRecordRepository.Save(dailyTimeRecord);
            }

            return notification;
        }

        public DailyTimeRecord Get(Guid id)
        {
            return _dailyTimeRecordRepository.Get(id);
        }
    }
}