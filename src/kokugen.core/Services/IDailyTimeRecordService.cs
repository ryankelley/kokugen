using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface IDailyTimeRecordService
    {
        IEnumerable<DailyTimeRecord> GetAllTimeRecords();
    }

    public class DailyTimeRecordService : IDailyTimeRecordService
    {
        private readonly IDailyTimeRecordRepository _dailyTimeRecordRepository;

        public DailyTimeRecordService(IDailyTimeRecordRepository dailyTimeRecordRepository)
        {
            _dailyTimeRecordRepository = dailyTimeRecordRepository;
        }

        public IEnumerable<DailyTimeRecord> GetAllTimeRecords()
        {
            return _dailyTimeRecordRepository.Query();
        }
    }
}