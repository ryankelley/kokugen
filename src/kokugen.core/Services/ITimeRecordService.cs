using System;
using System.Collections.Generic;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories.Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface ITimeRecordService
    {
        IEnumerable<TimeRecord> GetAllTimeRecords();
        
    }

    public class TimeRecordService : ITimeRecordService
    {
        private readonly ITimeRecordRepository _timeRecordRepository;

        public TimeRecordService(ITimeRecordRepository timeRecordRepository)
        {
            _timeRecordRepository = timeRecordRepository;
        }


        public IEnumerable<TimeRecord> GetAllTimeRecords()
        {
            return _timeRecordRepository.Query();
        }
    }
}